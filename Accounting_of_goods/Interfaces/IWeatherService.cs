namespace WinFormsApp1.Interfaces
{
    public interface IWeatherService
    {
        /// <summary>
        /// Получает прогноз погоды на 3 дня для указанного города.
        /// Использует Nominatim (OSM) для геокодинга и Open-Meteo для погоды.
        /// </summary>
        Task<WeatherForecastResult> GetForecastAsync(string cityName);

        /// <summary>
        /// Загружает категории товаров из БД и заполняет WeatherSettings.Thresholds
        /// значениями по умолчанию (если запись ещё не создана).
        /// </summary>
        Task LoadCategoriesAsync();

        /// <summary>
        /// Возвращает список категорий с текущими порогами.
        /// </summary>
        Task<List<ProductTempThreshold>> GetThresholdsAsync();

        /// <summary>
        /// Формирует список рекомендаций на основе прогноза и порогов категорий.
        /// </summary>
        List<string> BuildRecommendations(WeatherForecastResult forecast);
    }
}
