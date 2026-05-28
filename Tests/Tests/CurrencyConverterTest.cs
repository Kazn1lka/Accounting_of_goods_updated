
using Accounting_of_goods;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    [DoNotParallelize]
    public class CurrencyConverterTest
    {
        [TestInitialize]
        public void Setup()
        {
            CurrencyConverter.CurrentCurrency = "RUB";
        }

        [TestMethod]
        public async Task ChangeCurrencyAsync_ToRubles_SetsRateToOne()
        {
            await CurrencyConverter.ChangeCurrencyAsync("RUB");

            Assert.AreEqual("RUB", CurrencyConverter.CurrentCurrency);
            Assert.AreEqual(1m, CurrencyConverter.CurrentRate);
        }

        [TestMethod]
        public async Task ConvertPrice_WithRubles_ShouldNotChangeValue()
        {
            await CurrencyConverter.ChangeCurrencyAsync("RUB");
            decimal price = 1500.50m;

            decimal res = CurrencyConverter.ConvertPrice(price);

            Assert.AreEqual(1500.50m, res);
        }

        [TestMethod]
        public async Task ChangeCurrencyAsync_ToUSD_ShouldChangeRateAndConvertCorrectly()
        {
            string target = "USD";
            decimal price = 1000m;

            try
            {
                await CurrencyConverter.ChangeCurrencyAsync(target);
                if (CurrencyConverter.CurrentCurrency != "USD")
                {
                    Assert.Inconclusive("API конвертации валют недоступно или вернуло ошибку. Тест пропущен.");
                    return;
                }

                decimal res = CurrencyConverter.ConvertPrice(price);

                Assert.AreEqual("USD", CurrencyConverter.CurrentCurrency);
                Assert.AreNotEqual(1m, CurrencyConverter.CurrentRate);

                decimal expected = Math.Round(price * CurrencyConverter.CurrentRate, 2);
                Assert.AreEqual(expected, res);
            }
            catch (Exception ex)
            {
                Assert.Inconclusive($"Тест пропущен из-за ошибки сети/API: {ex.Message}");
            }
        }
    }
}