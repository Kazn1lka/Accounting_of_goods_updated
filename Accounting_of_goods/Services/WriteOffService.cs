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
            if (product == null)
                return null;
            return new { product.Article, product.CurrentStock };
        }

        public object GetProductByArticle(string article)
        {
            var product = _db.Products.FirstOrDefault(p => p.Article == article);
            if (product == null)
                return null;
            return new { product.Name, product.Size };
        }

        public List<ExpiredSupplyItem> GetExpiredItems()
        {
            var now = DateTime.UtcNow;
            var result = new List<ExpiredSupplyItem>();

            var expiredSupplies = _db.Supplies
                .Where(s => s.ExpiryDate != null && s.ExpiryDate < now && s.Quantity > 0)
                .Join(_db.Products,
                    s => s.ProductId,
                    p => p.Id,
                    (s, p) => new { Supply = s, Product = p })
                .OrderBy(x => x.Supply.ExpiryDate)
                .ToList();

            foreach (var row in expiredSupplies)
            {
                int daysExpired = (int)(now - row.Supply.ExpiryDate.Value).TotalDays;

                var item = new ExpiredSupplyItem
                {
                    SupplyId = row.Supply.Id,
                    ProductId = row.Product.Id,
                    Article = row.Product.Article,
                    ProductName = row.Product.Name,
                    Size = row.Product.Size,
                    Quantity = row.Supply.Quantity,
                    ExpiryDate = row.Supply.ExpiryDate.Value,
                    DaysExpired = daysExpired,
                    PurchasePrice = row.Product.PurchasePrice
                };
                result.Add(item);
            }

            return result;
        }

        public void ProcessWriteOffBySupply(int userId, int supplyId, int quantity, string reason)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var supply = _db.Supplies.FirstOrDefault(s => s.Id == supplyId);
                if (supply == null)
                    throw new Exception("Партия не найдена.");

                if (quantity > supply.Quantity)
                    throw new Exception("Количество для списания превышает остаток в партии.");

                var product = _db.Products.FirstOrDefault(p => p.Id == supply.ProductId);
                if (product == null)
                    throw new Exception("Товар не найден.");

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

                supply.Quantity -= quantity;

                if (product.CurrentStock >= quantity)
                    product.CurrentStock -= quantity;
                else
                    product.CurrentStock = 0;

                _db.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void ProcessWriteOff(int userId, string article, int quantity, string reason)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var product = _db.Products.FirstOrDefault(p => p.Article == article);
                if (product == null || product.CurrentStock < quantity)
                    throw new Exception("Недостаточно товара для списания.");

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
                    .OrderBy(s => s.ExpiryDate ?? DateTime.MaxValue)
                    .ToList();

                foreach (var supply in supplies)
                {
                    if (remaining <= 0)
                        break;

                    int deduct = Math.Min(supply.Quantity, remaining);
                    supply.Quantity -= deduct;
                    remaining -= deduct;
                }

                product.CurrentStock -= quantity;
                if (product.CurrentStock < 0)
                    product.CurrentStock = 0;

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
