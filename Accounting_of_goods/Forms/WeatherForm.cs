namespace WinFormsApp1
{
    public partial class WeatherForm : Form
    {
        private readonly IWeatherService _weatherService;
        private WeatherForecastResult _lastForecast;

        public WeatherForm()
        {
            InitializeComponent();
        }

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

            btnGetForecast.Enabled = false;
            btnGetForecast.Text = "Загрузка…";
            pnlForecast.Visible = false;
            pnlRec.Visible = false;

            try
            {
                _lastForecast = await _weatherService.GetForecastAsync(city);
                ShowForecast(_lastForecast);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGetForecast.Enabled = true;
                btnGetForecast.Text = "Обновить прогноз";
            }
        }

        private void ShowForecast(WeatherForecastResult r)
        {
            if (r == null)
                return;

            if (!r.Success)
            {
                MessageBox.Show(r.ErrorMsg, "Ошибка прогноза",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblCity.Text = "📍 " + r.CityName + "  (" + r.Latitude.ToString("F2") + "° с.ш., " + r.Longitude.ToString("F2") + "° в.д.)";

            if (r.Days.Count > 0)
                FillDayPanel(pnlDay0, lblDay0Label, lblDay0Temp, r.Days[0]);
            else
                pnlDay0.Visible = false;

            if (r.Days.Count > 1)
                FillDayPanel(pnlDay1, lblDay1Label, lblDay1Temp, r.Days[1]);
            else
                pnlDay1.Visible = false;

            if (r.Days.Count > 2)
                FillDayPanel(pnlDay2, lblDay2Label, lblDay2Temp, r.Days[2]);
            else
                pnlDay2.Visible = false;

            var recs = _weatherService.BuildRecommendations(r);
            lstRecommendations.Items.Clear();

            for (int i = 0; i < recs.Count; i++)
                lstRecommendations.Items.Add(recs[i]);

            pnlForecast.Visible = true;
            pnlRec.Visible = true;
        }

        private void FillDayPanel(Panel pnl, Label lblLabel, Label lblTemp, DayForecast day)
        {
            pnl.Visible = true;
            lblLabel.Text = day.Label;

            string maxStr = day.TempMax >= 0 ? "+" + day.TempMax.ToString("0.#") : day.TempMax.ToString("0.#");
            string minStr = day.TempMin >= 0 ? "+" + day.TempMin.ToString("0.#") : day.TempMin.ToString("0.#");
            lblTemp.Text = maxStr + "° / " + minStr + "°";

            double tMin = day.TempMin;
            double tMax = day.TempMax;

            bool frost = tMin < WeatherSettings.GlobalFrostThreshold;
            bool heat = tMax > WeatherSettings.GlobalHeatThreshold;

            bool catWarn = false;
            foreach (var t in WeatherSettings.Thresholds.Values)
            {
                if (tMin < t.MinSafeTemp || tMax > t.MaxSafeTemp)
                {
                    catWarn = true;
                    break;
                }
            }

            if (frost || heat)
            {
                pnl.BackColor = Color.FromArgb(229, 57, 53);
                lblTemp.ForeColor = Color.White;
                lblLabel.ForeColor = Color.White;
            }
            else if (catWarn)
            {
                pnl.BackColor = Color.FromArgb(251, 140, 0);
                lblTemp.ForeColor = Color.White;
                lblLabel.ForeColor = Color.White;
            }
            else
            {
                pnl.BackColor = Color.FromArgb(56, 142, 60);
                lblTemp.ForeColor = Color.White;
                lblLabel.ForeColor = Color.White;
            }
        }

        private void btnThresholds_Click(object sender, EventArgs e)
        {
            var dlg = new TempThresholdForm(_weatherService);
            dlg.ShowDialog(this);
            dlg.Dispose();

            if (_lastForecast != null && _lastForecast.Success)
                ShowForecast(_lastForecast);
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnGetForecast_Click(sender, e);
        }
    }
}
