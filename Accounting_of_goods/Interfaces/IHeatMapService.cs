namespace WinFormsApp1.Interfaces
{
    public interface IHeatMapService
    {
        /// <summary>Возвращает список ячеек тепловой карты по выбранному режиму и настройкам.</summary>
        List<HeatMapCell> GetCells(HeatMapSettings settings);

        /// <summary>Кол-во уникальных позиций (артикул+размер) с остатком > 0.</summary>
        int GetTotalPositions();

        /// <summary>Кол-во позиций, у которых до истечения срока годности осталось меньше X дней.</summary>
        int GetStaleCount(int thresholdDays);
    }
}
