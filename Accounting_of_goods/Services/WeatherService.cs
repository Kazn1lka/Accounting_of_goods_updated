using System.Net.Http;
using System.Text.Json;

namespace WinFormsApp1.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ApplicationDbContext _db;
        private static readonly HttpClient _http = new HttpClient
        {
            DefaultRequestHeaders = { { "User-Agent", "Accounting_of_goods_logistics/1.0" } }
        };

        public WeatherService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<WeatherForecastResult> GetForecastAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return Fail("Введите название города.");

            try
            {
                var geoUrl = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(cityName)}&format=json&limit=1&accept-language=ru";
                var geoJson = await _http.GetStringAsync(geoUrl);
                using var geoDoc = JsonDocument.Parse(geoJson);

                if (geoDoc.RootElement.GetArrayLength() == 0)
                    return Fail($"Город «{cityName}» не найден.");

                var place = geoDoc.RootElement[0];
                double lat = double.Parse(place.GetProperty("lat").GetString()!, System.Globalization.CultureInfo.InvariantCulture);
                double lon = double.Parse(place.GetProperty("lon").GetString()!, System.Globalization.CultureInfo.InvariantCulture);
                string displayName = place.GetProperty("display_name").GetString() ?? cityName;

                var weatherUrl = $"https://api.open-meteo.com/v1/forecast" +
                    $"?latitude={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
                    $"&longitude={lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
                    $"&daily=temperature_2m_max,temperature_2m_min" +
                    $"&forecast_days=3&timezone=auto";

                var weatherJson = await _http.GetStringAsync(weatherUrl);
                using var wDoc = JsonDocument.Parse(weatherJson);

                var daily  = wDoc.RootElement.GetProperty("daily");
                var dates   = daily.GetProperty("time");
                var maxTemps= daily.GetProperty("temperature_2m_max");
                var minTemps= daily.GetProperty("temperature_2m_min");

                var days = new List<DayForecast>();
                string[] labels = { "Сегодня", "Завтра", "+2 дня" };
                for (int i = 0; i < Math.Min(3, dates.GetArrayLength()); i++)
                {
                    days.Add(new DayForecast
                    {
                        Date    = DateTime.Parse(dates[i].GetString()!),
                        TempMax = Math.Round(maxTemps[i].GetDouble(), 1),
                        TempMin = Math.Round(minTemps[i].GetDouble(), 1),
                        Label   = labels[i]
                    });
                }

                return new WeatherForecastResult
                {
                    Success   = true,
                    CityName  = displayName.Split(',')[0].Trim(),
                    Latitude  = lat,
                    Longitude = lon,
                    Days      = days
                };
            }
            catch (HttpRequestException ex)
            {
                return Fail($"Нет доступа к интернету: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Fail($"Ошибка получения погоды: {ex.Message}");
            }
        }

        public async Task LoadCategoriesAsync()
        {
            var cats = await Task.Run(() => _db.Categories.ToList());
            foreach (var cat in cats)
            {
                if (!WeatherSettings.Thresholds.ContainsKey(cat.Id))
                {
                    WeatherSettings.Thresholds[cat.Id] = new ProductTempThreshold
                    {
                        CategoryId   = cat.Id,
                        CategoryName = cat.Name,
                        MinSafeTemp  = -5,
                        MaxSafeTemp  = 35
                    };
                }
                else
                {
                    WeatherSettings.Thresholds[cat.Id].CategoryName = cat.Name;
                }
            }
        }

        public async Task<List<ProductTempThreshold>> GetThresholdsAsync()
        {
            await LoadCategoriesAsync();
            return WeatherSettings.Thresholds.Values.OrderBy(t => t.CategoryName).ToList();
        }

        public List<string> BuildRecommendations(WeatherForecastResult forecast)
        {
            var recs = new List<string>();
            if (!forecast.Success || forecast.Days.Count == 0) return recs;

            double forecastMin = forecast.Days.Min(d => d.TempMin);
            double forecastMax = forecast.Days.Max(d => d.TempMax);

            bool anyFrost = forecastMin < WeatherSettings.GlobalFrostThreshold;
            bool anyHeat  = forecastMax > WeatherSettings.GlobalHeatThreshold;

            var categoryWarnings = new List<string>();
            foreach (var t in WeatherSettings.Thresholds.Values)
            {
                if (forecastMin < t.MinSafeTemp)
                    categoryWarnings.Add($"«{t.CategoryName}»: мин. темп. {forecastMin}°C ниже порога {t.MinSafeTemp}°C");
                if (forecastMax > t.MaxSafeTemp)
                    categoryWarnings.Add($"«{t.CategoryName}»: макс. темп. {forecastMax}°C выше порога {t.MaxSafeTemp}°C");
            }

            if (categoryWarnings.Count == 0 && !anyFrost && !anyHeat)
            {
                recs.Add("✅ Условия доставки в норме. Специальных мер не требуется.");
                return recs;
            }

            if (anyFrost)
                recs.Add($"❄ Аномальный мороз: до {forecastMin}°C. Рекомендуется термоконтейнер (обогрев).");
            if (anyHeat)
                recs.Add($"🔥 Аномальная жара: до {forecastMax}°C. Рекомендуется термоконтейнер (охлаждение).");

            foreach (var w in categoryWarnings)
                recs.Add($"⚠ Категория {w} — рассмотрите страховку груза.");

            if (categoryWarnings.Count > 0)
                recs.Add("📋 Оформите страховку перевозки для чувствительных категорий товаров.");

            return recs;
        }

        private static WeatherForecastResult Fail(string msg)
            => new WeatherForecastResult { Success = false, ErrorMsg = msg };
    }
}
