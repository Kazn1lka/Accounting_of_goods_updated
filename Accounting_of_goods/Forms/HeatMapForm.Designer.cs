namespace WinFormsApp1
{
    partial class HeatMapForm
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
            components  = new System.ComponentModel.Container();
            toolTip     = new ToolTip(components);
            pnlTop      = new Panel();
            lblModeLabel= new Label();
            cmbMode     = new ComboBox();
            btnRefresh  = new Button();
            btnSettings = new Button();
            pnlMap      = new Panel();
            pnlBottom   = new Panel();
            lblTotal    = new Label();
            lblStale    = new Label();
            btnExport   = new Button();
            pnlLegend   = new Panel();
            pnlLegGreen = new Panel();
            lblLegGreen = new Label();
            pnlLegYellow= new Panel();
            lblLegYellow= new Label();
            pnlLegOrange= new Panel();
            lblLegOrange= new Label();
            pnlLegRed   = new Panel();
            lblLegRed   = new Label();
            lblLegTitle = new Label();

            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlLegend.SuspendLayout();
            SuspendLayout();

            // ── pnlTop ───────────────────────────────────────────────────────────
            pnlTop.Dock    = DockStyle.Top;
            pnlTop.Height  = 46;
            pnlTop.Name    = "pnlTop";
            pnlTop.Padding = new Padding(8, 8, 8, 4);
            pnlTop.Controls.AddRange(new Control[]
            {
                lblModeLabel, cmbMode, btnRefresh, btnSettings
            });

            lblModeLabel.AutoSize = true;
            lblModeLabel.Font     = new Font("Arial", 8.25F);
            lblModeLabel.Location = new Point(8, 14);
            lblModeLabel.Name     = "lblModeLabel";
            lblModeLabel.Text     = "Режим карты:";

            cmbMode.DropDownStyle     = ComboBoxStyle.DropDownList;
            cmbMode.Font              = new Font("Arial", 8.25F);
            cmbMode.FormattingEnabled = true;
            cmbMode.Items.AddRange(new object[] { "По сроку годности", "По оборачиваемости" });
            cmbMode.Location          = new Point(105, 10);
            cmbMode.Name              = "cmbMode";
            cmbMode.Size              = new Size(190, 21);
            cmbMode.SelectedIndexChanged += cmbMode_SelectedIndexChanged;

            btnRefresh.FlatStyle = FlatStyle.System;
            btnRefresh.Font      = new Font("Arial", 8.25F);
            btnRefresh.Location  = new Point(308, 9);
            btnRefresh.Name      = "btnRefresh";
            btnRefresh.Size      = new Size(90, 27);
            btnRefresh.Text      = "⟳ Обновить";
            btnRefresh.Click    += btnRefresh_Click;

            btnSettings.FlatStyle = FlatStyle.System;
            btnSettings.Font      = new Font("Arial", 8.25F);
            btnSettings.Location  = new Point(406, 9);
            btnSettings.Name      = "btnSettings";
            btnSettings.Size      = new Size(110, 27);
            btnSettings.Text      = "⚙ Настройки";
            btnSettings.Click    += btnSettings_Click;

            // ── pnlLegend (легенда цветов) ────────────────────────────────────────
            pnlLegend.Dock    = DockStyle.Top;
            pnlLegend.Height  = 36;
            pnlLegend.Name    = "pnlLegend";
            pnlLegend.Padding = new Padding(8, 6, 8, 4);
            pnlLegend.BackColor = Color.FromArgb(245, 245, 245);

            lblLegTitle.AutoSize = true;
            lblLegTitle.Font     = new Font("Arial", 7.5F, FontStyle.Bold);
            lblLegTitle.Location = new Point(8, 10);
            lblLegTitle.Text     = "Обозначения:";

            // Зелёный
            pnlLegGreen.BackColor = Color.FromArgb(67, 160, 71);
            pnlLegGreen.Location  = new Point(105, 9);
            pnlLegGreen.Name      = "pnlLegGreen";
            pnlLegGreen.Size      = new Size(14, 14);

            lblLegGreen.AutoSize = true;
            lblLegGreen.Font     = new Font("Arial", 7.5F);
            lblLegGreen.Location = new Point(122, 10);
            lblLegGreen.Text     = "Свежий / активный";

            // Жёлтый
            pnlLegYellow.BackColor = Color.FromArgb(251, 192, 45);
            pnlLegYellow.Location  = new Point(252, 9);
            pnlLegYellow.Name      = "pnlLegYellow";
            pnlLegYellow.Size      = new Size(14, 14);

            lblLegYellow.AutoSize = true;
            lblLegYellow.Font     = new Font("Arial", 7.5F);
            lblLegYellow.Location = new Point(269, 10);
            lblLegYellow.Text     = "Нормально";

            // Оранжевый
            pnlLegOrange.BackColor = Color.FromArgb(251, 140, 0);
            pnlLegOrange.Location  = new Point(370, 9);
            pnlLegOrange.Name      = "pnlLegOrange";
            pnlLegOrange.Size      = new Size(14, 14);

            lblLegOrange.AutoSize = true;
            lblLegOrange.Font     = new Font("Arial", 7.5F);
            lblLegOrange.Location = new Point(387, 10);
            lblLegOrange.Text     = "Скоро истекает";

            // Красный
            pnlLegRed.BackColor = Color.FromArgb(229, 57, 53);
            pnlLegRed.Location  = new Point(492, 9);
            pnlLegRed.Name      = "pnlLegRed";
            pnlLegRed.Size      = new Size(14, 14);

            lblLegRed.AutoSize = true;
            lblLegRed.Font     = new Font("Arial", 7.5F);
            lblLegRed.Location = new Point(509, 10);
            lblLegRed.Text     = "Критично / залежался";

            pnlLegend.Controls.AddRange(new Control[]
            {
                lblLegTitle,
                pnlLegGreen, lblLegGreen,
                pnlLegYellow, lblLegYellow,
                pnlLegOrange, lblLegOrange,
                pnlLegRed, lblLegRed
            });

            // ── pnlBottom (статистика) ────────────────────────────────────────────
            pnlBottom.Dock      = DockStyle.Bottom;
            pnlBottom.Height    = 42;
            pnlBottom.Name      = "pnlBottom";
            pnlBottom.Padding   = new Padding(8, 8, 8, 6);
            pnlBottom.BackColor = Color.FromArgb(245, 245, 245);

            lblTotal.AutoSize = true;
            lblTotal.Font     = new Font("Arial", 8.25F);
            lblTotal.Location = new Point(8, 12);
            lblTotal.Name     = "lblTotal";
            lblTotal.Text     = "Всего позиций: —";

            lblStale.AutoSize = true;
            lblStale.Font     = new Font("Arial", 8.25F);
            lblStale.ForeColor= Color.DarkRed;
            lblStale.Location = new Point(200, 12);
            lblStale.Name     = "lblStale";
            lblStale.Text     = "Истекает: —";

            btnExport.Anchor    = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExport.FlatStyle = FlatStyle.System;
            btnExport.Font      = new Font("Arial", 8.25F);
            btnExport.Location  = new Point(720, 8);
            btnExport.Name      = "btnExport";
            btnExport.Size      = new Size(150, 27);
            btnExport.Text      = "Открыть отчёт (CSV)";
            btnExport.Click    += btnExport_Click;

            pnlBottom.Controls.AddRange(new Control[] { lblTotal, lblStale, btnExport });

            // ── pnlMap (тепловая карта) ───────────────────────────────────────────
            pnlMap.Anchor      = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlMap.AutoScroll  = true;
            pnlMap.BackColor   = Color.FromArgb(235, 235, 235);
            pnlMap.Location    = new Point(0, 82);
            pnlMap.Name        = "pnlMap";
            pnlMap.Size        = new Size(900, 520);
            pnlMap.Paint      += pnlMap_Paint;
            pnlMap.MouseMove  += pnlMap_MouseMove;
            pnlMap.Resize     += pnlMap_Resize;

            // ── Форма ─────────────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(900, 680);
            FormBorderStyle     = FormBorderStyle.Sizable;
            MaximizeBox         = true;
            MinimumSize         = new Size(700, 500);
            Name                = "HeatMapForm";
            StartPosition       = FormStartPosition.CenterParent;
            Text                = "Складской учёт — Живая тепловая карта склада";
            Load               += HeatMapForm_Load;

            Controls.Add(pnlMap);
            Controls.Add(pnlBottom);
            Controls.Add(pnlLegend);
            Controls.Add(pnlTop);

            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            pnlLegend.ResumeLayout(false);
            pnlLegend.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolTip  toolTip;
        private Panel    pnlTop;
        private Label    lblModeLabel;
        private ComboBox cmbMode;
        private Button   btnRefresh;
        private Button   btnSettings;
        private Panel    pnlLegend;
        private Label    lblLegTitle;
        private Panel    pnlLegGreen, pnlLegYellow, pnlLegOrange, pnlLegRed;
        private Label    lblLegGreen, lblLegYellow, lblLegOrange, lblLegRed;
        private Panel    pnlMap;
        private Panel    pnlBottom;
        private Label    lblTotal;
        private Label    lblStale;
        private Button   btnExport;
    }
}
