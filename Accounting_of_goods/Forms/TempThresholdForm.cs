namespace WinFormsApp1
{
    public partial class TempThresholdForm : Form
    {
        private readonly IWeatherService _weatherService;
        private List<ProductTempThreshold> _thresholds = new();

        public TempThresholdForm() { InitializeComponent(); }

        public TempThresholdForm(IWeatherService weatherService) : this()
        {
            _weatherService = weatherService;
        }

        private async void TempThresholdForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            if (_weatherService == null) return;

            _thresholds = await _weatherService.GetThresholdsAsync();
            dgvThresholds.Rows.Clear();

            foreach (var t in _thresholds)
            {
                dgvThresholds.Rows.Add(
                    t.CategoryId,
                    t.CategoryName,
                    t.MinSafeTemp,
                    t.MaxSafeTemp
                );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvThresholds.Rows)
            {
                if (row.IsNewRow) continue;

                if (!int.TryParse(row.Cells[0].Value?.ToString(), out int catId)) continue;
                if (!double.TryParse(row.Cells[2].Value?.ToString()?.Replace(',', '.'), System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double minT)) continue;
                if (!double.TryParse(row.Cells[3].Value?.ToString()?.Replace(',', '.'), System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double maxT)) continue;

                if (minT >= maxT)
                {
                    MessageBox.Show(
                        $"Категория «{row.Cells[1].Value}»: Мин. температура должна быть меньше Макс.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (WeatherSettings.Thresholds.TryGetValue(catId, out var threshold))
                {
                    threshold.MinSafeTemp = minT;
                    threshold.MaxSafeTemp = maxT;
                }
            }

            MessageBox.Show("Пороги сохранены!", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvThresholds.Rows)
            {
                if (row.IsNewRow) continue;
                row.Cells[2].Value = -5.0;
                row.Cells[3].Value = 35.0;
            }
        }
    }
}
