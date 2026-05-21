using Accounting_of_goods;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class CurrencyConverterTest
    {
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

            await CurrencyConverter.ChangeCurrencyAsync(target);
            decimal res = CurrencyConverter.ConvertPrice(price);

            Assert.AreEqual("USD", CurrencyConverter.CurrentCurrency);

            Assert.AreNotEqual(1m, CurrencyConverter.CurrentRate);

            decimal expected = Math.Round(price * CurrencyConverter.CurrentRate, 2);
            Assert.AreEqual(expected, res);
        }
    }
}