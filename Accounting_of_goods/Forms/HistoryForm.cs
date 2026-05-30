namespace WinFormsApp1
{
    public partial class HistoryForm : Form
    {
        private readonly IHistoryService _historyService;

        public HistoryForm(IHistoryService historyService)
        {
            InitializeComponent();
            _historyService = historyService;
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            this.Text = "История отгрузок и списаний";
            
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.RowHeadersVisible = false;

            dtpStartDate.MaxDate = DateTime.Today;
            dtpEndDate.MaxDate = DateTime.Today;

            dtpStartDate.MinDate = new DateTime(2020, 1, 1);
            dtpEndDate.MinDate = new DateTime(2020, 1, 1);

            dtpStartDate.Value = DateTime.Today.AddMonths(-1);
            dtpEndDate.Value = DateTime.Today;

            LoadHistory();
            AddSearchIcon(txtSearchHistory);
        }

        private void LoadHistory()
        {
            DateTime start = dtpStartDate.Value.Date.ToUniversalTime();
            DateTime end = dtpEndDate.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

            string searchText = txtSearchHistory.Text.Trim().ToLower();

            var history = _historyService.GetShipmentHistory(start, end, searchText);

            dgvHistory.DataSource = null;
            dgvHistory.Columns.Clear();
            dgvHistory.AutoGenerateColumns = true;
            dgvHistory.DataSource = history;

            if (dgvHistory.Columns["Кол_во"] != null) dgvHistory.Columns["Кол_во"].HeaderText = "Кол-во";
            if (dgvHistory.Columns["Сумма"] != null) dgvHistory.Columns["Сумма"].HeaderText = "Сумма";
            if (dgvHistory.Columns["Прибыль"] != null) dgvHistory.Columns["Прибыль"].HeaderText = "Прибыль";
            if (dgvHistory.Columns["СуммаЧисло"] != null) dgvHistory.Columns["СуммаЧисло"].Visible = false;
            if (dgvHistory.Columns["ПрибыльЧисло"] != null) dgvHistory.Columns["ПрибыльЧисло"].Visible = false;
            if (dgvHistory.Columns["Валюта"] != null) dgvHistory.Columns["Валюта"].Visible = false;
            if (dgvHistory.Columns["Регион"] != null) dgvHistory.Columns["Регион"].Visible = false;

            var revenueByCurrency = new Dictionary<string, decimal>();
            var lossByCurrency = new Dictionary<string, decimal>();
            var profitByCurrency = new Dictionary<string, decimal>();

            foreach (dynamic h in history)
            {
                string currency = h.Валюта;
                string type = h.Тип;
                decimal amount = (decimal)h.СуммаЧисло;
                decimal profit = (decimal)h.ПрибыльЧисло;

                if (!revenueByCurrency.ContainsKey(currency)) revenueByCurrency[currency] = 0;
                if (!lossByCurrency.ContainsKey(currency)) lossByCurrency[currency] = 0;
                if (!profitByCurrency.ContainsKey(currency)) profitByCurrency[currency] = 0;

                if (type == "Отгрузка")
                {
                    revenueByCurrency[currency] += amount;
                }
                else if (type == "Списание")
                {
                    lossByCurrency[currency] += -amount;
                }

                profitByCurrency[currency] += profit;
            }

            string revenueStr = string.Join(" + ", revenueByCurrency.Select(kv => $"{kv.Value:N2} {kv.Key}"));
            string lossStr = string.Join(" + ", lossByCurrency.Select(kv => $"{kv.Value:N2} {kv.Key}"));
            string profitStr = string.Join(" + ", profitByCurrency.Select(kv => $"{kv.Value:N2} {kv.Key}"));

            lblTotalRevenue.Text = string.IsNullOrEmpty(revenueStr) ? "0,00" : revenueStr;
            lblTotalLoss.Text = string.IsNullOrEmpty(lossStr) ? "0,00" : lossStr;
            lblTotalProfit.Text = string.IsNullOrEmpty(profitStr) ? "0,00" : profitStr;
        }

        private void AddSearchIcon(TextBox tb)
        {
            Label searchIcon = new Label();
            searchIcon.Text = "🔍";
            searchIcon.AutoSize = true;
            searchIcon.BackColor = tb.BackColor;
            searchIcon.ForeColor = Color.Gray;
            searchIcon.Cursor = Cursors.IBeam;
            searchIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchIcon.Location = new Point(tb.Width - 25, 2);
            searchIcon.Click += (s, e) => tb.Focus();
            tb.Controls.Add(searchIcon);
        }

        private void txtSearchHistory_TextChanged(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value > DateTime.Today)
            {
                dtpStartDate.Value = DateTime.Today;
            }

            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }

            LoadHistory();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value > DateTime.Today)
            {
                dtpEndDate.Value = DateTime.Today;
            }

            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                dtpStartDate.Value = dtpEndDate.Value;
            }

            LoadHistory();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (sender == dtpStartDate)
            {
                if (dtpStartDate.Value > DateTime.Today)
                    dtpStartDate.Value = DateTime.Today;

                if (dtpStartDate.Value > dtpEndDate.Value)
                    dtpEndDate.Value = dtpStartDate.Value;
            }
            else if (sender == dtpEndDate)
            {
                if (dtpEndDate.Value > DateTime.Today)
                    dtpEndDate.Value = DateTime.Today;

                if (dtpEndDate.Value < dtpStartDate.Value)
                    dtpStartDate.Value = dtpEndDate.Value;
            }

            LoadHistory();
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (dgvHistory.Rows.Count == 0 || dgvHistory.Rows[0].IsNewRow)
            {
                MessageBox.Show("Нет данных для экспорта!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV файл (*.csv)|*.csv";
            sfd.FileName = $"История_отгрузок_{DateTime.Now:dd_MM_yyyy}.csv";
            sfd.Title = "Сохранить отчет как...";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                    {
                        string[] headers = new string[dgvHistory.Columns.Count];
                        for (int i = 0; i < dgvHistory.Columns.Count; i++)
                        {
                            headers[i] = dgvHistory.Columns[i].HeaderText;
                        }
                        sw.WriteLine(string.Join(";", headers));
                        foreach (DataGridViewRow row in dgvHistory.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string[] cells = new string[dgvHistory.Columns.Count];
                            for (int i = 0; i < dgvHistory.Columns.Count; i++)
                            {
                                string cellValue = row.Cells[i].Value?.ToString() ?? "";
                                cells[i] = cellValue.Replace(";", ",");
                            }
                            sw.WriteLine(string.Join(";", cells));
                        }
                    }

                    MessageBox.Show("Данные успешно экспортированы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
