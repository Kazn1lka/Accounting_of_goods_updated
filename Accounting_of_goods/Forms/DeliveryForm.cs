using WinFormsApp1;

namespace Accounting_of_goods
{
    public partial class DeliveryForm : Form
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ICounterpartyService _counterpartyService;
        private readonly CounterpartyForm _counterpartyForm;
        private string _verifiedCompanyName;
        private bool _isInnApproved = false;

        public DeliveryForm(IDeliveryService deliveryService, ICounterpartyService counterpartyService)
        {
            InitializeComponent();
            _deliveryService = deliveryService;
            _counterpartyService = counterpartyService;
            _counterpartyForm = new CounterpartyForm(_counterpartyService);
        }

        private void DeliveryForm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadProductList();
            btnCancel.Click += btnCancel_Click;
            lblInnStatus.Text = "";
        }

        private void SetupGrid()
        {
            if (dgvPreview.Columns.Count == 0)
            {
                dgvPreview.Columns.Add("Article", "Артикул");
                dgvPreview.Columns.Add("Name", "Название");
                dgvPreview.Columns.Add("Size", "Размер");
                dgvPreview.Columns.Add("Quantity", "Кол-во");
                dgvPreview.Columns.Add("Price", "Закупка");
                dgvPreview.Columns.Add("SellingPrice", "Продажа");
                dgvPreview.Columns.Add("ExpiryDate", "Срок годн.");

                var delBtn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    Text = "✕",
                    UseColumnTextForButtonValue = true,
                    Width = 40
                };
                dgvPreview.Columns.Add(delBtn);

                dgvPreview.CellContentClick += dgvPreview_CellContentClick;
            }
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreview.AllowUserToAddRows = false;
        }

        private void dgvPreview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPreview.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (!dgvPreview.Rows[e.RowIndex].IsNewRow)
                    dgvPreview.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void LoadProductList()
        {
            var names = _deliveryService.GetProductNames();
            names.Insert(0, "— Выберите товар —");
            cmbProduct.DataSource = names;
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex <= 0)
                return;
            cmbSize.DataSource = _deliveryService.GetProductSizes(cmbProduct.Text);
            cmbSize.DisplayMember = "Size";
            cmbSize.ValueMember = "Article";
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex <= 0 || numQty.Value <= 0)
                return;

            dgvPreview.Rows.Add(
                cmbSize.SelectedValue?.ToString(),
                cmbProduct.Text,
                cmbSize.Text,
                (int)numQty.Value,
                numPrice.Value,
                numSellingPrice.Value,
                dtpExpiry.Value.ToShortDateString()
            );
        }

        private void UpdateInnStatus(bool? valid, string companyName, string reason = null)
        {
            if (valid == null)
            {
                lblInnStatus.Text      = "";
                lblInnStatus.ForeColor = Color.Gray;
                _verifiedCompanyName   = null;
                _isInnApproved         = false;
            }
            else if (valid == true)
            {
                lblInnStatus.Text      = $"✔ Разрешен ({companyName})";
                lblInnStatus.ForeColor = Color.DarkGreen;
                _verifiedCompanyName   = companyName;
                _isInnApproved         = true;
            }
            else
            {
                string suffix = string.IsNullOrEmpty(reason) ? "" : $" ({reason})";
                lblInnStatus.Text      = $"✖ Запрещен{suffix}";
                lblInnStatus.ForeColor = Color.DarkRed;
                _verifiedCompanyName   = null;
                _isInnApproved         = false;
            }
        }

        private void btnVerifyInn_Click(object sender, EventArgs e)
        {
            string inn = txtSupplierInn.Text.Trim();

            var (isValid, error) = _counterpartyService.ValidateInn(inn);
            if (!isValid)
            {
                MessageBox.Show(error, "Неверный ИНН", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UpdateInnStatus(false, null, "Неверный ИНН");
                return;
            }

            _counterpartyForm.SetInn(inn);
            if (_counterpartyForm.ShowDialog(this) == DialogResult.OK)
            {
                string status = _counterpartyForm.FinalStatus;
                string company = _counterpartyForm.CompanyName;

                if (status == "Разрешен")
                {
                    UpdateInnStatus(true, company);
                }
                else
                {
                    UpdateInnStatus(false, company, "Запрещен");
                }
            }
        }

        private void txtSupplierInn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnVerifyInn_Click(sender, e);
            }
        }

        private void txtSupplierInn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtSupplierInn_TextChanged(object sender, EventArgs e)
        {
            UpdateInnStatus(null, null);
        }

        private void btnConfirmDelivery_Click(object sender, EventArgs e)
        {
            if (dgvPreview.Rows.Count == 0)
            {
                MessageBox.Show("Список поставки пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isInnApproved)
            {
                MessageBox.Show("Поставка запрещена. Поставщик не прошёл проверку ИНН или проверка не выполнена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var items = new List<DeliveryItemDto>();
            foreach (DataGridViewRow row in dgvPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                items.Add(new DeliveryItemDto
                {
                    Article = row.Cells["Article"].Value?.ToString() ?? "",
                    Quantity = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0),
                    PurchasePrice = Convert.ToDecimal(row.Cells["Price"].Value ?? 0),
                    SellingPrice = Convert.ToDecimal(row.Cells["SellingPrice"].Value ?? 0),
                    ExpiryDate = Convert.ToDateTime(row.Cells["ExpiryDate"].Value ?? DateTime.UtcNow).ToUniversalTime()
                });
            }

            try
            {
                _deliveryService.ProcessDelivery(items);
                MessageBox.Show("Поставка успешно оприходована!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
