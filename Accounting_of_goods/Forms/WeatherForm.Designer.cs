namespace WinFormsApp1
{
    partial class WeatherForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle         = new Label();
            pnlSearch        = new Panel();
            lblCityLabel     = new Label();
            txtCity          = new TextBox();
            btnGetForecast   = new Button();
            btnThresholds    = new Button();
            lblCity          = new Label();
            pnlForecast      = new Panel();
            lblForecastTitle = new Label();
            pnlDay0          = new Panel();
            lblDay0Label     = new Label();
            lblDay0Temp      = new Label();
            pnlDay1          = new Panel();
            lblDay1Label     = new Label();
            lblDay1Temp      = new Label();
            pnlDay2          = new Panel();
            lblDay2Label     = new Label();
            lblDay2Temp      = new Label();
            pnlRec           = new Panel();
            lblRecTitle      = new Label();
            lstRecommendations = new ListBox();

            pnlSearch.SuspendLayout();
            pnlForecast.SuspendLayout();
            pnlDay0.SuspendLayout();
            pnlDay1.SuspendLayout();
            pnlDay2.SuspendLayout();
            pnlRec.SuspendLayout();
            SuspendLayout();

            lblTitle.Dock      = DockStyle.Top;
            lblTitle.Font      = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Height    = 50;
            lblTitle.Name      = "lblTitle";
            lblTitle.Padding   = new Padding(14, 12, 0, 0);
            lblTitle.Text      = "Геолокация и погода для логистики";

            pnlSearch.Dock    = DockStyle.Top;
            pnlSearch.Height  = 50;
            pnlSearch.Name    = "pnlSearch";
            pnlSearch.Padding = new Padding(14, 10, 14, 6);

            lblCityLabel.AutoSize = true;
            lblCityLabel.Font     = new Font("Arial", 8.25F);
            lblCityLabel.Location = new Point(14, 16);
            lblCityLabel.Name     = "lblCityLabel";
            lblCityLabel.Text     = "Регион назначения:";

            txtCity.Font     = new Font("Arial", 8.25F);
            txtCity.Location = new Point(150, 12);
            txtCity.Name     = "txtCity";
            txtCity.Size     = new Size(220, 26);
            txtCity.KeyDown += txtCity_KeyDown;

            btnGetForecast.FlatStyle = FlatStyle.System;
            btnGetForecast.Font      = new Font("Arial", 8.25F);
            btnGetForecast.Location  = new Point(380, 10);
            btnGetForecast.Name      = "btnGetForecast";
            btnGetForecast.Size      = new Size(160, 30);
            btnGetForecast.Text      = "Обновить прогноз";
            btnGetForecast.Click    += btnGetForecast_Click;

            btnThresholds.FlatStyle = FlatStyle.System;
            btnThresholds.Font      = new Font("Arial", 8.25F);
            btnThresholds.Location  = new Point(550, 10);
            btnThresholds.Name      = "btnThresholds";
            btnThresholds.Size      = new Size(180, 30);
            btnThresholds.Text      = "Пороги температур…";
            btnThresholds.Click    += btnThresholds_Click;

            pnlSearch.Controls.AddRange(new Control[]
            { lblCityLabel, txtCity, btnGetForecast, btnThresholds });

            lblCity.Dock      = DockStyle.Top;
            lblCity.Font      = new Font("Arial", 8.5F, FontStyle.Italic);
            lblCity.ForeColor = Color.DimGray;
            lblCity.Height    = 28;
            lblCity.Name      = "lblCity";
            lblCity.Padding   = new Padding(14, 6, 0, 0);
            lblCity.Text      = "";

            pnlForecast.Dock    = DockStyle.Top;
            pnlForecast.Height  = 130;
            pnlForecast.Name    = "pnlForecast";
            pnlForecast.Padding = new Padding(14, 8, 14, 8);
            pnlForecast.Visible = false;

            lblForecastTitle.AutoSize = true;
            lblForecastTitle.Font     = new Font("Arial", 8.25F, FontStyle.Bold);
            lblForecastTitle.Location = new Point(14, 8);
            lblForecastTitle.Name     = "lblForecastTitle";
            lblForecastTitle.Text     = "Прогноз на 3 дня:";

            BuildDayPanel(pnlDay0, lblDay0Label, lblDay0Temp, 0);
            BuildDayPanel(pnlDay1, lblDay1Label, lblDay1Temp, 1);
            BuildDayPanel(pnlDay2, lblDay2Label, lblDay2Temp, 2);

            pnlForecast.Controls.AddRange(new Control[]
            { lblForecastTitle, pnlDay0, pnlDay1, pnlDay2 });

            pnlRec.Anchor  = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlRec.Location= new Point(0, 260);
            pnlRec.Name    = "pnlRec";
            pnlRec.Size    = new Size(760, 240);
            pnlRec.Padding = new Padding(14, 8, 14, 8);
            pnlRec.Visible = false;

            lblRecTitle.AutoSize = true;
            lblRecTitle.Font     = new Font("Arial", 8.25F, FontStyle.Bold);
            lblRecTitle.Location = new Point(14, 8);
            lblRecTitle.Name     = "lblRecTitle";
            lblRecTitle.Text     = "Рекомендации системы:";

            lstRecommendations.Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstRecommendations.Font     = new Font("Arial", 8.5F);
            lstRecommendations.Location = new Point(14, 32);
            lstRecommendations.Name     = "lstRecommendations";
            lstRecommendations.Size     = new Size(730, 190);
            lstRecommendations.SelectionMode = SelectionMode.None;
            lstRecommendations.IntegralHeight = false;

            pnlRec.Controls.AddRange(new Control[] { lblRecTitle, lstRecommendations });

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(760, 520);
            FormBorderStyle     = FormBorderStyle.Sizable;
            MinimumSize         = new Size(650, 460);
            MaximizeBox         = true;
            Name                = "WeatherForm";
            StartPosition       = FormStartPosition.CenterParent;
            Text                = "Складской учёт — Геолокация и погода для логистики";
            Load               += WeatherForm_Load;

            Controls.Add(pnlRec);
            Controls.Add(pnlForecast);
            Controls.Add(lblCity);
            Controls.Add(pnlSearch);
            Controls.Add(lblTitle);

            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlForecast.ResumeLayout(false);
            pnlForecast.PerformLayout();
            pnlDay0.ResumeLayout(false);
            pnlDay1.ResumeLayout(false);
            pnlDay2.ResumeLayout(false);
            pnlRec.ResumeLayout(false);
            pnlRec.PerformLayout();
            ResumeLayout(false);
        }

        private void BuildDayPanel(Panel pnl, Label lblLabel, Label lblTemp, int index)
        {
            int panelW = 150;
            int x      = 14 + index * (panelW + 10);

            pnl.BackColor = Color.FromArgb(56, 142, 60);
            pnl.Location  = new Point(x, 30);
            pnl.Name      = $"pnlDay{index}";
            pnl.Size      = new Size(panelW, 80);

            lblLabel.AutoSize  = false;
            lblLabel.Dock      = DockStyle.Top;
            lblLabel.Font      = new Font("Arial", 9F, FontStyle.Bold);
            lblLabel.ForeColor = Color.White;
            lblLabel.Height    = 34;
            lblLabel.Name      = $"lblDay{index}Label";
            lblLabel.TextAlign = ContentAlignment.MiddleCenter;

            lblTemp.AutoSize  = false;
            lblTemp.Dock      = DockStyle.Fill;
            lblTemp.Font      = new Font("Arial", 11F, FontStyle.Bold);
            lblTemp.ForeColor = Color.White;
            lblTemp.Name      = $"lblDay{index}Temp";
            lblTemp.TextAlign = ContentAlignment.MiddleCenter;

            pnl.Controls.Add(lblTemp);
            pnl.Controls.Add(lblLabel);
        }

        #endregion

        private Label    lblTitle;
        private Panel    pnlSearch;
        private Label    lblCityLabel;
        private TextBox  txtCity;
        private Button   btnGetForecast;
        private Button   btnThresholds;
        private Label    lblCity;
        private Panel    pnlForecast;
        private Label    lblForecastTitle;
        private Panel    pnlDay0, pnlDay1, pnlDay2;
        private Label    lblDay0Label, lblDay0Temp;
        private Label    lblDay1Label, lblDay1Temp;
        private Label    lblDay2Label, lblDay2Temp;
        private Panel    pnlRec;
        private Label    lblRecTitle;
        private ListBox  lstRecommendations;
    }
}
