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

        public void ProcessShipment(int userId, string recipient, List<ShipmentItemDto> items)
        {
            _logger.LogInformation("РќР°С‡Р°Р»Рѕ РѕР±СЂР°Р±РѕС‚РєРё РѕС‚РіСЂСѓР·РєРё РѕС‚ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏ {UserId} РїРѕР»СѓС‡Р°С‚РµР»СЋ {Recipient}. РљРѕР»РёС‡РµСЃС‚РІРѕ РїРѕР·РёС†РёР№: {Count}", userId, recipient, items.Count);

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
                            _logger.LogWarning("РўРѕРІР°СЂ СЃ Р°СЂС‚РёРєСѓР»РѕРј {Article} РЅРµ РЅР°Р№РґРµРЅ РїСЂРё РїРѕРїС‹С‚РєРµ РѕС‚РіСЂСѓР·РєРё", item.Article);
                            continue;
                        }

                        var shipment = new Shipment
                        {
                            UserId = userId,
                            ProductId = product.Id,
                            Quantity = item.Quantity,
                            SellingPriceAtShipment = item.Price,
                            Recipient = recipient,
                            ShipmentDate = DateTime.UtcNow,
                            TotalAmount = item.Quantity * item.Price,
                            CurrencyAtShipment = CurrencyConverter.CurrentCurrency,
                            RateAtShipment = CurrencyConverter.CurrentRate,
                            RatesJson = CurrencyConverter.CurrentRatesJson
                        };
                        _db.Shipments.Add(shipment);
                        _db.SaveChanges();

                        _logger.LogInformation("РЎРѕР·РґР°РЅР° РѕС‚РіСЂСѓР·РєР° ID {ShipmentId} РґР»СЏ С‚РѕРІР°СЂР° {Article}, РєРѕР»РёС‡РµСЃС‚РІРѕ: {Quantity}", shipment.Id, product.Article, item.Quantity);

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
                    _logger.LogInformation("РўСЂР°РЅР·Р°РєС†РёСЏ РѕС‚РіСЂСѓР·РєРё РґР»СЏ РїРѕР»СѓС‡Р°С‚РµР»СЏ {Recipient} СѓСЃРїРµС€РЅРѕ Р·Р°РІРµСЂС€РµРЅР°", recipient);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "РћС€РёР±РєР° РїСЂРё РѕС„РѕСЂРјР»РµРЅРёРё РѕС‚РіСЂСѓР·РєРё РґР»СЏ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏ {UserId} РїРѕР»СѓС‡Р°С‚РµР»СЋ {Recipient}", userId, recipient);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
