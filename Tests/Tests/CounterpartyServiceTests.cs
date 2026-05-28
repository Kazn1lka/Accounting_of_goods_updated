using WinFormsApp1.Services;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class CounterpartyServiceTests
    {
        private CounterpartyService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new CounterpartyService();
        }

        [TestMethod]
        public void ValidateInn_EmptyString_ShouldReturnFalse()
        {
            var result = _service.ValidateInn("");
            Assert.IsFalse(result.isValid);
            Assert.AreEqual("ИНН не может быть пустым.", result.error);
        }

        [TestMethod]
        public void ValidateInn_ContainsLetters_ShouldReturnFalse()
        {
            var result = _service.ValidateInn("12345ABCDE");
            Assert.IsFalse(result.isValid);
            Assert.AreEqual("ИНН должен содержать только цифры.", result.error);
        }

        [TestMethod]
        public void ValidateInn_WrongLength_ShouldReturnFalse()
        {
            var result = _service.ValidateInn("12345678");
            Assert.IsFalse(result.isValid);
            Assert.IsTrue(result.error.Contains("ИНН должен содержать 10 (юр. лицо) или 12 (ИП) цифр"));
        }

        [TestMethod]
        public void ValidateInn_Valid10Digits_ShouldReturnTrue()
        {
           
            var validInn = "7707083893";
            var result = _service.ValidateInn(validInn);

            Assert.IsTrue(result.isValid);
            Assert.IsNull(result.error);
        }

        [TestMethod]
        public void ValidateInn_Invalid10DigitsCheckSum_ShouldReturnFalse()
        {
            var invalidCheckSumInn = "7707083894";
            var result = _service.ValidateInn(invalidCheckSumInn);

            Assert.IsFalse(result.isValid);
            Assert.AreEqual("Контрольная цифра ИНН не совпадает. Проверьте введённые данные.", result.error);
        }
    }
}