namespace WinFormsApp1.Services
{
    public class WriteOffService : IWriteOffService
    {
        private readonly ApplicationDbContext _db;

        public WriteOffService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<string> GetProductNames()
        {
            return _db.Products.Select(p => p.Name).Distinct().ToList();
        }

        public List<string> GetSizesForProduct(string productName)
        {
            return _db.Products.Where(p => p.Name == productName).Select(p => p.Size).ToList();
        }

        public object GetProductDetails(string productName, string size)
        {
            var product = _db.Products.FirstOrDefault(p => p.Name == productName && p.Size == size);
            if (product == null) return null;
            return new { product.Article, product.CurrentStock };
        }

        public object GetProductByArticle(string article)
        {
            var product = _db.Products.FirstOrDefault(p => p.Article == article);
            if (product == null) return null;
            return new { product.Name, product.Size };
        }

        public void ProcessWriteOff(int userId, string article, int quantity, string reason)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var product = _db.Products.FirstOrDefault(p => p.Article == article);
                    if (product == null || product.CurrentStock < quantity)
                        throw new Exception("РќРµРґРѕСЃС‚Р°С‚РѕС‡РЅРѕ С‚РѕРІР°СЂР° РґР»СЏ СЃРїРёСЃР°РЅРёСЏ.");

                    var writeOff = new WriteOff
                    {
                        ProductId = product.Id,
                        UserId = userId,
                        Quantity = quantity,
                        Reason = reason,
                        WriteOffDate = DateTime.UtcNow,
                        CurrencyAtWriteOff = Accounting_of_goods.CurrencyConverter.CurrentCurrency,
                        RateAtWriteOff = Accounting_of_goods.CurrencyConverter.CurrentRate,
                        RatesJson = Accounting_of_goods.CurrencyConverter.CurrentRatesJson
                    };
                    _db.WriteOffs.Add(writeOff);

                    int remaining = quantity;
                    var supplies = _db.Supplies
                        .Where(s => s.ProductId == product.Id && s.Quantity > 0)
                        .OrderBy(s => s.ExpiryDate ?? DateTime.MaxValue).ToList();

                    foreach (var supply in supplies)
                    {
                        if (remaining <= 0) break;
                        int deduct = Math.Min(supply.Quantity, remaining);
                        supply.Quantity -= deduct;
                        remaining -= deduct;
                    }

                    product.CurrentStock -= quantity;
                    if (product.CurrentStock < 0) product.CurrentStock = 0;

                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
