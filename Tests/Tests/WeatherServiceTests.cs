using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    [DoNotParallelize]
    public class WeatherServiceTests
    {
        [TestInitialize]
        public void Setup()
        {
         
            WeatherSettings.Thresholds.Clear();
            WeatherSettings.Thresholds[1] = new ProductTempThreshold
            {
                CategoryId = 1,
                CategoryName = "Легкая одежда",
                MinSafeTemp = -5,
                MaxSafeTemp = 30
            };
            WeatherSettings.GlobalFrostThreshold = -20;
            WeatherSettings.GlobalHeatThreshold = 35;
        }

        [TestMethod]
        public void BuildRecommendations_FailedForecast_ShouldReturnEmptyList()
        {
            var service = new WeatherService(null);
            var forecast = new WeatherForecastResult { Success = false };

            var result = service.BuildRecommendations(forecast);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void BuildRecommendations_AllOk_ShouldReturnNormalMessage()
        {
            var service = new WeatherService(null);
            var forecast = new WeatherForecastResult
            {
                Success = true,
                Days = new List<DayForecast> { new DayForecast { TempMin = 10, TempMax = 20 } }
            };

            var result = service.BuildRecommendations(forecast);

            Assert.AreEqual(1, result.Count);
            StringAssert.Contains(result[0], "Условия доставки в норме. Специальных мер не требуется.");
        }

        [TestMethod]
        public void BuildRecommendations_ExtremeFrost_ShouldSuggestHeating()
        {
            var service = new WeatherService(null);
            var forecast = new WeatherForecastResult
            {
                Success = true,
                Days = new List<DayForecast> { new DayForecast { TempMin = -25, TempMax = -15 } }
            };

            var result = service.BuildRecommendations(forecast);

            Assert.IsTrue(result.Exists(r => r.Contains("Аномальный мороз") && r.Contains("термоконтейнер (обогрев)")));
        }

        [TestMethod]
        public void BuildRecommendations_BelowCategoryThreshold_ShouldSuggestInsurance()
        {
            var service = new WeatherService(null);
            var forecast = new WeatherForecastResult
            {
                Success = true,
                Days = new List<DayForecast> { new DayForecast { TempMin = -10, TempMax = 0 } }
            };

            var result = service.BuildRecommendations(forecast);

            Assert.IsTrue(result.Exists(r => r.Contains("Легкая одежда") && r.Contains("ниже порога -5°C")));
            Assert.IsTrue(result.Exists(r => r.Contains("рассмотрите страховку груза")));
        }
    }
}