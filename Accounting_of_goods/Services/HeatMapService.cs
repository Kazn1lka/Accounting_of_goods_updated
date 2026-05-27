namespace WinFormsApp1.Services
{
    public class HeatMapService : IHeatMapService
    {
        private readonly ApplicationDbContext _db;

        public HeatMapService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<HeatMapCell> GetCells(HeatMapSettings settings)
        {
            return settings.Mode == HeatMapMode.Expiry
                ? BuildExpiryMap(settings)
                : BuildTurnoverMap(settings);
        }
        private List<HeatMapCell> BuildExpiryMap(HeatMapSettings s)
        {
            var today = DateTime.UtcNow.Date;

            var supplies = _db.Supplies
                .Where(sup => sup.Quantity > 0)
                .Include(sup => sup.Product)
                    .ThenInclude(p => p.Category)
                .ToList();

            var grouped = supplies
                .GroupBy(s2 => new { s2.Product.Article, s2.Product.Size })
                .Select(g =>
                {
                    var firstExpiry = g.OrderBy(x => x.ExpiryDate ?? DateTime.MaxValue).First();
                    int daysLeft = firstExpiry.ExpiryDate.HasValue
                        ? (firstExpiry.ExpiryDate.Value.Date - today).Days
                        : 9999;

                    return new HeatMapCell
                    {
                        Article      = firstExpiry.Product.Article,
                        ProductName  = firstExpiry.Product.Name,
                        Size         = firstExpiry.Product.Size,
                        CategoryName = firstExpiry.Product.Category?.Name ?? "",
                        Quantity     = g.Sum(x => x.Quantity),
                        DaysUntilExpiry = daysLeft,
                        Color        = ExpiryColor(daysLeft, s)
                    };
                })
                .OrderBy(c => c.DaysUntilExpiry)
                .ToList();

            return grouped;
        }

        private static HeatCellColor ExpiryColor(int daysLeft, HeatMapSettings s)
        {
            if (daysLeft >= s.GreenThresholdDays)  return HeatCellColor.Green;
            if (daysLeft >= s.YellowThresholdDays) return HeatCellColor.Yellow;
            if (daysLeft >= s.OrangeThresholdDays) return HeatCellColor.Orange;
            return HeatCellColor.Red;
        }


        private List<HeatMapCell> BuildTurnoverMap(HeatMapSettings s)
        {
            var since = DateTime.UtcNow.AddDays(-30);

            var shipmentCounts = _db.Shipments
                .Where(sh => sh.ShipmentDate >= since)
                .GroupBy(sh => sh.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Sum(x => x.Quantity) })
                .ToDictionary(x => x.ProductId, x => x.Count);

            var supplies = _db.Supplies
                .Where(sup => sup.Quantity > 0)
                .Include(sup => sup.Product)
                    .ThenInclude(p => p.Category)
                .ToList();

            var grouped = supplies
                .GroupBy(sup => new { sup.Product.Article, sup.Product.Size })
                .Select(g =>
                {
                    var first    = g.First();
                    int pid      = first.ProductId;
                    int shipped  = shipmentCounts.TryGetValue(pid, out var cnt) ? cnt : 0;

                    return new HeatMapCell
                    {
                        Article       = first.Product.Article,
                        ProductName   = first.Product.Name,
                        Size          = first.Product.Size,
                        CategoryName  = first.Product.Category?.Name ?? "",
                        Quantity      = g.Sum(x => x.Quantity),
                        Shipments30Days = shipped,
                        Color         = TurnoverColor(shipped, s)
                    };
                })
                .OrderBy(c => c.Shipments30Days)
                .ToList();

            return grouped;
        }

        private static HeatCellColor TurnoverColor(int shipped, HeatMapSettings s)
        {
            if (shipped >= s.TurnoverGreenMin)  return HeatCellColor.Green;
            if (shipped >= s.TurnoverYellowMin) return HeatCellColor.Yellow;
            if (shipped >= s.TurnoverOrangeMin) return HeatCellColor.Orange;
            return HeatCellColor.Red;
        }

        public int GetTotalPositions()
        {
            return _db.Supplies
                .Where(s => s.Quantity > 0)
                .Select(s => new { s.Product.Article, s.Product.Size })
                .Distinct()
                .Count();
        }

        public int GetStaleCount(int thresholdDays)
        {
            var today = DateTime.UtcNow.Date;
            var cutoff = today.AddDays(thresholdDays);

            return _db.Supplies
                .Where(s => s.Quantity > 0 && s.ExpiryDate.HasValue && s.ExpiryDate.Value.Date <= cutoff)
                .Select(s => new { s.Product.Article, s.Product.Size })
                .Distinct()
                .Count();
        }
    }
}
