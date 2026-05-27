namespace WinFormsApp1
{
    partial class WriteOffForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvExpired = new DataGridView();
            lblTitle = new Label();
            lblReason = new Label();
            txtReason = new TextBox();
            lblStatus = new Label();
            lblTotalLoss = new Label();
            btnWriteOff = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvExpired).BeginInit();
            SuspendLayout();
            // 
            // dgvExpired
            // 
            dgvExpired.AllowUserToAddRows = false;
            dgvExpired.AllowUserToDeleteRows = false;
            dgvExpired.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvExpired.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpired.Location = new Point(15, 55);
            dgvExpired.Margin = new Padding(5);
            dgvExpired.Name = "dgvExpired";
            dgvExpired.RowHeadersWidth = 51;
            dgvExpired.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpired.Size = new Size(870, 460);
            dgvExpired.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(12, 10, 0, 0);
            lblTitle.Size = new Size(900, 48);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Товары с истёкшим сроком годности";
            // 
            // lblReason
            // 
            lblReason.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblReason.AutoSize = true;
            lblReason.Location = new Point(15, 528);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(118, 32);
            lblReason.TabIndex = 2;
            lblReason.Text = "Причина:";
            // 
            // txtReason
            // 
            txtReason.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtReason.Location = new Point(139, 528);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(500, 39);
            txtReason.TabIndex = 3;
            txtReason.Text = "Истёк срок годности";
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Arial", 8F, FontStyle.Italic);
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.Location = new Point(15, 568);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 26);
            lblStatus.TabIndex = 5;
            // 
            // lblTotalLoss
            // 
            lblTotalLoss.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotalLoss.AutoSize = true;
            lblTotalLoss.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblTotalLoss.ForeColor = Color.DarkRed;
            lblTotalLoss.Location = new Point(15, 598);
            lblTotalLoss.Name = "lblTotalLoss";
            lblTotalLoss.Size = new Size(300, 28);
            lblTotalLoss.TabIndex = 8;
            lblTotalLoss.Text = "Общий убыток: 0,00";
            // 
            // btnWriteOff
            // 
            btnWriteOff.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnWriteOff.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnWriteOff.Location = new Point(522, 590);
            btnWriteOff.Name = "btnWriteOff";
            btnWriteOff.Size = new Size(190, 48);
            btnWriteOff.TabIndex = 6;
            btnWriteOff.Text = "Списать выбранные";
            btnWriteOff.UseVisualStyleBackColor = true;
            btnWriteOff.Click += btnWriteOff_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(718, 590);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(167, 48);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // WriteOffForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 655);
            Controls.Add(lblTitle);
            Controls.Add(dgvExpired);
            Controls.Add(lblReason);
            Controls.Add(txtReason);
            Controls.Add(lblStatus);
            Controls.Add(lblTotalLoss);
            Controls.Add(btnWriteOff);
            Controls.Add(btnCancel);
            MinimumSize = new Size(750, 600);
            Name = "WriteOffForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Списание просроченных товаров";
            Load += WriteOffForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvExpired).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvExpired;
        private Label lblTitle;
        private Label lblReason;
        private TextBox txtReason;
        private Label lblStatus;
        private Label lblTotalLoss;
        private Button btnWriteOff;
        private Button btnCancel;
    }
}