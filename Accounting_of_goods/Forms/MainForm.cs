namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly IProductService _productService;
        private User _loggedInUser;

        public MainForm(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
        }

        public void SetUser(User user)
        {
            _loggedInUser = user;
            this.FormClosed += (s, e) => Application.Exit();

            bool isAdmin = _loggedInUser.RoleId == 1;
            btnShipment.Visible = !isAdmin;
            btnAddProduct.Visible = isAdmin;
            btnHistory.Visible = isAdmin;
            btnManageCategories.Visible = isAdmin;

            LoadData();
            AddSearchIcon(txtSearch);
        }

        private void LoadData(string searchText = "")
        {
            dgvProducts.DataSource = _productService.GetProductsForGrid(searchText);

            if (dgvProducts.Columns["ID_Поставки"] != null)
                dgvProducts.Columns["ID_Поставки"].Visible = false;

            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.RowHeadersVisible = false;

            if (dgvProducts.Columns["Цена"] != null)
                dgvProducts.Columns["Цена"].HeaderText = $"Цена ({CurrencyConverter.CurrentCurrency})";

            EnsureActionButtons();

            if (_loggedInUser != null)
                ShowEditDeleteButtons(_loggedInUser.RoleId == 1);
        }

        private void EnsureActionButtons()
        {
            if (dgvProducts.Columns["EditColumn"] == null)
            {
                var editCol = new DataGridViewButtonColumn { Name = "EditColumn", HeaderText = "", Text = "✏️", UseColumnTextForButtonValue = true, Width = 35 };
                dgvProducts.Columns.Add(editCol);
            }

            if (dgvProducts.Columns["DeleteColumn"] == null)
            {
                var delCol = new DataGridViewButtonColumn { Name = "DeleteColumn", HeaderText = "", Text = "🗑️", UseColumnTextForButtonValue = true, Width = 35 };
                dgvProducts.Columns.Add(delCol);
            }
        }

        private void ShowEditDeleteButtons(bool visible)
        {
            if (dgvProducts.Columns["EditColumn"] != null) dgvProducts.Columns["EditColumn"].Visible = visible;
            if (dgvProducts.Columns["DeleteColumn"] != null) dgvProducts.Columns["DeleteColumn"].Visible = visible;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int supplyId = (int)dgvProducts.Rows[e.RowIndex].Cells["ID_Поставки"].Value;
            string article = dgvProducts.Rows[e.RowIndex].Cells["Артикул"].Value.ToString();

            if (dgvProducts.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                if (MessageBox.Show("Удалить эту партию товара?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _productService.DeleteSupply(supplyId);
                    LoadData();
                }
            }
            else if (dgvProducts.Columns[e.ColumnIndex].Name == "EditColumn")
            {
                var editForm = Program.ServiceProvider.GetRequiredService<ProductEditForm>();
                editForm.Init(article);
                editForm.ShowDialog();
                LoadData();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var addForm = Program.ServiceProvider.GetRequiredService<ProductAddForm>();
            addForm.ShowDialog();
            LoadData();
        }

        private void btnShipment_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                string article = dgvProducts.CurrentRow.Cells["Артикул"].Value.ToString();
                var shipForm = Program.ServiceProvider.GetRequiredService<ShipmentForm>();
                shipForm.Init(article, _loggedInUser.Id);
                shipForm.ShowDialog();
                LoadData();
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            Program.ServiceProvider.GetRequiredService<HistoryForm>().ShowDialog();
        }

        private void btnHeatMap_Click(object sender, EventArgs e)
        {
            Program.ServiceProvider.GetRequiredService<HeatMapForm>().ShowDialog();
        }

        private void btnWeather_Click(object sender, EventArgs e)
        {
            Program.ServiceProvider.GetRequiredService<WeatherForm>().ShowDialog();
        }

        private void btnSupply_Click(object sender, EventArgs e)
        {
            Program.ServiceProvider.GetRequiredService<DeliveryForm>().ShowDialog();
            LoadData();
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            string article = null;
            if (dgvProducts.CurrentRow != null)
            {
                article = dgvProducts.CurrentRow.Cells["Артикул"].Value?.ToString();
            }

            var writeOffForm = Program.ServiceProvider.GetRequiredService<WriteOffForm>();
            writeOffForm.Init(_loggedInUser?.Id ?? 1, article);
            writeOffForm.ShowDialog();
            LoadData();
        }

        private void категорииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ServiceProvider.GetRequiredService<dgvCategories>().ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadData(txtSearch.Text);

        private async void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCurrency.SelectedItem == null) return;
            await CurrencyConverter.ChangeCurrencyAsync(cmbCurrency.SelectedItem.ToString());
            LoadData();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void сменитьАккаунтToolStripMenuItem_Click(object sender, EventArgs e) => Application.Restart();

        private void AddSearchIcon(TextBox tb)
        {
            Label searchIcon = new Label
            {
                Text = "🔍",
                AutoSize = true,
                BackColor = tb.BackColor,
                ForeColor = Color.Gray,
                Cursor = Cursors.IBeam,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            searchIcon.Font = new Font("Segoe UI Emoji", 9F);
            this.Controls.Add(searchIcon);
            searchIcon.BringToFront();

            this.Layout += (s, e) =>
            {
                searchIcon.Location = new Point(
                    tb.Right - searchIcon.Width - 6,
                    tb.Top + (tb.Height - searchIcon.Height) / 2
                );
            };

            searchIcon.Click += (s, e) => tb.Focus();
        }
    }
}
