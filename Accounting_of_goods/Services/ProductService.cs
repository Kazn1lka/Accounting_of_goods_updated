namespace WinFormsApp1.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public object GetProductsForGrid(string searchText)
        {
            var query = _db.Supplies
                .Include(s => s.Product).ThenInclude(p => p.Category)
                .Where(s => s.Quantity > 0)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(s =>
                    s.Product.Name.ToLower().Contains(searchText) ||
                    s.Product.Article.ToLower().Contains(searchText) ||
                    s.Product.Brand.ToLower().Contains(searchText));
            }

            return query.Select(s => new
            {
                ID_Поставки = s.Id,
                Артикул = s.Product.Article,
                Бренд = s.Product.Brand,
                Название = s.Product.Name,
                Категория = s.Product.Category != null ? s.Product.Category.Name : "Без категории",
                Размер = s.Product.Size,
                Цена = CurrencyConverter.ConvertPrice(s.SellingPrice),
                Остаток = s.Quantity,
                Срок_Годности = s.ExpiryDate.HasValue ? s.ExpiryDate.Value.ToLocalTime().ToShortDateString() : "-"
            }).ToList();
        }

        public void DeleteSupply(int supplyId)
        {
            var supply = _db.Supplies.Find(supplyId);
            if (supply != null)
            {
                var product = _db.Products.Find(supply.ProductId);
                if (product != null)
                {
                    product.CurrentStock -= supply.Quantity;
                    if (product.CurrentStock < 0) product.CurrentStock = 0;
                }

                _db.Supplies.Remove(supply);
                _db.SaveChanges();
            }
        }
    }
}
