namespace WinFormsApp1
{
    public partial class HeatMapSettingsForm : Form
    {
        public HeatMapSettings Result { get; private set; }

        private readonly HeatMapSettings _source;

        public HeatMapSettingsForm() : this(HeatMapSettings.Current) { }

        public HeatMapSettingsForm(HeatMapSettings source)
        {
            InitializeComponent();
            _source = source;
        }

        private void HeatMapSettingsForm_Load(object sender, EventArgs e)
        {
            numGreen.Value = _source.GreenThresholdDays;
            numYellow.Value = _source.YellowThresholdDays;
            numOrange.Value = _source.OrangeThresholdDays;

            numTGreen.Value = _source.TurnoverGreenMin;
            numTYellow.Value = _source.TurnoverYellowMin;
            numTOrange.Value = _source.TurnoverOrangeMin;

            numRefresh.Value = _source.AutoRefreshSeconds;

            chkEnabled.Checked = _source.Enabled;
            chkShowNames.Checked = _source.ShowProductNames;

            cmbMode.SelectedIndex = _source.Mode == HeatMapMode.Expiry ? 0 : 1;

            ApplyConstraints();

            numGreen.ValueChanged += numGreen_ValueChanged;
            numYellow.ValueChanged += numYellow_ValueChanged;
            numOrange.ValueChanged += numOrange_ValueChanged;
            numTGreen.ValueChanged += numTGreen_ValueChanged;
            numTYellow.ValueChanged += numTYellow_ValueChanged;
            numTOrange.ValueChanged += numTOrange_ValueChanged;

            UpdatePanelVisibility();
        }

        private void numGreen_ValueChanged(object sender, EventArgs e)
        {
            numYellow.Maximum = numGreen.Value - 1;
            if (numYellow.Value >= numGreen.Value)
                numYellow.Value = numGreen.Value - 1;
        }

        private void numYellow_ValueChanged(object sender, EventArgs e)
        {
            numOrange.Maximum = Math.Max(0, numYellow.Value - 1);
            if (numOrange.Value >= numYellow.Value)
                numOrange.Value = Math.Max(0, numYellow.Value - 1);
            numYellow.Minimum = numOrange.Value + 1;
        }

        private void numOrange_ValueChanged(object sender, EventArgs e)
        {
            numYellow.Minimum = numOrange.Value + 1;
        }

        private void numTGreen_ValueChanged(object sender, EventArgs e)
        {
            numTYellow.Maximum = numTGreen.Value - 1;
            if (numTYellow.Value >= numTGreen.Value)
                numTYellow.Value = numTGreen.Value - 1;
        }

        private void numTYellow_ValueChanged(object sender, EventArgs e)
        {
            numTOrange.Maximum = Math.Max(0, numTYellow.Value - 1);
            if (numTOrange.Value >= numTYellow.Value)
                numTOrange.Value = Math.Max(0, numTYellow.Value - 1);
            numTYellow.Minimum = numTOrange.Value + 1;
        }

        private void numTOrange_ValueChanged(object sender, EventArgs e)
        {
            numTYellow.Minimum = numTOrange.Value + 1;
        }

        private void ApplyConstraints()
        {
            numYellow.Maximum = numGreen.Value - 1;
            numOrange.Maximum = Math.Max(0, numYellow.Value - 1);
            numYellow.Minimum = numOrange.Value + 1;

            numTYellow.Maximum = numTGreen.Value - 1;
            numTOrange.Maximum = Math.Max(0, numTYellow.Value - 1);
            numTYellow.Minimum = numTOrange.Value + 1;
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e) => UpdatePanelVisibility();

        private void UpdatePanelVisibility()
        {
            bool isExpiry = cmbMode.SelectedIndex == 0;
            grpExpiry.Visible = isExpiry;
            grpTurnover.Visible = !isExpiry;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (numYellow.Value >= numGreen.Value || numOrange.Value >= numYellow.Value)
            {
                MessageBox.Show(
                    "Пороги должны соблюдать порядок:\nЗелёный > Жёлтый > Оранжевый",
                    "Ошибка настроек", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Result = new HeatMapSettings
            {
                Enabled = chkEnabled.Checked,
                ShowProductNames = chkShowNames.Checked,
                Mode = cmbMode.SelectedIndex == 0 ? HeatMapMode.Expiry : HeatMapMode.Turnover,

                GreenThresholdDays = (int)numGreen.Value,
                YellowThresholdDays = (int)numYellow.Value,
                OrangeThresholdDays = (int)numOrange.Value,

                TurnoverGreenMin = (int)numTGreen.Value,
                TurnoverYellowMin = (int)numTYellow.Value,
                TurnoverOrangeMin = (int)numTOrange.Value,

                AutoRefreshSeconds = (int)numRefresh.Value
            };

            HeatMapSettings.Current.Enabled = Result.Enabled;
            HeatMapSettings.Current.ShowProductNames = Result.ShowProductNames;
            HeatMapSettings.Current.Mode = Result.Mode;
            HeatMapSettings.Current.GreenThresholdDays = Result.GreenThresholdDays;
            HeatMapSettings.Current.YellowThresholdDays = Result.YellowThresholdDays;
            HeatMapSettings.Current.OrangeThresholdDays = Result.OrangeThresholdDays;
            HeatMapSettings.Current.TurnoverGreenMin = Result.TurnoverGreenMin;
            HeatMapSettings.Current.TurnoverYellowMin = Result.TurnoverYellowMin;
            HeatMapSettings.Current.TurnoverOrangeMin = Result.TurnoverOrangeMin;
            HeatMapSettings.Current.AutoRefreshSeconds = Result.AutoRefreshSeconds;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            numYellow.Minimum = 1;
            numOrange.Minimum = 0;
            numTYellow.Minimum = 1;
            numTOrange.Minimum = 0;

            numGreen.Value = 90;
            numYellow.Value = 20;
            numOrange.Value = 7;
            numTGreen.Value = 10;
            numTYellow.Value = 4;
            numTOrange.Value = 1;
            numRefresh.Value = 0;

            ApplyConstraints();
        }
    }
}
