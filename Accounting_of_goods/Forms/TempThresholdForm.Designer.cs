namespace WinFormsApp1
{
    partial class TempThresholdForm
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
            lblTitle       = new Label();
            lblHint        = new Label();
            dgvThresholds  = new DataGridView();
            colId          = new DataGridViewTextBoxColumn();
            colCategory    = new DataGridViewTextBoxColumn();
            colMin         = new DataGridViewTextBoxColumn();
            colMax         = new DataGridViewTextBoxColumn();
            pnlBottom      = new Panel();
            btnResetDefaults = new Button();
            btnCancel      = new Button();
            btnSave        = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvThresholds).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();

            // ── Заголовок ─────────────────────────────────────────────────────────
            lblTitle.Dock    = DockStyle.Top;
            lblTitle.Font    = new Font("Arial", 11F, FontStyle.Bold);
            lblTitle.Height  = 46;
            lblTitle.Name    = "lblTitle";
            lblTitle.Padding = new Padding(12, 12, 0, 0);
            lblTitle.Text    = "Настройка порогов температур по типам товаров";

            lblHint.Dock    = DockStyle.Top;
            lblHint.Font    = new Font("Arial", 8F, FontStyle.Italic);
            lblHint.Height  = 36;
            lblHint.Name    = "lblHint";
            lblHint.Padding = new Padding(12, 6, 12, 0);
            lblHint.ForeColor = Color.DimGray;
            lblHint.Text    = "Укажите диапазон безопасных температур для каждой категории товаров (°C).";

            // ── Таблица ───────────────────────────────────────────────────────────
            dgvThresholds.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvThresholds.Location = new Point(12, 90);
            dgvThresholds.Name     = "dgvThresholds";
            dgvThresholds.Size     = new Size(560, 260);
            dgvThresholds.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThresholds.AllowUserToAddRows    = false;
            dgvThresholds.AllowUserToDeleteRows = false;
            dgvThresholds.RowHeadersVisible     = false;
            dgvThresholds.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgvThresholds.Font                  = new Font("Arial", 8.25F);
            dgvThresholds.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8.25F, FontStyle.Bold);

            colId.HeaderText = "ID";
            colId.Name       = "colId";
            colId.ReadOnly   = true;
            colId.Visible    = false;

            colCategory.HeaderText = "Категория товара";
            colCategory.Name       = "colCategory";
            colCategory.ReadOnly   = true;
            colCategory.FillWeight = 50;

            colMin.HeaderText = "Мин. темп. (°C)";
            colMin.Name       = "colMin";
            colMin.FillWeight = 25;

            colMax.HeaderText = "Макс. темп. (°C)";
            colMax.Name       = "colMax";
            colMax.FillWeight = 25;

            dgvThresholds.Columns.AddRange(new DataGridViewColumn[]
            { colId, colCategory, colMin, colMax });

            // ── Нижняя панель ─────────────────────────────────────────────────────
            pnlBottom.Dock    = DockStyle.Bottom;
            pnlBottom.Height  = 56;
            pnlBottom.Name    = "pnlBottom";
            pnlBottom.Padding = new Padding(10, 10, 10, 10);

            btnResetDefaults.Anchor    = AnchorStyles.Bottom | AnchorStyles.Left;
            btnResetDefaults.FlatStyle = FlatStyle.System;
            btnResetDefaults.Font      = new Font("Arial", 8.25F);
            btnResetDefaults.Location  = new Point(10, 12);
            btnResetDefaults.Name      = "btnResetDefaults";
            btnResetDefaults.Size      = new Size(160, 32);
            btnResetDefaults.Text      = "По умолчанию";
            btnResetDefaults.Click    += btnResetDefaults_Click;

            btnCancel.Anchor    = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.FlatStyle = FlatStyle.System;
            btnCancel.Font      = new Font("Arial", 8.25F);
            btnCancel.Location  = new Point(276, 12);
            btnCancel.Name      = "btnCancel";
            btnCancel.Size      = new Size(130, 32);
            btnCancel.Text      = "Отмена";
            btnCancel.Click    += btnCancel_Click;

            btnSave.Anchor    = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.FlatStyle = FlatStyle.System;
            btnSave.Font      = new Font("Arial", 8.25F);
            btnSave.Location  = new Point(415, 12);
            btnSave.Name      = "btnSave";
            btnSave.Size      = new Size(157, 32);
            btnSave.Text      = "Сохранить пороги";
            btnSave.Click    += btnSave_Click;

            pnlBottom.Controls.AddRange(new Control[] { btnResetDefaults, btnCancel, btnSave });

            // ── Форма ─────────────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(584, 410);
            FormBorderStyle     = FormBorderStyle.Sizable;
            MinimumSize         = new Size(500, 380);
            MaximizeBox         = false;
            Name                = "TempThresholdForm";
            StartPosition       = FormStartPosition.CenterParent;
            Text                = "Складской учёт — Настройка температурных порогов";
            Load               += TempThresholdForm_Load;

            Controls.Add(dgvThresholds);
            Controls.Add(pnlBottom);
            Controls.Add(lblHint);
            Controls.Add(lblTitle);

            ((System.ComponentModel.ISupportInitialize)dgvThresholds).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label                    lblTitle;
        private Label                    lblHint;
        private DataGridView             dgvThresholds;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colMin;
        private DataGridViewTextBoxColumn colMax;
        private Panel                    pnlBottom;
        private Button                   btnResetDefaults;
        private Button                   btnCancel;
        private Button                   btnSave;
    }
}
