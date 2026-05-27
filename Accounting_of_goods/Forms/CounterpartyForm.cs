using System;
using System.Windows.Forms;
using WinFormsApp1.Interfaces;

namespace WinFormsApp1
{
    public partial class CounterpartyForm : Form
    {
        private readonly ICounterpartyService _counterpartyService;

        public string FinalStatus { get; private set; }
        public new string CompanyName => txtCompanyName.Text.Trim();

        public CounterpartyForm()
        {
            InitializeComponent();
            this.Load += CounterpartyForm_Load;
        }

        public CounterpartyForm(ICounterpartyService counterpartyService) : this()
        {
            _counterpartyService = counterpartyService;
        }

        private void CounterpartyForm_Load(object sender, EventArgs e)
        {
            chkTaxDebtor.Text = "Налоговый должник";
            chkBankrupt.Text = "Процедура банкротства";
            chkDisqualified.Text = "Дисквалифицированные директора";

            lblTaxDebtorNote.Visible = false;
            lblBankruptNote.Visible = false;
            lblDisqualNote.Visible = false;
            lblAddress.Visible = false;
            lblDirector.Visible = false;

            chkTaxDebtor.CheckedChanged += (s, ev) => UpdateVerificationStatus();
            chkBankrupt.CheckedChanged += (s, ev) => UpdateVerificationStatus();
            chkDisqualified.CheckedChanged += (s, ev) => UpdateVerificationStatus();
            txtCompanyName.TextChanged += (s, ev) => UpdateVerificationStatus();

            UpdateVerificationStatus();
        }

        public void SetInn(string inn)
        {
            txtInn.Text = inn ?? "";
            if (!string.IsNullOrEmpty(inn))
            {
                BtnCheck_Click(this, EventArgs.Empty);
            }
            else
            {
                pnlResults.Visible = false;
            }
        }

        private void TxtInn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TxtInn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnCheck_Click(sender, e);
            }
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            string inn = txtInn.Text.Trim();

            var (isValid, validationError) = _counterpartyService.ValidateInn(inn);
            if (!isValid)
            {
                MessageBox.Show(validationError, "Неверный ИНН",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblInnKppOgrn.Text = $"ИНН: {inn}      Дата проверки: {DateTime.Now:dd.MM.yyyy HH:mm}";
            chkTaxDebtor.Checked = false;
            chkBankrupt.Checked = false;
            chkDisqualified.Checked = false;
            txtCompanyName.Clear();

            pnlResults.Visible = true;
            lblHint.Text = "";
            txtCompanyName.Focus();

            UpdateVerificationStatus();
        }

        private void UpdateVerificationStatus()
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                txtStatus.Text = "";
                btnAllow.Enabled = false;
                btnForbid.Enabled = false;
                return;
            }

            bool hasRisk = chkTaxDebtor.Checked || chkBankrupt.Checked || chkDisqualified.Checked;
            if (hasRisk)
            {
                txtStatus.Text = "Запрещен";
                btnAllow.Enabled = false;
                btnForbid.Enabled = true;
            }
            else
            {
                txtStatus.Text = "Разрешен";
                btnAllow.Enabled = true;
                btnForbid.Enabled = false;
            }
        }

        private void btnAllow_Click(object sender, EventArgs e)
        {
            FinalStatus = "Разрешен";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnForbid_Click(object sender, EventArgs e)
        {
            FinalStatus = "Запрещен";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
