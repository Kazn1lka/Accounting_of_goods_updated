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
            lblTitle         = new Label();
            lblInnLabel      = new Label();
            txtInn           = new TextBox();
            btnCheck         = new Button();
            lblHint          = new Label();
            pnlResults       = new Panel();
            lblCompanyName   = new Label();
            lblInnKppOgrn    = new Label();
            lblAddress       = new Label();
            lblDirector      = new Label();
            lblChecksTitle   = new Label();
            chkTaxDebtor     = new CheckBox();
            lblTaxDebtorNote = new Label();
            chkBankrupt      = new CheckBox();
            lblBankruptNote  = new Label();
            chkDisqualified  = new CheckBox();
            lblDisqualNote   = new Label();
            lblStatusLabel   = new Label();
            txtStatus        = new TextBox();
            pnlBottom        = new Panel();
            btnClose         = new Button();
            btnCheckAgain    = new Button();

            pnlResults.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();

            // ── lblTitle ──────────────────────────────────────────────────────────
            lblTitle.Dock      = DockStyle.Top;
            lblTitle.Font      = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Height    = 48;
            lblTitle.Name      = "lblTitle";
            lblTitle.Padding   = new Padding(10, 10, 0, 0);
            lblTitle.Text      = "Проверка контрагента по ИНН";

            // ── lblInnLabel ───────────────────────────────────────────────────────
            lblInnLabel.AutoSize = true;
            lblInnLabel.Font     = new Font("Arial", 8.25F);
            lblInnLabel.Location = new Point(20, 58);
            lblInnLabel.Name     = "lblInnLabel";
            lblInnLabel.Text     = "ИНН контрагента:";

            // ── txtInn ────────────────────────────────────────────────────────────
            txtInn.BorderStyle      = BorderStyle.FixedSingle;
            txtInn.Font             = new Font("Arial", 8.25F);
            txtInn.Location         = new Point(20, 78);
            txtInn.MaxLength        = 12;
            txtInn.Name             = "txtInn";
            txtInn.PlaceholderText  = "10 или 12 цифр";
            txtInn.Size             = new Size(250, 23);
            txtInn.TabIndex         = 0;
            txtInn.KeyPress        += TxtInn_KeyPress;
            txtInn.KeyDown         += TxtInn_KeyDown;

            // ── btnCheck ──────────────────────────────────────────────────────────
            btnCheck.FlatStyle = FlatStyle.System;
            btnCheck.Font      = new Font("Arial", 8.25F);
            btnCheck.Location  = new Point(280, 76);
            btnCheck.Name      = "btnCheck";
            btnCheck.Size      = new Size(140, 27);
            btnCheck.TabIndex  = 1;
            btnCheck.Text      = "Проверить по API";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click    += BtnCheck_Click;

            // ── lblHint ───────────────────────────────────────────────────────────
            lblHint.AutoSize  = true;
            lblHint.Font      = new Font("Arial", 7.5F, FontStyle.Italic);
            lblHint.ForeColor = Color.Gray;
            lblHint.Location  = new Point(20, 108);
            lblHint.Name      = "lblHint";
            lblHint.Text      = "Введите ИНН и нажмите «Проверить по API» (или Enter)";

            // ── pnlResults ────────────────────────────────────────────────────────
            pnlResults.Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlResults.Location = new Point(0, 130);
            pnlResults.Name     = "pnlResults";
            pnlResults.Padding  = new Padding(12, 4, 12, 4);
            pnlResults.Size     = new Size(620, 345);
            pnlResults.Visible  = false;
            pnlResults.Controls.AddRange(new Control[]
            {
                lblCompanyName, lblInnKppOgrn, lblAddress, lblDirector,
                lblChecksTitle,
                chkTaxDebtor, lblTaxDebtorNote,
                chkBankrupt, lblBankruptNote,
                chkDisqualified, lblDisqualNote,
                lblStatusLabel, txtStatus
            });

            // Реквизиты
            lblCompanyName.AutoSize    = true;
            lblCompanyName.Font        = new Font("Arial", 9.75F, FontStyle.Bold);
            lblCompanyName.Location    = new Point(20, 6);
            lblCompanyName.MaximumSize = new Size(580, 0);
            lblCompanyName.Name        = "lblCompanyName";
            lblCompanyName.Text        = "—";

            lblInnKppOgrn.AutoSize  = true;
            lblInnKppOgrn.Font      = new Font("Arial", 7.8F);
            lblInnKppOgrn.ForeColor = Color.DimGray;
            lblInnKppOgrn.Location  = new Point(20, 32);
            lblInnKppOgrn.Name      = "lblInnKppOgrn";
            lblInnKppOgrn.Text      = "ИНН — / КПП — / ОГРН —";

            lblAddress.AutoSize    = true;
            lblAddress.Font        = new Font("Arial", 7.5F);
            lblAddress.ForeColor   = Color.DimGray;
            lblAddress.Location    = new Point(20, 52);
            lblAddress.MaximumSize = new Size(580, 0);
            lblAddress.Name        = "lblAddress";
            lblAddress.Text        = "";

            lblDirector.AutoSize  = true;
            lblDirector.Font      = new Font("Arial", 7.5F);
            lblDirector.ForeColor = Color.DimGray;
            lblDirector.Location  = new Point(20, 68);
            lblDirector.Name      = "lblDirector";
            lblDirector.Text      = "";

            // Результаты проверок
            lblChecksTitle.AutoSize  = true;
            lblChecksTitle.Font      = new Font("Arial", 8.25F, FontStyle.Bold);
            lblChecksTitle.Location  = new Point(20, 92);
            lblChecksTitle.Name      = "lblChecksTitle";
            lblChecksTitle.Text      = "Результаты проверок:";

            chkTaxDebtor.AutoSize  = true;
            chkTaxDebtor.Enabled   = false;
            chkTaxDebtor.Font      = new Font("Arial", 8.25F);
            chkTaxDebtor.Location  = new Point(20, 112);
            chkTaxDebtor.Name      = "chkTaxDebtor";
            chkTaxDebtor.Text      = "Не налоговый должник";

            lblTaxDebtorNote.AutoSize  = true;
            lblTaxDebtorNote.Font      = new Font("Arial", 7.5F, FontStyle.Italic);
            lblTaxDebtorNote.ForeColor = Color.Gray;
            lblTaxDebtorNote.Location  = new Point(320, 115);
            lblTaxDebtorNote.Name      = "lblTaxDebtorNote";
            lblTaxDebtorNote.Text      = "";

            chkBankrupt.AutoSize  = true;
            chkBankrupt.Enabled   = false;
            chkBankrupt.Font      = new Font("Arial", 8.25F);
            chkBankrupt.Location  = new Point(20, 136);
            chkBankrupt.Name      = "chkBankrupt";
            chkBankrupt.Text      = "Не в реестре банкротов";

            lblBankruptNote.AutoSize  = true;
            lblBankruptNote.Font      = new Font("Arial", 7.5F, FontStyle.Italic);
            lblBankruptNote.ForeColor = Color.Gray;
            lblBankruptNote.Location  = new Point(320, 139);
            lblBankruptNote.Name      = "lblBankruptNote";
            lblBankruptNote.Text      = "";

            chkDisqualified.AutoSize  = true;
            chkDisqualified.Enabled   = false;
            chkDisqualified.Font      = new Font("Arial", 8.25F);
            chkDisqualified.Location  = new Point(20, 160);
            chkDisqualified.Name      = "chkDisqualified";
            chkDisqualified.Text      = "Все директора не дисквалифицированы";

            lblDisqualNote.AutoSize  = true;
            lblDisqualNote.Font      = new Font("Arial", 7.5F, FontStyle.Italic);
            lblDisqualNote.ForeColor = Color.Gray;
            lblDisqualNote.Location  = new Point(320, 163);
            lblDisqualNote.Name      = "lblDisqualNote";
            lblDisqualNote.Text      = "";

            // Статус
            lblStatusLabel.AutoSize = true;
            lblStatusLabel.Font     = new Font("Arial", 8.25F, FontStyle.Bold);
            lblStatusLabel.Location = new Point(20, 196);
            lblStatusLabel.Name     = "lblStatusLabel";
            lblStatusLabel.Text     = "Актуальный статус компании:";

            txtStatus.BorderStyle = BorderStyle.FixedSingle;
            txtStatus.Font        = new Font("Arial", 8.25F);
            txtStatus.Location    = new Point(20, 214);
            txtStatus.Name        = "txtStatus";
            txtStatus.ReadOnly    = true;
            txtStatus.Size        = new Size(580, 23);
            txtStatus.TabStop     = false;

            // ── pnlBottom ─────────────────────────────────────────────────────────
            pnlBottom.Dock    = DockStyle.Bottom;
            pnlBottom.Height  = 55;
            pnlBottom.Name    = "pnlBottom";
            pnlBottom.Padding = new Padding(10, 8, 10, 8);
            pnlBottom.Controls.AddRange(new Control[] { btnClose, btnCheckAgain });

            btnClose.Anchor   = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.FlatStyle = FlatStyle.System;
            btnClose.Font     = new Font("Arial", 8.25F);
            btnClose.Location = new Point(480, 12);
            btnClose.Name     = "btnClose";
            btnClose.Size     = new Size(120, 32);
            btnClose.TabIndex = 10;
            btnClose.Text     = "Закрыть";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click   += btnClose_Click;

            btnCheckAgain.Anchor   = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCheckAgain.FlatStyle = FlatStyle.System;
            btnCheckAgain.Font     = new Font("Arial", 8.25F);
            btnCheckAgain.Location = new Point(350, 12);
            btnCheckAgain.Name     = "btnCheckAgain";
            btnCheckAgain.Size     = new Size(120, 32);
            btnCheckAgain.TabIndex = 9;
            btnCheckAgain.Text     = "Проверить";
            btnCheckAgain.UseVisualStyleBackColor = true;
            btnCheckAgain.Click   += BtnCheck_Click;

            // ── CounterpartyForm ──────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(620, 530);
            FormBorderStyle     = FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            Name                = "CounterpartyForm";
            StartPosition       = FormStartPosition.CenterParent;
            Text                = "Складской учёт — Проверка контрагента по ИНН";

            Controls.Add(lblTitle);
            Controls.Add(lblInnLabel);
            Controls.Add(txtInn);
            Controls.Add(btnCheck);
            Controls.Add(lblHint);
            Controls.Add(pnlResults);
            Controls.Add(pnlBottom);

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
        private Label    lblCompanyName;
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
        private Button   btnClose;
        private Button   btnCheckAgain;
    }
}
