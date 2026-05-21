namespace WinFormsApp1.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ApplicationDbContext _db;

        public DeliveryService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<string> GetProductNames()
        {
            return _db.Products.Select(p => p.Name).Distinct().ToList();
        }
        public object GetProductSizes(string productName)
        {
            return _db.Products
                .Where(p => p.Name == productName)
                .Select(p => new { p.Article, p.Size })
                .ToList();
        }
        public void ProcessDelivery(List<DeliveryItemDto> items)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in items)
                    {
                        var product = _db.Products.FirstOrDefault(p => p.Article == item.Article);
                        if (product == null) continue;

                        _db.Supplies.Add(new Supply
                        {
                            ProductId = product.Id,
                            Quantity = item.Quantity,
                            PurchasePrice = item.PurchasePrice,
                            SellingPrice = item.SellingPrice,
                            ExpiryDate = item.ExpiryDate,
                            CurrencyAtSupply = CurrencyConverter.CurrentCurrency,
                            RateAtSupply = CurrencyConverter.CurrentRate,
                            SupplyDate = DateTime.UtcNow
                        });

                        product.CurrentStock += item.Quantity;
                    }
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
