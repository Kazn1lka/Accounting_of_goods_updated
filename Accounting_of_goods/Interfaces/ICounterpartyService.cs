namespace WinFormsApp1.Interfaces
{
    public interface ICounterpartyService
    {
        (bool isValid, string error) ValidateInn(string inn);
        Task<CounterpartyInfo> CheckByInnAsync(string inn);
    }
}
