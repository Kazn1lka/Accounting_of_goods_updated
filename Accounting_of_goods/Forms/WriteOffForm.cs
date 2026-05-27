namespace WinFormsApp1
{
    public partial class WriteOffForm : Form
    {
        private readonly IWriteOffService _writeOffService;
        private int _userId;
        private List<ExpiredSupplyItem> _expiredItems = new List<ExpiredSupplyItem>();

        public WriteOffForm(IWriteOffService writeOffService)
        {
            InitializeComponent();
            _writeOffService = writeOffService;
        }

        public void Init(int userId, string article = null)
        {
            _userId = userId;
        }

        private void WriteOffForm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadExpiredItems();
        }

        private void SetupGrid()
        {
            dgvExpired.Columns.Add("SupplyId", "ID партии");
            dgvExpired.Columns.Add("Article", "Артикул");
            dgvExpired.Columns.Add("ProductName", "Название");
            dgvExpired.Columns.Add("Size", "Размер");
            dgvExpired.Columns.Add("Quantity", "Остаток");
            dgvExpired.Columns.Add("ExpiryDate", "Срок годности");
            dgvExpired.Columns.Add("DaysExpired", "Просрочено (дн.)");

            dgvExpired.Columns["SupplyId"].Visible = false;
            dgvExpired.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpired.RowHeadersVisible = false;
            dgvExpired.SelectionChanged += dgvExpired_SelectionChanged;
        }

        private void LoadExpiredItems()
        {
            _expiredItems = _writeOffService.GetExpiredItems();
            dgvExpired.Rows.Clear();

            if (_expiredItems.Count == 0)
            {
                lblStatus.Text = "Просроченных товаров не найдено.";
                lblTotalLoss.Text = "Общий убыток: 0,00 " + Accounting_of_goods.CurrencyConverter.CurrentCurrency;
                btnWriteOff.Enabled = false;
                return;
            }

            lblStatus.Text = "Найдено: " + _expiredItems.Count + " поз.";
            btnWriteOff.Enabled = true;

            for (int i = 0; i < _expiredItems.Count; i++)
            {
                var item = _expiredItems[i];
                dgvExpired.Rows.Add(
                    item.SupplyId,
                    item.Article,
                    item.ProductName,
                    item.Size,
                    item.Quantity,
                    item.ExpiryDate.ToLocalTime().ToShortDateString(),
                    item.DaysExpired
                );

                if (item.DaysExpired > 30)
                    dgvExpired.Rows[i].DefaultCellStyle.ForeColor = Color.DarkRed;
                else
                    dgvExpired.Rows[i].DefaultCellStyle.ForeColor = Color.OrangeRed;
            }

            UpdateTotalLoss();
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (dgvExpired.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строки для списания.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason = txtReason.Text.Trim();
            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Укажите причину списания.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = _userId > 0 ? _userId : 1;
            int successCount = 0;
            var errors = new List<string>();

            foreach (DataGridViewRow row in dgvExpired.SelectedRows)
            {
                if (!int.TryParse(row.Cells["SupplyId"].Value?.ToString(), out int supplyId))
                    continue;

                if (!int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int qty))
                    continue;

                try
                {
                    _writeOffService.ProcessWriteOffBySupply(userId, supplyId, qty, reason);
                    successCount++;
                }
                catch (Exception ex)
                {
                    string name = row.Cells["ProductName"].Value?.ToString();
                    errors.Add(name + ": " + ex.Message);
                }
            }

            if (errors.Count > 0)
            {
                string errText = "Часть позиций не удалось списать:\n" + string.Join("\n", errors);
                MessageBox.Show(errText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (successCount > 0)
            {
                MessageBox.Show("Списано позиций: " + successCount, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadExpiredItems();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvExpired_SelectionChanged(object sender, EventArgs e)
        {
            UpdateTotalLoss();
        }

        private void UpdateTotalLoss()
        {
            decimal totalLoss = 0;
            decimal selectedLoss = 0;
            string currency = Accounting_of_goods.CurrencyConverter.CurrentCurrency;

            foreach (var item in _expiredItems)
            {
                totalLoss += Accounting_of_goods.CurrencyConverter.ConvertPrice(item.PurchasePrice) * item.Quantity;
            }

            foreach (DataGridViewRow row in dgvExpired.SelectedRows)
            {
                if (row.Cells["SupplyId"].Value != null && int.TryParse(row.Cells["SupplyId"].Value.ToString(), out int supplyId))
                {
                    var item = _expiredItems.FirstOrDefault(i => i.SupplyId == supplyId);
                    if (item != null)
                    {
                        selectedLoss += Accounting_of_goods.CurrencyConverter.ConvertPrice(item.PurchasePrice) * item.Quantity;
                    }
                }
            }

            lblTotalLoss.Text = $"Общий убыток: {totalLoss:N2} {currency} (выбрано: {selectedLoss:N2} {currency})";
        }
    }
}
