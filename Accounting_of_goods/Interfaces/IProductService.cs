namespace WinFormsApp1.Interfaces
{
    public interface IProductService
    {
        object GetProductsForGrid(string searchText);
        void DeleteSupply(int supplyId);
    }
}
