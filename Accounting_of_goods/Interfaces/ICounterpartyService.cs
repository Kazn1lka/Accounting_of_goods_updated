namespace WinFormsApp1.Interfaces
{
    public interface ICounterpartyService
    {
        /// <summary>Проверяет ИНН по контрольным цифрам (без сети).</summary>
        (bool isValid, string error) ValidateInn(string inn);

        /// <summary>Запрашивает данные о компании и проверяет её по чёрным спискам.</summary>
        Task<CounterpartyInfo> CheckByInnAsync(string inn);
    }
}
