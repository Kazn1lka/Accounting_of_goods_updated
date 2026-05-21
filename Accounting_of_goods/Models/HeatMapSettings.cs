namespace WinFormsApp1.Models
{
    /// <summary>Настройки модуля тепловой карты склада.</summary>
    public class HeatMapSettings
    {
        // ── Активация ─────────────────────────────────────────────────────────────
        public bool Enabled          { get; set; } = true;
        public bool ShowProductNames { get; set; } = true;

        // ── Режим отображения ──────────────────────────────────────────────────────
        public HeatMapMode Mode { get; set; } = HeatMapMode.Expiry;

        // ── Пороговые значения для режима «По сроку годности» (дни) ───────────────
        /// <summary>Более X дней → Зелёный.</summary>
        public int GreenThresholdDays  { get; set; } = 90;

        /// <summary>От X до GreenThresholdDays дней → Жёлтый.</summary>
        public int YellowThresholdDays { get; set; } = 20;

        /// <summary>От X до YellowThresholdDays дней → Оранжевый.</summary>
        public int OrangeThresholdDays { get; set; } = 7;

        // ── Пороговые значения для режима «По оборачиваемости» (отгрузок за 30 дн) ─
        /// <summary>Более X отгрузок → Зелёный.</summary>
        public int TurnoverGreenMin  { get; set; } = 10;

        /// <summary>От X до TurnoverGreenMin → Жёлтый.</summary>
        public int TurnoverYellowMin { get; set; } = 4;

        /// <summary>От X до TurnoverYellowMin → Оранжевый.</summary>
        public int TurnoverOrangeMin { get; set; } = 1;

        // ── Автообновление ──────────────────────────────────────────────────────────
        /// <summary>Интервал в секундах. 0 = отключено.</summary>
        public int AutoRefreshSeconds { get; set; } = 0;

        // ── Глобальный singleton ──────────────────────────────────────────────────
        public static HeatMapSettings Current { get; } = new HeatMapSettings();
    }
}
