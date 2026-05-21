namespace WinFormsApp1.Interfaces
{
    public interface ICategoryService
    {
        object GetCategoriesForGrid();
        bool CategoryExists(string name);
        void AddCategory(string name);
        bool HasProducts(int categoryId);
        void DeleteCategory(int categoryId);
        void UpdateCategoryName(int categoryId, string newName);
    }
}
