namespace WinFormsApp1
{
    public partial class WeatherForm : Form
    {
        private readonly IWeatherService _weatherService;
        private WeatherForecastResult _lastForecast;

        public WeatherForm() { InitializeComponent(); }

        public WeatherForm(IWeatherService weatherService) : this()
        {
            _weatherService = weatherService;
        }

        private async void WeatherForm_Load(object sender, EventArgs e)
        {
            if (_weatherService != null)
                await _weatherService.LoadCategoriesAsync();
        }

        private async void btnGetForecast_Click(object sender, EventArgs e)
        {
            string city = txtCity.Text.Trim();
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Введите название города.", "Геолокация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetLoading(true);
            try
            {
                _lastForecast = await _weatherService.GetForecastAsync(city);
                DisplayForecast(_lastForecast);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void SetLoading(bool isLoading)
        {
            btnGetForecast.Enabled = !isLoading;
            btnGetForecast.Text    = isLoading ? "Загрузка…" : "Обновить прогноз";
            if (isLoading)
            {
                pnlForecast.Visible    = false;
                pnlRec.Visible         = false;
            }
        }

        private void DisplayForecast(WeatherForecastResult r)
        {
            if (!r.Success)
            {
                MessageBox.Show(r.ErrorMsg, "Ошибка прогноза",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblCity.Text = $"📍 {r.CityName}  ({r.Latitude:F2}° с.ш., {r.Longitude:F2}° в.д.)";

            SetDayPanel(pnlDay0, lblDay0Label, lblDay0Temp, r.Days.Count > 0 ? r.Days[0] : null);
            SetDayPanel(pnlDay1, lblDay1Label, lblDay1Temp, r.Days.Count > 1 ? r.Days[1] : null);
            SetDayPanel(pnlDay2, lblDay2Label, lblDay2Temp, r.Days.Count > 2 ? r.Days[2] : null);

            var recs = _weatherService.BuildRecommendations(r);
            lstRecommendations.Items.Clear();
            foreach (var rec in recs)
                lstRecommendations.Items.Add(rec);

            pnlForecast.Visible = true;
            pnlRec.Visible      = true;
        }

        private void SetDayPanel(Panel pnl, Label lblLabel, Label lblTemp, DayForecast day)
        {
            if (day == null) { pnl.Visible = false; return; }

            pnl.Visible   = true;
            lblLabel.Text = day.Label;
            lblTemp.Text  = $"{day.TempMax:+0.#;-0.#;0}° / {day.TempMin:+0.#;-0.#;0}°";

            double absMin = day.TempMin;
            double absMax = day.TempMax;
            bool isFrost  = absMin < WeatherSettings.GlobalFrostThreshold;
            bool isHeat   = absMax > WeatherSettings.GlobalHeatThreshold;

            bool isCatWarn = WeatherSettings.Thresholds.Values
                .Any(t => absMin < t.MinSafeTemp || absMax > t.MaxSafeTemp);

            if (isFrost || isHeat)
            {
                pnl.BackColor    = Color.FromArgb(229, 57, 53);
                lblTemp.ForeColor = Color.White;
                lblLabel.ForeColor = Color.White;
            }
            else if (isCatWarn)
            {
                pnl.BackColor    = Color.FromArgb(251, 140, 0);
                lblTemp.ForeColor = Color.White;
                lblLabel.ForeColor = Color.White;
            }
            else
            {
                pnl.BackColor    = Color.FromArgb(56, 142, 60);
                lblTemp.ForeColor = Color.White;
                lblLabel.ForeColor = Color.White;
            }
        }

        private async void btnThresholds_Click(object sender, EventArgs e)
        {
            using var dlg = new TempThresholdForm(_weatherService);
            dlg.ShowDialog(this);

            if (_lastForecast?.Success == true)
                DisplayForecast(_lastForecast);
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnGetForecast_Click(sender, e);
        }
    }
}
