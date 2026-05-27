namespace WinFormsApp1.Models
{
    public class HeatMapSettings
    {
        public bool Enabled { get; set; } = true;
        public bool ShowProductNames { get; set; } = true;

        public HeatMapMode Mode { get; set; } = HeatMapMode.Expiry;

        public int GreenThresholdDays { get; set; } = 90;
        public int YellowThresholdDays { get; set; } = 20;
        public int OrangeThresholdDays { get; set; } = 7;

        public int TurnoverGreenMin { get; set; } = 10;
        public int TurnoverYellowMin { get; set; } = 4;
        public int TurnoverOrangeMin { get; set; } = 1;

        public int AutoRefreshSeconds { get; set; } = 0;

        public static HeatMapSettings Current { get; } = new HeatMapSettings();
    }
}
