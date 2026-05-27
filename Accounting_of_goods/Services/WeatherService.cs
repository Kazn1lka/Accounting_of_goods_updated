using System.Net.Http;
using System.Text.Json;

namespace WinFormsApp1.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ApplicationDbContext _db;
        private static readonly HttpClient _http = new HttpClient();

        public WeatherService(ApplicationDbContext db)
        {
            _db = db;
            _http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "WinFormsApp1/1.0");
        }

        public async Task<WeatherForecastResult> GetForecastAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return new WeatherForecastResult { Success = false, ErrorMsg = "Введите название города." };
            }

            try
            {
                var geoUrl = "https://nominatim.openstreetmap.org/search?q="
                    + Uri.EscapeDataString(cityName)
                    + "&format=json&limit=1&accept-language=ru";

                var geoJson = await _http.GetStringAsync(geoUrl);
                using var geoDoc = JsonDocument.Parse(geoJson);

                if (geoDoc.RootElement.GetArrayLength() == 0)
                {
                    return new WeatherForecastResult { Success = false, ErrorMsg = $"Город «{cityName}» не найден." };
                }

                var place = geoDoc.RootElement[0];
                var latStr = place.GetProperty("lat").GetString();
                var lonStr = place.GetProperty("lon").GetString();

                double lat = double.Parse(latStr!, System.Globalization.CultureInfo.InvariantCulture);
                double lon = double.Parse(lonStr!, System.Globalization.CultureInfo.InvariantCulture);

                string displayName = place.GetProperty("display_name").GetString();
                if (string.IsNullOrEmpty(displayName))
                    displayName = cityName;

                var latStr2 = lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
                var lonStr2 = lon.ToString(System.Globalization.CultureInfo.InvariantCulture);

                var weatherUrl = "https://api.open-meteo.com/v1/forecast"
                    + "?latitude=" + latStr2
                    + "&longitude=" + lonStr2
                    + "&daily=temperature_2m_max,temperature_2m_min"
                    + "&forecast_days=3&timezone=auto";

                var weatherJson = await _http.GetStringAsync(weatherUrl);
                using var wDoc = JsonDocument.Parse(weatherJson);

                var daily = wDoc.RootElement.GetProperty("daily");
                var dates = daily.GetProperty("time");
                var maxTemps = daily.GetProperty("temperature_2m_max");
                var minTemps = daily.GetProperty("temperature_2m_min");

                string[] labels = { "Сегодня", "Завтра", "+2 дня" };
                var days = new List<DayForecast>();
                int count = dates.GetArrayLength();
                if (count > 3)
                    count = 3;

                for (int i = 0; i < count; i++)
                {
                    var df = new DayForecast
                    {
                        Date = DateTime.Parse(dates[i].GetString()!),
                        TempMax = Math.Round(maxTemps[i].GetDouble(), 1),
                        TempMin = Math.Round(minTemps[i].GetDouble(), 1),
                        Label = labels[i]
                    };
                    days.Add(df);
                }

                string shortName = displayName.Split(',')[0].Trim();

                var result = new WeatherForecastResult
                {
                    Success = true,
                    CityName = shortName,
                    Latitude = lat,
                    Longitude = lon,
                    Days = days
                };
                return result;
            }
            catch (HttpRequestException ex)
            {
                return new WeatherForecastResult { Success = false, ErrorMsg = "Нет доступа к интернету: " + ex.Message };
            }
            catch (Exception ex)
            {
                return new WeatherForecastResult { Success = false, ErrorMsg = "Ошибка получения погоды: " + ex.Message };
            }
        }

        public async Task LoadCategoriesAsync()
        {
            var cats = await Task.Run(() => _db.Categories.ToList());

            foreach (var cat in cats)
            {
                bool alreadyExists = WeatherSettings.Thresholds.ContainsKey(cat.Id);

                if (alreadyExists)
                {
                    WeatherSettings.Thresholds[cat.Id].CategoryName = cat.Name;
                }
                else
                {
                    var newThreshold = new ProductTempThreshold
                    {
                        CategoryId = cat.Id,
                        CategoryName = cat.Name,
                        MinSafeTemp = -5,
                        MaxSafeTemp = 35
                    };
                    WeatherSettings.Thresholds[cat.Id] = newThreshold;
                }
            }
        }

        public async Task<List<ProductTempThreshold>> GetThresholdsAsync()
        {
            await LoadCategoriesAsync();

            var sorted = WeatherSettings.Thresholds.Values
                .OrderBy(t => t.CategoryName)
                .ToList();

            return sorted;
        }

        public List<string> BuildRecommendations(WeatherForecastResult forecast)
        {
            var recs = new List<string>();

            if (!forecast.Success)
                return recs;

            if (forecast.Days.Count == 0)
                return recs;

            double forecastMin = forecast.Days[0].TempMin;
            double forecastMax = forecast.Days[0].TempMax;

            foreach (var d in forecast.Days)
            {
                if (d.TempMin < forecastMin)
                    forecastMin = d.TempMin;

                if (d.TempMax > forecastMax)
                    forecastMax = d.TempMax;
            }

            bool anyFrost = forecastMin < WeatherSettings.GlobalFrostThreshold;
            bool anyHeat = forecastMax > WeatherSettings.GlobalHeatThreshold;

            var categoryWarnings = new List<string>();

            foreach (var t in WeatherSettings.Thresholds.Values)
            {
                if (forecastMin < t.MinSafeTemp)
                {
                    categoryWarnings.Add($"«{t.CategoryName}»: мин. темп. {forecastMin}°C ниже порога {t.MinSafeTemp}°C");
                }

                if (forecastMax > t.MaxSafeTemp)
                {
                    categoryWarnings.Add($"«{t.CategoryName}»: макс. темп. {forecastMax}°C выше порога {t.MaxSafeTemp}°C");
                }
            }

            bool allOk = categoryWarnings.Count == 0 && !anyFrost && !anyHeat;

            if (allOk)
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
    }
}
