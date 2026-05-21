namespace WinFormsApp1.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ApplicationDbContext _db;

        public HistoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<object> GetShipmentHistory(DateTime start, DateTime end, string searchText)
        {
            var shipmentsQuery = _db.Shipments
                .Include(s => s.Product)
                .Include(s => s.User)
                .Where(s => s.ShipmentDate >= start && s.ShipmentDate <= end)
                .AsQueryable();

            var writeOffsQuery = _db.WriteOffs
                .Include(w => w.Product)
                .Include(w => w.User)
                .Where(w => w.WriteOffDate >= start && w.WriteOffDate <= end)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                shipmentsQuery = shipmentsQuery.Where(s =>
                    s.User.Login.ToLower().Contains(searchText) ||
                    s.Product.Name.ToLower().Contains(searchText) ||
                    s.Product.Brand.ToLower().Contains(searchText)
                );

                writeOffsQuery = writeOffsQuery.Where(w =>
                    w.User.Login.ToLower().Contains(searchText) ||
                    w.Product.Name.ToLower().Contains(searchText) ||
                    w.Product.Brand.ToLower().Contains(searchText)
                );
            }

            var shipments = shipmentsQuery.ToList();
            var writeOffs = writeOffsQuery.ToList();

            var historyItems = new List<object>();

            string targetCurrency = Accounting_of_goods.CurrencyConverter.CurrentCurrency;

            decimal GetHistoricalRate(string ratesJson, decimal fallbackRate, string fallbackCurrency)
            {
                if (targetCurrency == "RUB") return 1m;
                if (string.IsNullOrEmpty(ratesJson) || ratesJson == "{}" || ratesJson == "{\"RUB\": 1}")
                {
                    if (targetCurrency == fallbackCurrency) return fallbackRate == 0 ? 1m : fallbackRate;
                    return Accounting_of_goods.CurrencyConverter.CurrentRate;
                }

                try
                {
                    using (JsonDocument doc = JsonDocument.Parse(ratesJson))
                    {
                        if (doc.RootElement.TryGetProperty(targetCurrency, out JsonElement el))
                            return el.GetDecimal();
                    }
                }
                catch { }

                return Accounting_of_goods.CurrencyConverter.CurrentRate;
            }

            historyItems.AddRange(shipments.Select(s => {
                decimal rate = GetHistoricalRate(s.RatesJson, s.RateAtShipment, s.CurrencyAtShipment);
                return (object)new
                {
                    Тип = "Отгрузка",
                    Дата = s.ShipmentDate.ToLocalTime(),
                    Сотрудник = s.User.Login,
                    Бренд = s.Product.Brand,
                    Товар = s.Product.Name,
                    Размер = s.Product.Size,
                    Кол_во = s.Quantity,
                    Получатель = s.Recipient,
                    Причина = "",
                    Сумма = $"{Math.Round(s.SellingPriceAtShipment * rate * s.Quantity, 2)} {targetCurrency}",
                    Прибыль = $"{Math.Round((s.SellingPriceAtShipment - s.Product.PurchasePrice) * s.Quantity * rate, 2)} {targetCurrency}",
                    СуммаЧисло = Math.Round(s.SellingPriceAtShipment * rate * s.Quantity, 2),
                    ПрибыльЧисло = Math.Round((s.SellingPriceAtShipment - s.Product.PurchasePrice) * s.Quantity * rate, 2),
                    Валюта = targetCurrency
                };
            }));

            historyItems.AddRange(writeOffs.Select(w => {
                decimal rate = GetHistoricalRate(w.RatesJson, w.RateAtWriteOff, w.CurrencyAtWriteOff);
                return (object)new
                {
                    Тип = "Списание",
                    Дата = w.WriteOffDate.ToLocalTime(),
                    Сотрудник = w.User.Login,
                    Бренд = w.Product.Brand,
                    Товар = w.Product.Name,
                    Размер = w.Product.Size,
                    Кол_во = w.Quantity,
                    Получатель = "",
                    Причина = w.Reason,
                    Сумма = $"{-Math.Round(w.Product.PurchasePrice * w.Quantity * rate, 2)} {targetCurrency}",
                    Прибыль = $"{-Math.Round(w.Product.PurchasePrice * w.Quantity * rate, 2)} {targetCurrency}",
                    СуммаЧисло = -Math.Round(w.Product.PurchasePrice * w.Quantity * rate, 2),
                    ПрибыльЧисло = -Math.Round(w.Product.PurchasePrice * w.Quantity * rate, 2),
                    Валюта = targetCurrency
                };
            }));

            return historyItems.OrderByDescending(h => ((dynamic)h).Дата).ToList();
        }
    }
}
