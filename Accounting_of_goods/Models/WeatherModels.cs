namespace WinFormsApp1.Models
{
    public class DayForecast
    {
        public DateTime Date { get; init; }
        public double TempMax { get; init; }
        public double TempMin { get; init; }
        public string Label { get; init; } = "";
    }

    public class WeatherForecastResult
    {
        public bool Success { get; init; }
        public string ErrorMsg { get; init; } = "";
        public string CityName { get; init; } = "";
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public List<DayForecast> Days { get; init; } = new List<DayForecast>();
    }

    public class ProductTempThreshold
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public double MinSafeTemp { get; set; } = -5;
        public double MaxSafeTemp { get; set; } = 35;
    }

    public static class WeatherSettings
    {
        public static Dictionary<int, ProductTempThreshold> Thresholds { get; }
            = new Dictionary<int, ProductTempThreshold>();

        public static double GlobalFrostThreshold { get; set; } = -15;
        public static double GlobalHeatThreshold { get; set; } = 40;
    }
}
