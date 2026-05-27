namespace WinFormsApp1.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ShipmentService> _logger;

        public ShipmentService(ApplicationDbContext db, ILogger<ShipmentService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public List<string> GetProductNames()
        {
            return _db.Products.Select(p => p.Name).Distinct().ToList();
        }

        public List<string> GetSizesForProduct(string productName)
        {
            return _db.Products
                .Where(p => p.Name == productName)
                .Select(p => p.Size)
                .ToList();
        }

        public object GetProductDetails(string productName, string size)
        {
            var product = _db.Products.FirstOrDefault(p => p.Name == productName && p.Size == size);
            if (product == null) return null;

            var lastPrice = _db.Supplies
                .Where(s => s.ProductId == product.Id && s.Quantity > 0)
                .OrderByDescending(s => s.SupplyDate)
                .Select(s => s.SellingPrice)
                .FirstOrDefault();

            return new { product.Article, product.CurrentStock, Price = lastPrice };
        }

        public void ProcessShipment(int userId, string recipient, string region, List<ShipmentItemDto> items)
        {
            _logger.LogInformation("Начало обработки отгрузки от пользователя {UserId} получателю {Recipient}. Регион: {Region}. Количество позиций: {Count}", userId, recipient, region, items.Count);

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var user = _db.Users.Find(userId);

                    foreach (var item in items)
                    {
                        var product = _db.Products.FirstOrDefault(p => p.Article == item.Article);
                        if (product == null)
                        {
                            _logger.LogWarning("Товар с артикулом {Article} не найден при попытке отгрузки", item.Article);
                            continue;
                        }

                        var shipment = new Shipment
                        {
                            UserId = userId,
                            ProductId = product.Id,
                            Quantity = item.Quantity,
                            SellingPriceAtShipment = item.Price,
                            Recipient = recipient,
                            Region = region,
                            ShipmentDate = DateTime.UtcNow,
                            TotalAmount = item.Quantity * item.Price,
                            CurrencyAtShipment = CurrencyConverter.CurrentCurrency,
                            RateAtShipment = CurrencyConverter.CurrentRate,
                            RatesJson = CurrencyConverter.CurrentRatesJson
                        };
                        _db.Shipments.Add(shipment);
                        _db.SaveChanges();

                        _logger.LogInformation("Создана отгрузка ID {ShipmentId} для товара {Article}, количество: {Quantity}", shipment.Id, product.Article, item.Quantity);

                        int remainingToShip = item.Quantity;

                        var supplies = _db.Supplies
                            .Where(s => s.ProductId == product.Id && s.Quantity > 0)
                            .OrderBy(s => s.ExpiryDate ?? DateTime.MaxValue)
                            .ToList();

                        foreach (var supply in supplies)
                        {
                            if (remainingToShip <= 0) break;

                            int deduct = Math.Min(supply.Quantity, remainingToShip);
                            supply.Quantity -= deduct;
                            remainingToShip -= deduct;

                            _db.ShipmentItems.Add(new ShipmentItem
                            {
                                ShipmentId = shipment.Id,
                                ProductId = product.Id,
                                Quantity = deduct,
                                Price = item.Price
                            });
                        }

                        product.CurrentStock -= item.Quantity;
                        if (product.CurrentStock < 0) product.CurrentStock = 0;
                    }

                    _db.SaveChanges();
                    transaction.Commit();
                    _logger.LogInformation("Транзакция отгрузки для получателя {Recipient} успешно завершена", recipient);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при оформлении отгрузки для пользователя {UserId} получателю {Recipient}", userId, recipient);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
