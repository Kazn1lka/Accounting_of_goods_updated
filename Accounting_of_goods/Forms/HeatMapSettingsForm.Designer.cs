namespace WinFormsApp1
{
    partial class HeatMapSettingsForm
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
            lblTitle = new Label();
            chkEnabled = new CheckBox();
            chkShowNames = new CheckBox();
            lblMode = new Label();
            cmbMode = new ComboBox();
            grpExpiry = new GroupBox();
            lblGreen = new Label();
            numGreen = new NumericUpDown();
            lblGreenSuffix = new Label();
            lblYellow = new Label();
            numYellow = new NumericUpDown();
            lblYellowSuffix = new Label();
            lblOrange = new Label();
            numOrange = new NumericUpDown();
            lblOrangeSuffix = new Label();
            lblRedNote = new Label();
            grpTurnover = new GroupBox();
            lblTGreen = new Label();
            numTGreen = new NumericUpDown();
            lblTGreenSuffix = new Label();
            lblTYellow = new Label();
            numTYellow = new NumericUpDown();
            lblTOrange = new Label();
            numTOrange = new NumericUpDown();
            grpRefresh = new GroupBox();
            numRefresh = new NumericUpDown();
            lblRefreshSuffix = new Label();
            pnlBottom = new Panel();
            btnResetDefaults = new Button();
            btnApply = new Button();
            btnCancel = new Button();
            grpExpiry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYellow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numOrange).BeginInit();
            grpTurnover.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTYellow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTOrange).BeginInit();
            grpRefresh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRefresh).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(20, 16, 0, 0);
            lblTitle.Size = new Size(630, 70);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Настройки тепловой карты";
            // 
            // chkEnabled
            // 
            chkEnabled.AutoSize = true;
            chkEnabled.Font = new Font("Arial", 8.25F);
            chkEnabled.Location = new Point(26, 83);
            chkEnabled.Margin = new Padding(5);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(312, 29);
            chkEnabled.TabIndex = 1;
            chkEnabled.Text = "Включить тепловую карту";
            // 
            // chkShowNames
            // 
            chkShowNames.AutoSize = true;
            chkShowNames.Font = new Font("Arial", 8.25F);
            chkShowNames.Location = new Point(26, 122);
            chkShowNames.Margin = new Padding(5);
            chkShowNames.Name = "chkShowNames";
            chkShowNames.Size = new Size(400, 29);
            chkShowNames.TabIndex = 2;
            chkShowNames.Text = "Отображать наименование товара";
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Font = new Font("Arial", 8.25F);
            lblMode.Location = new Point(26, 170);
            lblMode.Margin = new Padding(5, 0, 5, 0);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(152, 25);
            lblMode.TabIndex = 3;
            lblMode.Text = "Режим карты:";
            // 
            // cmbMode
            // 
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.Font = new Font("Arial", 8.25F);
            cmbMode.FormattingEnabled = true;
            cmbMode.Items.AddRange(new object[] { "По сроку годности", "По оборачиваемости" });
            cmbMode.Location = new Point(211, 163);
            cmbMode.Margin = new Padding(5);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(339, 33);
            cmbMode.TabIndex = 4;
            cmbMode.SelectedIndexChanged += cmbMode_SelectedIndexChanged;
            // 
            // grpExpiry
            // 
            grpExpiry.Controls.Add(lblGreen);
            grpExpiry.Controls.Add(numGreen);
            grpExpiry.Controls.Add(lblGreenSuffix);
            grpExpiry.Controls.Add(lblYellow);
            grpExpiry.Controls.Add(numYellow);
            grpExpiry.Controls.Add(lblYellowSuffix);
            grpExpiry.Controls.Add(lblOrange);
            grpExpiry.Controls.Add(numOrange);
            grpExpiry.Controls.Add(lblOrangeSuffix);
            grpExpiry.Controls.Add(lblRedNote);
            grpExpiry.Font = new Font("Arial", 8.25F);
            grpExpiry.Location = new Point(20, 211);
            grpExpiry.Margin = new Padding(5);
            grpExpiry.Name = "grpExpiry";
            grpExpiry.Padding = new Padding(5);
            grpExpiry.Size = new Size(578, 240);
            grpExpiry.TabIndex = 5;
            grpExpiry.TabStop = false;
            grpExpiry.Text = "Пороги (срок годности, дни до истечения)";
            // 
            // lblGreen
            // 
            lblGreen.AutoSize = true;
            lblGreen.Font = new Font("Arial", 8.25F);
            lblGreen.ForeColor = Color.DarkGreen;
            lblGreen.Location = new Point(16, 42);
            lblGreen.Margin = new Padding(5, 0, 5, 0);
            lblGreen.Name = "lblGreen";
            lblGreen.Size = new Size(228, 25);
            lblGreen.TabIndex = 0;
            lblGreen.Text = "\U0001f7e2 Зелёный — более";
            // 
            // numGreen
            // 
            numGreen.Location = new Point(276, 35);
            numGreen.Margin = new Padding(5);
            numGreen.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numGreen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numGreen.Name = "numGreen";
            numGreen.Size = new Size(98, 33);
            numGreen.TabIndex = 1;
            numGreen.Value = new decimal(new int[] { 90, 0, 0, 0 });
            // 
            // lblGreenSuffix
            // 
            lblGreenSuffix.AutoSize = true;
            lblGreenSuffix.Font = new Font("Arial", 8.25F);
            lblGreenSuffix.Location = new Point(387, 42);
            lblGreenSuffix.Margin = new Padding(5, 0, 5, 0);
            lblGreenSuffix.Name = "lblGreenSuffix";
            lblGreenSuffix.Size = new Size(42, 25);
            lblGreenSuffix.TabIndex = 2;
            lblGreenSuffix.Text = "дн.";
            // 
            // lblYellow
            // 
            lblYellow.AutoSize = true;
            lblYellow.Font = new Font("Arial", 8.25F);
            lblYellow.ForeColor = Color.Goldenrod;
            lblYellow.Location = new Point(16, 93);
            lblYellow.Margin = new Padding(5, 0, 5, 0);
            lblYellow.Name = "lblYellow";
            lblYellow.Size = new Size(223, 25);
            lblYellow.TabIndex = 3;
            lblYellow.Text = "\U0001f7e1 Жёлтый — более";
            // 
            // numYellow
            // 
            numYellow.Location = new Point(276, 86);
            numYellow.Margin = new Padding(5);
            numYellow.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numYellow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numYellow.Name = "numYellow";
            numYellow.Size = new Size(98, 33);
            numYellow.TabIndex = 4;
            numYellow.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // lblYellowSuffix
            // 
            lblYellowSuffix.AutoSize = true;
            lblYellowSuffix.Font = new Font("Arial", 8.25F);
            lblYellowSuffix.Location = new Point(387, 93);
            lblYellowSuffix.Margin = new Padding(5, 0, 5, 0);
            lblYellowSuffix.Name = "lblYellowSuffix";
            lblYellowSuffix.Size = new Size(42, 25);
            lblYellowSuffix.TabIndex = 5;
            lblYellowSuffix.Text = "дн.";
            // 
            // lblOrange
            // 
            lblOrange.AutoSize = true;
            lblOrange.Font = new Font("Arial", 8.25F);
            lblOrange.ForeColor = Color.DarkOrange;
            lblOrange.Location = new Point(16, 144);
            lblOrange.Margin = new Padding(5, 0, 5, 0);
            lblOrange.Name = "lblOrange";
            lblOrange.Size = new Size(260, 25);
            lblOrange.TabIndex = 6;
            lblOrange.Text = "\U0001f7e0 Оранжевый — более";
            // 
            // numOrange
            // 
            numOrange.Location = new Point(276, 138);
            numOrange.Margin = new Padding(5);
            numOrange.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numOrange.Name = "numOrange";
            numOrange.Size = new Size(98, 33);
            numOrange.TabIndex = 7;
            numOrange.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // lblOrangeSuffix
            // 
            lblOrangeSuffix.AutoSize = true;
            lblOrangeSuffix.Font = new Font("Arial", 8.25F);
            lblOrangeSuffix.Location = new Point(387, 144);
            lblOrangeSuffix.Margin = new Padding(5, 0, 5, 0);
            lblOrangeSuffix.Name = "lblOrangeSuffix";
            lblOrangeSuffix.Size = new Size(42, 25);
            lblOrangeSuffix.TabIndex = 8;
            lblOrangeSuffix.Text = "дн.";
            // 
            // lblRedNote
            // 
            lblRedNote.AutoSize = true;
            lblRedNote.Font = new Font("Arial", 7.5F, FontStyle.Italic);
            lblRedNote.ForeColor = Color.DarkRed;
            lblRedNote.Location = new Point(16, 195);
            lblRedNote.Margin = new Padding(5, 0, 5, 0);
            lblRedNote.Name = "lblRedNote";
            lblRedNote.Size = new Size(417, 24);
            lblRedNote.TabIndex = 9;
            lblRedNote.Text = "🔴 Красный — менее порога «Оранжевый»";
            // 
            // grpTurnover
            // 
            grpTurnover.Controls.Add(lblTGreen);
            grpTurnover.Controls.Add(numTGreen);
            grpTurnover.Controls.Add(lblTGreenSuffix);
            grpTurnover.Controls.Add(lblTYellow);
            grpTurnover.Controls.Add(numTYellow);
            grpTurnover.Controls.Add(lblTOrange);
            grpTurnover.Controls.Add(numTOrange);
            grpTurnover.Font = new Font("Arial", 8.25F);
            grpTurnover.Location = new Point(20, 211);
            grpTurnover.Margin = new Padding(5);
            grpTurnover.Name = "grpTurnover";
            grpTurnover.Padding = new Padding(5);
            grpTurnover.Size = new Size(578, 186);
            grpTurnover.TabIndex = 6;
            grpTurnover.TabStop = false;
            grpTurnover.Text = "Пороги (кол-во отгрузок за 30 дней)";
            grpTurnover.Visible = false;
            // 
            // lblTGreen
            // 
            lblTGreen.AutoSize = true;
            lblTGreen.Font = new Font("Arial", 8.25F);
            lblTGreen.ForeColor = Color.DarkGreen;
            lblTGreen.Location = new Point(16, 42);
            lblTGreen.Margin = new Padding(5, 0, 5, 0);
            lblTGreen.Name = "lblTGreen";
            lblTGreen.Size = new Size(188, 25);
            lblTGreen.TabIndex = 0;
            lblTGreen.Text = "\U0001f7e2 Зелёный — от";
            // 
            // numTGreen
            // 
            numTGreen.Location = new Point(244, 35);
            numTGreen.Margin = new Padding(5);
            numTGreen.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numTGreen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTGreen.Name = "numTGreen";
            numTGreen.Size = new Size(98, 33);
            numTGreen.TabIndex = 1;
            numTGreen.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblTGreenSuffix
            // 
            lblTGreenSuffix.AutoSize = true;
            lblTGreenSuffix.Font = new Font("Arial", 8.25F);
            lblTGreenSuffix.Location = new Point(354, 42);
            lblTGreenSuffix.Margin = new Padding(5, 0, 5, 0);
            lblTGreenSuffix.Name = "lblTGreenSuffix";
            lblTGreenSuffix.Size = new Size(99, 25);
            lblTGreenSuffix.TabIndex = 2;
            lblTGreenSuffix.Text = "отгрузок";
            // 
            // lblTYellow
            // 
            lblTYellow.AutoSize = true;
            lblTYellow.Font = new Font("Arial", 8.25F);
            lblTYellow.ForeColor = Color.Goldenrod;
            lblTYellow.Location = new Point(16, 93);
            lblTYellow.Margin = new Padding(5, 0, 5, 0);
            lblTYellow.Name = "lblTYellow";
            lblTYellow.Size = new Size(183, 25);
            lblTYellow.TabIndex = 3;
            lblTYellow.Text = "\U0001f7e1 Жёлтый — от";
            // 
            // numTYellow
            // 
            numTYellow.Location = new Point(244, 86);
            numTYellow.Margin = new Padding(5);
            numTYellow.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numTYellow.Name = "numTYellow";
            numTYellow.Size = new Size(98, 33);
            numTYellow.TabIndex = 4;
            numTYellow.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // lblTOrange
            // 
            lblTOrange.AutoSize = true;
            lblTOrange.Font = new Font("Arial", 8.25F);
            lblTOrange.ForeColor = Color.DarkOrange;
            lblTOrange.Location = new Point(16, 141);
            lblTOrange.Margin = new Padding(5, 0, 5, 0);
            lblTOrange.Name = "lblTOrange";
            lblTOrange.Size = new Size(220, 25);
            lblTOrange.TabIndex = 5;
            lblTOrange.Text = "\U0001f7e0 Оранжевый — от";
            // 
            // numTOrange
            // 
            numTOrange.Location = new Point(244, 134);
            numTOrange.Margin = new Padding(5);
            numTOrange.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numTOrange.Name = "numTOrange";
            numTOrange.Size = new Size(98, 33);
            numTOrange.TabIndex = 6;
            numTOrange.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // grpRefresh
            // 
            grpRefresh.Controls.Add(numRefresh);
            grpRefresh.Controls.Add(lblRefreshSuffix);
            grpRefresh.Font = new Font("Arial", 8.25F);
            grpRefresh.Location = new Point(20, 474);
            grpRefresh.Margin = new Padding(5);
            grpRefresh.Name = "grpRefresh";
            grpRefresh.Padding = new Padding(5);
            grpRefresh.Size = new Size(578, 86);
            grpRefresh.TabIndex = 7;
            grpRefresh.TabStop = false;
            grpRefresh.Text = "Автообновление карты";
            // 
            // numRefresh
            // 
            numRefresh.Location = new Point(16, 35);
            numRefresh.Margin = new Padding(5);
            numRefresh.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numRefresh.Name = "numRefresh";
            numRefresh.Size = new Size(114, 33);
            numRefresh.TabIndex = 0;
            // 
            // lblRefreshSuffix
            // 
            lblRefreshSuffix.AutoSize = true;
            lblRefreshSuffix.Font = new Font("Arial", 8.25F);
            lblRefreshSuffix.Location = new Point(143, 42);
            lblRefreshSuffix.Margin = new Padding(5, 0, 5, 0);
            lblRefreshSuffix.Name = "lblRefreshSuffix";
            lblRefreshSuffix.Size = new Size(218, 25);
            lblRefreshSuffix.TabIndex = 1;
            lblRefreshSuffix.Text = "сек. (0 = отключено)";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnResetDefaults);
            pnlBottom.Controls.Add(btnApply);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 592);
            pnlBottom.Margin = new Padding(5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(13, 16, 13, 16);
            pnlBottom.Size = new Size(630, 83);
            pnlBottom.TabIndex = 8;
            // 
            // btnResetDefaults  (левый край)
            // 
            btnResetDefaults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnResetDefaults.FlatStyle = FlatStyle.System;
            btnResetDefaults.Font = new Font("Arial", 8.25F);
            btnResetDefaults.Location = new Point(13, 16);
            btnResetDefaults.Margin = new Padding(5);
            btnResetDefaults.Name = "btnResetDefaults";
            btnResetDefaults.Size = new Size(190, 46);
            btnResetDefaults.TabIndex = 0;
            btnResetDefaults.Text = "По умолчанию";
            btnResetDefaults.Click += btnResetDefaults_Click;
            // 
            // btnCancel  (предпоследняя справа)
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.FlatStyle = FlatStyle.System;
            btnCancel.Font = new Font("Arial", 8.25F);
            btnCancel.Location = new Point(242, 16);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(178, 46);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Отмена";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnApply  (крайняя справа)
            // 
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApply.FlatStyle = FlatStyle.System;
            btnApply.Font = new Font("Arial", 8.25F);
            btnApply.Location = new Point(430, 16);
            btnApply.Margin = new Padding(5);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(178, 46);
            btnApply.TabIndex = 1;
            btnApply.Text = "Применить";
            btnApply.Click += btnApply_Click;
            // 
            // HeatMapSettingsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(630, 675);
            Controls.Add(lblTitle);
            Controls.Add(chkEnabled);
            Controls.Add(chkShowNames);
            Controls.Add(lblMode);
            Controls.Add(cmbMode);
            Controls.Add(grpExpiry);
            Controls.Add(grpTurnover);
            Controls.Add(grpRefresh);
            Controls.Add(pnlBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HeatMapSettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Складской учёт — Настройки тепловой карты";
            Load += HeatMapSettingsForm_Load;
            grpExpiry.ResumeLayout(false);
            grpExpiry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYellow).EndInit();
            ((System.ComponentModel.ISupportInitialize)numOrange).EndInit();
            grpTurnover.ResumeLayout(false);
            grpTurnover.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTYellow).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTOrange).EndInit();
            grpRefresh.ResumeLayout(false);
            grpRefresh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRefresh).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label         lblTitle;
        private CheckBox      chkEnabled;
        private CheckBox      chkShowNames;
        private Label         lblMode;
        private ComboBox      cmbMode;
        private GroupBox      grpExpiry;
        private Label         lblGreen, lblGreenSuffix;
        private NumericUpDown numGreen;
        private Label         lblYellow, lblYellowSuffix;
        private NumericUpDown numYellow;
        private Label         lblOrange, lblOrangeSuffix;
        private NumericUpDown numOrange;
        private Label         lblRedNote;
        private GroupBox      grpTurnover;
        private Label         lblTGreen, lblTGreenSuffix;
        private NumericUpDown numTGreen;
        private Label         lblTYellow;
        private NumericUpDown numTYellow;
        private Label         lblTOrange;
        private NumericUpDown numTOrange;
        private GroupBox      grpRefresh;
        private NumericUpDown numRefresh;
        private Label         lblRefreshSuffix;
        private Panel         pnlBottom;
        private Button        btnResetDefaults;
        private Button        btnCancel;
        private Button        btnApply;
    }
}
