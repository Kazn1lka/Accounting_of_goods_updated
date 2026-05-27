namespace WinFormsApp1.Interfaces
{
    public interface IWriteOffService
    {
        List<string> GetProductNames();
        List<string> GetSizesForProduct(string productName);
        object GetProductDetails(string productName, string size);
        object GetProductByArticle(string article);
        void ProcessWriteOff(int userId, string article, int quantity, string reason);
        List<ExpiredSupplyItem> GetExpiredItems();
        void ProcessWriteOffBySupply(int userId, int supplyId, int quantity, string reason);
    }
}
