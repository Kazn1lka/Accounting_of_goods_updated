namespace WinFormsApp1.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForecastResult> GetForecastAsync(string cityName);
        Task LoadCategoriesAsync();
        Task<List<ProductTempThreshold>> GetThresholdsAsync();
        List<string> BuildRecommendations(WeatherForecastResult forecast);
    }
}
