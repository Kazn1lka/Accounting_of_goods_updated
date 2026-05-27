namespace WinFormsApp1
{
    partial class CounterpartyForm
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
            lblInnLabel = new Label();
            txtInn = new TextBox();
            btnCheck = new Button();
            lblHint = new Label();
            pnlResults = new Panel();
            lblCompanyNameLabel = new Label();
            txtCompanyName = new TextBox();
            lblInnKppOgrn = new Label();
            lblAddress = new Label();
            lblDirector = new Label();
            lblChecksTitle = new Label();
            chkTaxDebtor = new CheckBox();
            lblTaxDebtorNote = new Label();
            chkBankrupt = new CheckBox();
            lblBankruptNote = new Label();
            chkDisqualified = new CheckBox();
            lblDisqualNote = new Label();
            lblStatusLabel = new Label();
            txtStatus = new TextBox();
            pnlBottom = new Panel();
            btnCancel = new Button();
            btnAllow = new Button();
            btnForbid = new Button();
            pnlResults.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(16, 16, 0, 0);
            lblTitle.Size = new Size(1070, 77);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Проверка контрагента по ИНН";
            // 
            // lblInnLabel
            // 
            lblInnLabel.AutoSize = true;
            lblInnLabel.Font = new Font("Arial", 8.25F);
            lblInnLabel.Location = new Point(32, 93);
            lblInnLabel.Margin = new Padding(5, 0, 5, 0);
            lblInnLabel.Name = "lblInnLabel";
            lblInnLabel.Size = new Size(193, 25);
            lblInnLabel.TabIndex = 1;
            lblInnLabel.Text = "ИНН контрагента:";
            // 
            // txtInn
            // 
            txtInn.BorderStyle = BorderStyle.FixedSingle;
            txtInn.Font = new Font("Arial", 8.25F);
            txtInn.Location = new Point(32, 125);
            txtInn.Margin = new Padding(5);
            txtInn.MaxLength = 12;
            txtInn.Name = "txtInn";
            txtInn.PlaceholderText = "10 или 12 цифр";
            txtInn.Size = new Size(405, 33);
            txtInn.TabIndex = 0;
            txtInn.KeyDown += TxtInn_KeyDown;
            txtInn.KeyPress += TxtInn_KeyPress;
            // 
            // btnCheck
            // 
            btnCheck.FlatStyle = FlatStyle.System;
            btnCheck.Font = new Font("Arial", 8.25F);
            btnCheck.Location = new Point(455, 122);
            btnCheck.Margin = new Padding(5);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(158, 43);
            btnCheck.TabIndex = 1;
            btnCheck.Text = "Проверить";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += BtnCheck_Click;
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Arial", 7.5F, FontStyle.Italic);
            lblHint.ForeColor = Color.Gray;
            lblHint.Location = new Point(32, 173);
            lblHint.Margin = new Padding(5, 0, 5, 0);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(496, 24);
            lblHint.TabIndex = 2;
            lblHint.Text = "Введите ИНН и нажмите «Проверить» (или Enter)";
            // 
            // pnlResults
            // 
            pnlResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlResults.Controls.Add(lblCompanyNameLabel);
            pnlResults.Controls.Add(txtCompanyName);
            pnlResults.Controls.Add(lblInnKppOgrn);
            pnlResults.Controls.Add(lblAddress);
            pnlResults.Controls.Add(lblDirector);
            pnlResults.Controls.Add(lblChecksTitle);
            pnlResults.Controls.Add(chkTaxDebtor);
            pnlResults.Controls.Add(lblTaxDebtorNote);
            pnlResults.Controls.Add(chkBankrupt);
            pnlResults.Controls.Add(lblBankruptNote);
            pnlResults.Controls.Add(chkDisqualified);
            pnlResults.Controls.Add(lblDisqualNote);
            pnlResults.Controls.Add(lblStatusLabel);
            pnlResults.Controls.Add(txtStatus);
            pnlResults.Location = new Point(0, 208);
            pnlResults.Margin = new Padding(5);
            pnlResults.Name = "pnlResults";
            pnlResults.Padding = new Padding(20, 6, 20, 6);
            pnlResults.Size = new Size(1070, 552);
            pnlResults.TabIndex = 3;
            pnlResults.Visible = false;
            // 
            // lblCompanyNameLabel
            // 
            lblCompanyNameLabel.AutoSize = true;
            lblCompanyNameLabel.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            lblCompanyNameLabel.Location = new Point(32, 10);
            lblCompanyNameLabel.Margin = new Padding(5, 0, 5, 0);
            lblCompanyNameLabel.Name = "lblCompanyNameLabel";
            lblCompanyNameLabel.Size = new Size(122, 24);
            lblCompanyNameLabel.TabIndex = 0;
            lblCompanyNameLabel.Text = "Контрагент:";
            // 
            // txtCompanyName
            // 
            txtCompanyName.BorderStyle = BorderStyle.FixedSingle;
            txtCompanyName.Font = new Font("Arial", 9.75F);
            txtCompanyName.Location = new Point(220, 8);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(460, 33);
            txtCompanyName.TabIndex = 0;
            // 
            // lblInnKppOgrn
            // 
            lblInnKppOgrn.AutoSize = true;
            lblInnKppOgrn.Font = new Font("Arial", 7.8F);
            lblInnKppOgrn.ForeColor = Color.DimGray;
            lblInnKppOgrn.Location = new Point(32, 51);
            lblInnKppOgrn.Margin = new Padding(5, 0, 5, 0);
            lblInnKppOgrn.Name = "lblInnKppOgrn";
            lblInnKppOgrn.Size = new Size(265, 24);
            lblInnKppOgrn.TabIndex = 1;
            lblInnKppOgrn.Text = "ИНН — / КПП — / ОГРН —";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Arial", 7.5F);
            lblAddress.ForeColor = Color.DimGray;
            lblAddress.Location = new Point(32, 83);
            lblAddress.Margin = new Padding(5, 0, 5, 0);
            lblAddress.MaximumSize = new Size(942, 0);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(0, 23);
            lblAddress.TabIndex = 2;
            // 
            // lblDirector
            // 
            lblDirector.AutoSize = true;
            lblDirector.Font = new Font("Arial", 7.5F);
            lblDirector.ForeColor = Color.DimGray;
            lblDirector.Location = new Point(32, 109);
            lblDirector.Margin = new Padding(5, 0, 5, 0);
            lblDirector.Name = "lblDirector";
            lblDirector.Size = new Size(0, 23);
            lblDirector.TabIndex = 3;
            // 
            // lblChecksTitle
            // 
            lblChecksTitle.AutoSize = true;
            lblChecksTitle.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblChecksTitle.Location = new Point(32, 147);
            lblChecksTitle.Margin = new Padding(5, 0, 5, 0);
            lblChecksTitle.Name = "lblChecksTitle";
            lblChecksTitle.Size = new Size(251, 26);
            lblChecksTitle.TabIndex = 4;
            lblChecksTitle.Text = "Результаты проверок:";
            // 
            // chkTaxDebtor
            // 
            chkTaxDebtor.AutoSize = true;
            chkTaxDebtor.Enabled = true;
            chkTaxDebtor.Font = new Font("Arial", 8.25F);
            chkTaxDebtor.Location = new Point(32, 179);
            chkTaxDebtor.Margin = new Padding(5);
            chkTaxDebtor.Name = "chkTaxDebtor";
            chkTaxDebtor.Size = new Size(282, 29);
            chkTaxDebtor.TabIndex = 5;
            chkTaxDebtor.Text = "Не налоговый должник";
            // 
            // lblTaxDebtorNote
            // 
            lblTaxDebtorNote.AutoSize = true;
            lblTaxDebtorNote.Font = new Font("Arial", 7.5F, FontStyle.Italic);
            lblTaxDebtorNote.ForeColor = Color.Gray;
            lblTaxDebtorNote.Location = new Point(520, 184);
            lblTaxDebtorNote.Margin = new Padding(5, 0, 5, 0);
            lblTaxDebtorNote.Name = "lblTaxDebtorNote";
            lblTaxDebtorNote.Size = new Size(0, 24);
            lblTaxDebtorNote.TabIndex = 6;
            // 
            // chkBankrupt
            // 
            chkBankrupt.AutoSize = true;
            chkBankrupt.Enabled = true;
            chkBankrupt.Font = new Font("Arial", 8.25F);
            chkBankrupt.Location = new Point(32, 218);
            chkBankrupt.Margin = new Padding(5);
            chkBankrupt.Name = "chkBankrupt";
            chkBankrupt.Size = new Size(290, 29);
            chkBankrupt.TabIndex = 7;
            chkBankrupt.Text = "Не в реестре банкротов";
            // 
            // lblBankruptNote
            // 
            lblBankruptNote.AutoSize = true;
            lblBankruptNote.Font = new Font("Arial", 7.5F, FontStyle.Italic);
            lblBankruptNote.ForeColor = Color.Gray;
            lblBankruptNote.Location = new Point(520, 222);
            lblBankruptNote.Margin = new Padding(5, 0, 5, 0);
            lblBankruptNote.Name = "lblBankruptNote";
            lblBankruptNote.Size = new Size(0, 24);
            lblBankruptNote.TabIndex = 8;
            // 
            // chkDisqualified
            // 
            chkDisqualified.AutoSize = true;
            chkDisqualified.Enabled = true;
            chkDisqualified.Font = new Font("Arial", 8.25F);
            chkDisqualified.Location = new Point(32, 256);
            chkDisqualified.Margin = new Padding(5);
            chkDisqualified.Name = "chkDisqualified";
            chkDisqualified.Size = new Size(458, 29);
            chkDisqualified.TabIndex = 9;
            chkDisqualified.Text = "Все директора не дисквалифицированы";
            // 
            // lblDisqualNote
            // 
            lblDisqualNote.AutoSize = true;
            lblDisqualNote.Font = new Font("Arial", 7.5F, FontStyle.Italic);
            lblDisqualNote.ForeColor = Color.Gray;
            lblDisqualNote.Location = new Point(520, 261);
            lblDisqualNote.Margin = new Padding(5, 0, 5, 0);
            lblDisqualNote.Name = "lblDisqualNote";
            lblDisqualNote.Size = new Size(0, 24);
            lblDisqualNote.TabIndex = 10;
            // 
            // lblStatusLabel
            // 
            lblStatusLabel.AutoSize = true;
            lblStatusLabel.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblStatusLabel.Location = new Point(32, 314);
            lblStatusLabel.Margin = new Padding(5, 0, 5, 0);
            lblStatusLabel.Name = "lblStatusLabel";
            lblStatusLabel.Size = new Size(335, 26);
            lblStatusLabel.TabIndex = 11;
            lblStatusLabel.Text = "Итоговый статус проверки:";
            // 
            // txtStatus
            // 
            txtStatus.BorderStyle = BorderStyle.FixedSingle;
            txtStatus.Font = new Font("Arial", 8.25F);
            txtStatus.Location = new Point(380, 311);
            txtStatus.Margin = new Padding(5);
            txtStatus.Name = "txtStatus";
            txtStatus.ReadOnly = true;
            txtStatus.Enabled = false;
            txtStatus.Size = new Size(300, 33);
            txtStatus.TabIndex = 12;
            txtStatus.TabStop = false;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnAllow);
            pnlBottom.Controls.Add(btnForbid);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 760);
            pnlBottom.Margin = new Padding(5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(16, 13, 16, 13);
            pnlBottom.Size = new Size(1070, 88);
            pnlBottom.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.FlatStyle = FlatStyle.System;
            btnCancel.Font = new Font("Arial", 8.25F);
            btnCancel.Location = new Point(32, 19);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(195, 51);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAllow
            // 
            btnAllow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAllow.FlatStyle = FlatStyle.System;
            btnAllow.Font = new Font("Arial", 8.25F);
            btnAllow.Location = new Point(620, 19);
            btnAllow.Margin = new Padding(5);
            btnAllow.Name = "btnAllow";
            btnAllow.Size = new Size(195, 51);
            btnAllow.TabIndex = 9;
            btnAllow.Text = "Разрешить";
            btnAllow.UseVisualStyleBackColor = true;
            btnAllow.Click += btnAllow_Click;
            // 
            // btnForbid
            // 
            btnForbid.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnForbid.FlatStyle = FlatStyle.System;
            btnForbid.Font = new Font("Arial", 8.25F);
            btnForbid.Location = new Point(840, 19);
            btnForbid.Margin = new Padding(5);
            btnForbid.Name = "btnForbid";
            btnForbid.Size = new Size(195, 51);
            btnForbid.TabIndex = 8;
            btnForbid.Text = "Запретить";
            btnForbid.UseVisualStyleBackColor = true;
            btnForbid.Click += btnForbid_Click;
            // 
            // CounterpartyForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 848);
            Controls.Add(lblTitle);
            Controls.Add(lblInnLabel);
            Controls.Add(txtInn);
            Controls.Add(btnCheck);
            Controls.Add(lblHint);
            Controls.Add(pnlResults);
            Controls.Add(pnlBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CounterpartyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Складской учёт — Проверка контрагента по ИНН";
            pnlResults.ResumeLayout(false);
            pnlResults.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label    lblTitle;
        private Label    lblInnLabel;
        private TextBox  txtInn;
        private Button   btnCheck;
        private Label    lblHint;
        private Panel    pnlResults;
        private Label    lblCompanyNameLabel;
        private TextBox  txtCompanyName;
        private Label    lblInnKppOgrn;
        private Label    lblAddress;
        private Label    lblDirector;
        private Label    lblChecksTitle;
        private CheckBox chkTaxDebtor;
        private Label    lblTaxDebtorNote;
        private CheckBox chkBankrupt;
        private Label    lblBankruptNote;
        private CheckBox chkDisqualified;
        private Label    lblDisqualNote;
        private Label    lblStatusLabel;
        private TextBox  txtStatus;
        private Panel    pnlBottom;
        private Button   btnCancel;
        private Button   btnAllow;
        private Button   btnForbid;
    }
}
