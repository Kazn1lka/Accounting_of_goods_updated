namespace WinFormsApp1
{
    public partial class HeatMapForm : Form
    {
        private readonly IHeatMapService _heatMapService;

        private List<HeatMapCell> _cells = new();
        private System.Windows.Forms.Timer _autoRefreshTimer;

        // Геометрия ячейки
        private const int CellW   = 185;
        private const int CellH   = 160;
        private const int CellGap = 10;
        private const int Radius  = 8;

        // Цвета
        private static readonly Color ColorGreen  = Color.FromArgb(56,  142, 60);
        private static readonly Color ColorYellow = Color.FromArgb(230, 162, 0);
        private static readonly Color ColorOrange = Color.FromArgb(220, 95,  0);
        private static readonly Color ColorRed    = Color.FromArgb(198, 40,  40);
        private static readonly Color ColorEmpty  = Color.FromArgb(160, 160, 160);

        // Осветлённый вариант для шапки ячейки
        private static Color Lighter(Color c, int amt = 40)
            => Color.FromArgb(c.A,
                Math.Min(255, c.R + amt),
                Math.Min(255, c.G + amt),
                Math.Min(255, c.B + amt));

        /// <summary>Конструктор для Visual Studio Designer.</summary>
        public HeatMapForm() { InitializeComponent(); }

        public HeatMapForm(IHeatMapService heatMapService) : this()
        {
            _heatMapService = heatMapService;
        }

        // ── Load ──────────────────────────────────────────────────────────────────

        private void HeatMapForm_Load(object sender, EventArgs e)
        {
            cmbMode.SelectedIndex = 0;
            RefreshMap();
        }

        // ── Обновление карты ──────────────────────────────────────────────────────

        private void RefreshMap()
        {
            if (_heatMapService == null) return;

            var settings = HeatMapSettings.Current;
            _cells = _heatMapService.GetCells(settings);

            UpdateStats(settings);
            pnlMap.Invalidate();
            ConfigureAutoRefresh(settings.AutoRefreshSeconds);
        }

        private void UpdateStats(HeatMapSettings settings)
        {
            int total = _heatMapService.GetTotalPositions();
            int stale = _heatMapService.GetStaleCount(settings.OrangeThresholdDays);

            lblTotal.Text = $"Всего позиций: {total}";
            lblStale.Text = $"Истекает ≤ {settings.OrangeThresholdDays} дн.: {stale}";
        }

        // ── Автообновление ────────────────────────────────────────────────────────

        private void ConfigureAutoRefresh(int seconds)
        {
            _autoRefreshTimer?.Stop();
            _autoRefreshTimer?.Dispose();

            if (seconds <= 0) return;

            _autoRefreshTimer          = new System.Windows.Forms.Timer();
            _autoRefreshTimer.Interval = seconds * 1000;
            _autoRefreshTimer.Tick    += (s, e) => RefreshMap();
            _autoRefreshTimer.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _autoRefreshTimer?.Stop();
            _autoRefreshTimer?.Dispose();
            base.OnFormClosed(e);
        }

        // ── Рисование тепловой карты (GDI+) ──────────────────────────────────────

        private void pnlMap_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(pnlMap.BackColor);

            if (_cells.Count == 0)
            {
                using var emptyFont = new Font("Arial", 11F);
                g.DrawString("Нет данных для отображения", emptyFont, Brushes.Gray, 20, 20);
                return;
            }

            int availW = pnlMap.ClientSize.Width - CellGap;
            int cols   = Math.Max(1, availW / (CellW + CellGap));
            int rows   = (int)Math.Ceiling((double)_cells.Count / cols);

            int totalH = CellGap + rows * (CellH + CellGap);
            if (pnlMap.AutoScrollMinSize.Height != totalH)
                pnlMap.AutoScrollMinSize = new Size(0, totalH);

            int ox = pnlMap.AutoScrollPosition.X;
            int oy = pnlMap.AutoScrollPosition.Y;

            var showNames = HeatMapSettings.Current.ShowProductNames;
            var mode      = HeatMapSettings.Current.Mode;

            using var fontArticle = new Font("Arial", 9F,  FontStyle.Bold);
            using var fontName    = new Font("Arial", 8F,  FontStyle.Regular);
            using var fontMeta    = new Font("Arial", 8F,  FontStyle.Regular);
            using var fontDays    = new Font("Arial", 8.5F, FontStyle.Bold);
            var sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter };

            // Измеряем реальную высоту строк для текущего DPI
            float lhA = g.MeasureString("Wgj", fontArticle).Height;
            float lhN = g.MeasureString("Wgj", fontName).Height;
            float lhM = g.MeasureString("Wgj", fontMeta).Height;
            float lhD = g.MeasureString("Wgj", fontDays).Height;

            for (int i = 0; i < _cells.Count; i++)
            {
                int col = i % cols;
                int row = i / cols;
                int x   = CellGap + col * (CellW + CellGap) + ox;
                int y   = CellGap + row * (CellH + CellGap) + oy;

                var cell = _cells[i];
                var bg   = GetCellColor(cell.Color);

                // Фон
                using var bgBrush = new SolidBrush(bg);
                g.FillRectangle(bgBrush, x, y, CellW, CellH);

                // Рамка
                g.DrawRectangle(Pens.White, x, y, CellW - 1, CellH - 1);

                float py = y + 5;
                float px = x + 6;
                float pw = CellW - 12;

                // Артикул
                g.DrawString(cell.Article, fontArticle, Brushes.White,
                    new RectangleF(px, py, pw, lhA + 2), sf);
                py += lhA + 4;

                // Название
                if (showNames)
                {
                    g.DrawString(cell.ProductName, fontName, Brushes.White,
                        new RectangleF(px, py, pw, lhN + 2), sf);
                    py += lhN + 3;
                }

                // Размер
                g.DrawString($"Разм: {cell.Size}", fontMeta, Brushes.White,
                    new RectangleF(px, py, pw, lhM + 2), sf);
                py += lhM + 3;

                // Кол-во
                g.DrawString($"Кол-во: {cell.Quantity} шт.", fontMeta, Brushes.White,
                    new RectangleF(px, py, pw, lhM + 2), sf);
                py += lhM + 3;

                // Срок / оборачиваемость
                string bottomLine = mode == HeatMapMode.Expiry
                    ? (cell.DaysUntilExpiry >= 9999 ? "∞ дн." : $"{cell.DaysUntilExpiry} дн.")
                    : $"{cell.Shipments30Days} отгр/30д";

                g.DrawString(bottomLine, fontDays, Brushes.White,
                    new RectangleF(px, py, pw, lhD + 2), sf);
            }
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle r, int rad)
        {
            var p = new System.Drawing.Drawing2D.GraphicsPath();
            p.AddArc(r.X,             r.Y,              rad * 2, rad * 2, 180, 90);
            p.AddArc(r.Right - rad*2, r.Y,              rad * 2, rad * 2, 270, 90);
            p.AddArc(r.Right - rad*2, r.Bottom - rad*2, rad * 2, rad * 2,   0, 90);
            p.AddArc(r.X,             r.Bottom - rad*2, rad * 2, rad * 2,  90, 90);
            p.CloseFigure();
            return p;
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRectTop(Rectangle r, int rad)
        {
            var p = new System.Drawing.Drawing2D.GraphicsPath();
            p.AddArc(r.X,             r.Y, rad * 2, rad * 2, 180, 90);
            p.AddArc(r.Right - rad*2, r.Y, rad * 2, rad * 2, 270, 90);
            p.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
            p.CloseFigure();
            return p;
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRectBottom(Rectangle r, int rad)
        {
            var p = new System.Drawing.Drawing2D.GraphicsPath();
            p.AddLine(r.X, r.Y, r.Right, r.Y);
            p.AddArc(r.Right - rad*2, r.Bottom - rad*2, rad*2, rad*2, 0,  90);
            p.AddArc(r.X,             r.Bottom - rad*2, rad*2, rad*2, 90, 90);
            p.CloseFigure();
            return p;
        }

        private static Color GetCellColor(HeatCellColor c) => c switch
        {
            HeatCellColor.Green  => ColorGreen,
            HeatCellColor.Yellow => ColorYellow,
            HeatCellColor.Orange => ColorOrange,
            HeatCellColor.Red    => ColorRed,
            _                    => ColorEmpty
        };

        // ── Фильтры ───────────────────────────────────────────────────────────────

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            HeatMapSettings.Current.Mode = cmbMode.SelectedIndex == 0
                ? HeatMapMode.Expiry
                : HeatMapMode.Turnover;
            RefreshMap();
        }

        private void pnlMap_Resize(object sender, EventArgs e) => pnlMap.Invalidate();

        private void btnRefresh_Click(object sender, EventArgs e) => RefreshMap();

        // ── Настройки ─────────────────────────────────────────────────────────────

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using var dlg = new HeatMapSettingsForm(HeatMapSettings.Current);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // Синхронизируем режим с комбо
                cmbMode.SelectedIndex = HeatMapSettings.Current.Mode == HeatMapMode.Expiry ? 0 : 1;
                RefreshMap();
            }
        }

        // ── Экспорт отчёта ────────────────────────────────────────────────────────

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_cells.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter   = "CSV файл|*.csv",
                FileName = $"Тепловая_карта_{DateTime.Now:dd_MM_yyyy}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Артикул;Название;Размер;Категория;Кол-во;Дней до истечения;Отгрузок за 30 дн;Цвет");
            foreach (var c in _cells)
            {
                string days = c.DaysUntilExpiry >= 9999 ? "∞" : c.DaysUntilExpiry.ToString();
                sb.AppendLine($"{c.Article};{c.ProductName};{c.Size};{c.CategoryName};{c.Quantity};{days};{c.Shipments30Days};{c.Color}");
            }

            System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
            MessageBox.Show("Отчёт сохранён!", "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Тултип по наведению ───────────────────────────────────────────────────

        private void pnlMap_MouseMove(object sender, MouseEventArgs e)
        {
            var cell = HitTest(e.Location);
            if (cell == null)
            {
                toolTip.SetToolTip(pnlMap, "");
                return;
            }

            string tip = HeatMapSettings.Current.Mode == HeatMapMode.Expiry
                ? $"{cell.ProductName} [{cell.Article}]\nРазмер: {cell.Size}\nОстаток: {cell.Quantity} шт.\nДней до истечения: {(cell.DaysUntilExpiry >= 9999 ? "∞" : cell.DaysUntilExpiry.ToString())}"
                : $"{cell.ProductName} [{cell.Article}]\nРазмер: {cell.Size}\nОстаток: {cell.Quantity} шт.\nОтгрузок за 30 дн.: {cell.Shipments30Days}";

            toolTip.SetToolTip(pnlMap, tip);
        }

        private HeatMapCell HitTest(Point mouse)
        {
            int availW = pnlMap.ClientSize.Width - CellGap;
            int cols   = Math.Max(1, availW / (CellW + CellGap));
            int ox     = pnlMap.AutoScrollPosition.X;
            int oy     = pnlMap.AutoScrollPosition.Y;

            for (int i = 0; i < _cells.Count; i++)
            {
                int col = i % cols;
                int row = i / cols;
                int x   = CellGap + col * (CellW + CellGap) + ox;
                int y   = CellGap + row * (CellH + CellGap) + oy;

                if (mouse.X >= x && mouse.X <= x + CellW &&
                    mouse.Y >= y && mouse.Y <= y + CellH)
                    return _cells[i];
            }
            return null;
        }
    }
}
