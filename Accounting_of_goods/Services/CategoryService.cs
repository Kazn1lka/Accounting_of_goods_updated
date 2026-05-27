namespace WinFormsApp1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public object GetCategoriesForGrid()
        {
            return _db.Categories
                .Select(c => new { ID = c.Id, Название = c.Name })
                .ToList();
        }

        public bool CategoryExists(string name)
        {
            return _db.Categories.Any(c => c.Name.ToLower() == name.ToLower());
        }

        public void AddCategory(string name)
        {
            _db.Categories.Add(new Category { Name = name });
            _db.SaveChanges();
        }

        public bool HasProducts(int categoryId)
        {
            return _db.Products.Any(p => p.CategoryId == categoryId);
        }

        public void DeleteCategory(int categoryId)
        {
            var cat = _db.Categories.Find(categoryId);
            if (cat != null)
            {
                _db.Categories.Remove(cat);
                _db.SaveChanges();
            }
        }

        public void UpdateCategoryName(int categoryId, string newName)
        {
            var cat = _db.Categories.Find(categoryId);
            if (cat != null)
            {
                cat.Name = newName;
                _db.SaveChanges();
            }
        }
    }
}
