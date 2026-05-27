namespace WinFormsApp1.Models
{
    public class HeatMapCell
    {
        public string Article { get; init; } = "";
        public string ProductName { get; init; } = "";
        public string Size { get; init; } = "";
        public string CategoryName { get; init; } = "";
        public int Quantity { get; init; }

        public int DaysUntilExpiry { get; init; }

        public int Shipments30Days { get; init; }

        public HeatCellColor Color { get; init; }
    }

    public enum HeatCellColor
    {
        Green,
        Yellow,
        Orange,
        Red
    }

    public enum HeatMapMode
    {
        Expiry,
        Turnover
    }
}
