namespace WinFormsApp1
{
    public partial class TempThresholdForm : Form
    {
        private readonly IWeatherService _weatherService;
        private List<ProductTempThreshold> _thresholds = new List<ProductTempThreshold>();

        public TempThresholdForm()
        {
            InitializeComponent();
        }

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
            if (_weatherService == null)
                return;

            _thresholds = await _weatherService.GetThresholdsAsync();
            dgvThresholds.Rows.Clear();

            for (int i = 0; i < _thresholds.Count; i++)
            {
                var t = _thresholds[i];
                dgvThresholds.Rows.Add(t.CategoryId, t.CategoryName, t.MinSafeTemp, t.MaxSafeTemp);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvThresholds.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string idStr = row.Cells[0].Value?.ToString();
                string minStr = row.Cells[2].Value?.ToString();
                string maxStr = row.Cells[3].Value?.ToString();

                if (!int.TryParse(idStr, out int catId))
                    continue;

                if (minStr != null)
                    minStr = minStr.Replace(',', '.');

                if (maxStr != null)
                    maxStr = maxStr.Replace(',', '.');

                bool minOk = double.TryParse(minStr, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double minT);

                bool maxOk = double.TryParse(maxStr, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double maxT);

                if (!minOk || !maxOk)
                    continue;

                if (minT >= maxT)
                {
                    string catName = row.Cells[1].Value?.ToString();
                    MessageBox.Show(
                        $"Категория «{catName}»: Мин. температура должна быть меньше Макс.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool found = WeatherSettings.Thresholds.TryGetValue(catId, out var threshold);
                if (found)
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
                if (row.IsNewRow)
                    continue;

                row.Cells[2].Value = -5.0;
                row.Cells[3].Value = 35.0;
            }
        }
    }
}
