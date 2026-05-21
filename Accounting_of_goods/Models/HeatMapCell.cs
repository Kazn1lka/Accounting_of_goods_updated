namespace WinFormsApp1.Models
{
    /// <summary>Данные одной ячейки тепловой карты склада.</summary>
    public class HeatMapCell
    {
        public string Article       { get; init; } = "";
        public string ProductName   { get; init; } = "";
        public string Size          { get; init; } = "";
        public string CategoryName  { get; init; } = "";
        public int    Quantity      { get; init; }

        /// <summary>Дней до истечения срока годности (режим «По сроку годности»).</summary>
        public int DaysUntilExpiry { get; init; }

        /// <summary>Кол-во отгрузок за последние 30 дней (режим «По оборачиваемости»).</summary>
        public int Shipments30Days { get; init; }

        /// <summary>Итоговый цвет ячейки, рассчитанный сервисом.</summary>
        public HeatCellColor Color { get; init; }
    }

    public enum HeatCellColor
    {
        /// <summary>Зелёный — товар свежий / быстро движется.</summary>
        Green,
        /// <summary>Жёлтый — нормальное состояние.</summary>
        Yellow,
        /// <summary>Оранжевый — приближается к истечению / слабое движение.</summary>
        Orange,
        /// <summary>Красный — критично (истекает / залежался).</summary>
        Red
    }

    public enum HeatMapMode
    {
        /// <summary>Цвет зависит от оставшегося срока годности.</summary>
        Expiry,
        /// <summary>Цвет зависит от скорости оборачиваемости (кол-во отгрузок за 30 дней).</summary>
        Turnover
    }
}
