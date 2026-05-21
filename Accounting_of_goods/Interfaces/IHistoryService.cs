namespace WinFormsApp1.Interfaces
{
    public interface IHistoryService
    {
        List<object> GetShipmentHistory(DateTime start, DateTime end, string searchText);
    }
}
