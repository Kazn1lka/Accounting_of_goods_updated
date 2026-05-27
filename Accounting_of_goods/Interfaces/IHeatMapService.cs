namespace WinFormsApp1.Interfaces
{
    public interface IHeatMapService
    {
        List<HeatMapCell> GetCells(HeatMapSettings settings);
        int GetTotalPositions();
        int GetStaleCount(int thresholdDays);
    }
}
