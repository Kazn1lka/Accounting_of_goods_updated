namespace WinFormsApp1
{
    public partial class CounterpartyForm : Form
    {
        private readonly ICounterpartyService _counterpartyService;

        public CounterpartyForm()
        {
            InitializeComponent();
        }

        public CounterpartyForm(ICounterpartyService counterpartyService) : this()
        {
            _counterpartyService = counterpartyService;
        }
        public void ShowResult(CounterpartyInfo info)
        {
            DisplayResult(info);
        }
        public void SetInn(string inn)
        {
            txtInn.Text = inn ?? "";
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void BtnCheck_Click(object sender, EventArgs e)
        {
            string inn = txtInn.Text.Trim();

            var (isValid, validationError) = _counterpartyService.ValidateInn(inn);
            if (!isValid)
            {
                MessageBox.Show(validationError, "Неверный ИНН",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetLoading(true);
            pnlResults.Visible = false;
            lblHint.Text = "Выполняется запрос…";
            try
            {
                var info = await _counterpartyService.CheckByInnAsync(inn);
                DisplayResult(info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblHint.Text = "Ошибка при проверке.";
            }
            finally
            {
                SetLoading(false);
            }
        }
        private void DisplayResult(CounterpartyInfo info)
        {
            lblCompanyName.Text = info.FullName ?? info.ShortName ?? $"ИНН {info.Inn}";
            lblInnKppOgrn.Text  = $"ИНН: {info.Inn}  КПП: {info.Kpp ?? "—"}  ОГРН: {info.Ogrn ?? "—"}";
            lblAddress.Text     = string.IsNullOrEmpty(info.Address) ? "" : $"Адрес: {info.Address}";
            lblDirector.Text    = string.IsNullOrEmpty(info.DirectorName) ? "" : $"Руководитель: {info.DirectorName}";

            txtStatus.Text = info.StatusDescription ?? "—";

            SetCheckResult(chkTaxDebtor, lblTaxDebtorNote,
                info.IsTaxDebtor, invert: true, info.TaxDebtorCheckError);
            SetCheckResult(chkBankrupt, lblBankruptNote,
                info.IsBankrupt, invert: true, info.BankruptCheckError);
            SetCheckResult(chkDisqualified, lblDisqualNote,
                info.HasDisqualifiedDirectors, invert: true, info.DisqualifiedCheckError);

            lblHint.Text = $"Проверка завершена · {DateTime.Now:HH:mm:ss}";
            pnlResults.Visible = true;
        }
        private static void SetCheckResult(CheckBox chk, Label note,
            bool? value, bool invert, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                chk.Checked   = false;
                chk.ForeColor = SystemColors.ControlText;
                note.Text     = $"({error})";
                return;
            }
            if (value == null)
            {
                chk.Checked   = false;
                chk.ForeColor = Color.Gray;
                note.Text     = "(не проверено)";
                return;
            }
            bool good = invert ? value == false : value == true;
            chk.Checked   = good;
            chk.ForeColor = good ? Color.DarkGreen : Color.DarkRed;
            note.Text     = "";
            }
        private void SetLoading(bool loading)
        {
            btnCheck.Enabled      = !loading;
            btnCheckAgain.Enabled = !loading;
            txtInn.Enabled        = !loading;
            btnCheck.Text      = loading ? "Загрузка…" : "Проверить по API";
            btnCheckAgain.Text = loading ? "Загрузка…" : "Проверить";
        }
    }
}
