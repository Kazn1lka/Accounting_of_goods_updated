namespace Accounting_of_goods
{
    public partial class DeliveryForm : Form
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryForm(IDeliveryService deliveryService)
        {
            InitializeComponent();
            _deliveryService = deliveryService;
        }

        private void DeliveryForm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadProductList();
            btnCancel.Click += btnCancel_Click;
        }

        private void SetupGrid()
        {
            if (dgvPreview.Columns.Count == 0)
            {
                dgvPreview.Columns.Add("Article",      "Артикул");
                dgvPreview.Columns.Add("Name",         "Название");
                dgvPreview.Columns.Add("Size",         "Размер");
                dgvPreview.Columns.Add("Quantity",     "Кол-во");
                dgvPreview.Columns.Add("Price",        "Закупка");
                dgvPreview.Columns.Add("SellingPrice", "Продажа");
                dgvPreview.Columns.Add("ExpiryDate",   "Срок годн.");

                var delBtn = new DataGridViewButtonColumn
                {
                    Name = "Delete", Text = "✕",
                    UseColumnTextForButtonValue = true, Width = 40
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
            if (cmbProduct.SelectedIndex <= 0) return;
            cmbSize.DataSource    = _deliveryService.GetProductSizes(cmbProduct.Text);
            cmbSize.DisplayMember = "Size";
            cmbSize.ValueMember   = "Article";
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex <= 0 || numQty.Value <= 0) return;

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

        private void btnConfirmDelivery_Click(object sender, EventArgs e)
        {
            if (dgvPreview.Rows.Count == 0)
            {
                MessageBox.Show("Список поставки пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var items = new List<DeliveryItemDto>();
            foreach (DataGridViewRow row in dgvPreview.Rows)
            {
                if (row.IsNewRow) continue;

                items.Add(new DeliveryItemDto
                {
                    Article       = row.Cells["Article"].Value?.ToString() ?? "",
                    Quantity      = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0),
                    PurchasePrice = Convert.ToDecimal(row.Cells["Price"].Value ?? 0),
                    SellingPrice  = Convert.ToDecimal(row.Cells["SellingPrice"].Value ?? 0),
                    ExpiryDate    = Convert.ToDateTime(row.Cells["ExpiryDate"].Value ?? DateTime.UtcNow).ToUniversalTime()
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
