namespace WinFormsApp1
{
    public partial class ShipmentForm : Form
    {
        private readonly IShipmentService _shipmentService;
        private readonly ICounterpartyService _counterpartyService;
        private readonly IWeatherService _weatherService;
        private readonly CounterpartyForm _counterpartyForm;
        private int _userId;
        private string _currentArticle;
        private decimal _currentPrice;

        private string _verifiedCompanyName;
        private bool _isInnApproved = false;

        public ShipmentForm(IShipmentService shipmentService, ICounterpartyService counterpartyService, IWeatherService weatherService)
        {
            InitializeComponent();
            _shipmentService = shipmentService;
            _counterpartyService = counterpartyService;
            _weatherService = weatherService;
            _counterpartyForm = new CounterpartyForm(_counterpartyService);
        }

        public void Init(string article, int userId)
        {
            _userId = userId;
        }

        private void ShipmentForm_Load(object sender, EventArgs e)
        {
            if (dgvCart.Columns.Count == 0)
            {
                dgvCart.Columns.Add("Article",  "Артикул");
                dgvCart.Columns.Add("Name",     "Название");
                dgvCart.Columns.Add("Size",     "Размер");
                dgvCart.Columns.Add("Quantity", "Кол-во");
                dgvCart.Columns.Add("Price",    "Цена");
                dgvCart.Columns.Add("Sum",      "Сумма");

                var delBtn = new DataGridViewButtonColumn
                {
                    Name = "Delete", Text = "✕",
                    UseColumnTextForButtonValue = true, Width = 35
                };
                dgvCart.Columns.Add(delBtn);
            }
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.RowHeadersVisible = false;

            cmbProduct.DataSource = _shipmentService.GetProductNames();
            cmbProduct.SelectedIndex = -1;

            UpdateInnStatus(null, null);

            txtRegion.KeyDown += txtRegion_KeyDown;
            txtRegion.Leave += txtRegion_Leave;
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem == null) return;
            cmbSize.DataSource = _shipmentService.GetSizesForProduct(cmbProduct.Text);
        }

        private void RefreshAvailableStock()
        {
            if (string.IsNullOrEmpty(cmbProduct.Text) || string.IsNullOrEmpty(cmbSize.Text)) return;

            var details = _shipmentService.GetProductDetails(cmbProduct.Text, cmbSize.Text);
            if (details != null)
            {
                dynamic d = details;
                _currentArticle = d.Article;
                int stock = d.CurrentStock;

                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.Cells["Article"].Value?.ToString() == _currentArticle)
                        stock -= Convert.ToInt32(row.Cells["Quantity"].Value);
                }

                textBox2.Text = stock.ToString();
                _currentPrice = d.Price;
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSize.SelectedItem == null) return;
            RefreshAvailableStock();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentArticle)) return;

            if (!int.TryParse(textBox1.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int available = int.Parse(textBox2.Text);
            if (qty > available)
            {
                MessageBox.Show("Недостаточно товара на складе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal sum = qty * _currentPrice;
            dgvCart.Rows.Add(_currentArticle, cmbProduct.Text, cmbSize.Text, qty, _currentPrice, sum);
            UpdateTotalSum();

            textBox1.Clear();
            RefreshAvailableStock();
        }

        private void UpdateTotalSum()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Sum"].Value != null)
                    total += Convert.ToDecimal(row.Cells["Sum"].Value);
            }
            txtTotalSum.Text = total.ToString();
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCart.Columns[e.ColumnIndex].Name == "Delete" && e.RowIndex >= 0)
            {
                dgvCart.Rows.RemoveAt(e.RowIndex);
                UpdateTotalSum();
                RefreshAvailableStock();
            }
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
            string inn = txtInn.Text.Trim();

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

        private void txtInn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnVerifyInn_Click(sender, e);
            }
        }

        private void txtInn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtInn_TextChanged(object sender, EventArgs e)
        {
            UpdateInnStatus(null, null);
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Список отгрузки пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isInnApproved)
            {
                MessageBox.Show("Отгрузка запрещена. Получатель не прошёл проверку ИНН или проверка не выполнена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string recipient = _verifiedCompanyName ?? txtInn.Text.Trim();

            if (string.IsNullOrWhiteSpace(recipient))
            {
                MessageBox.Show("Укажите ИНН получателя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string region = txtRegion.Text.Trim();
            if (string.IsNullOrWhiteSpace(region))
            {
                MessageBox.Show("Укажите регион отгрузки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var items = new List<ShipmentItemDto>();
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Article"].Value == null) continue;
                items.Add(new ShipmentItemDto
                {
                    Article  = row.Cells["Article"].Value.ToString(),
                    Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                    Price    = Convert.ToDecimal(row.Cells["Price"].Value)
                });
            }

            try
            {
                _shipmentService.ProcessShipment(_userId, recipient, region, items);
                MessageBox.Show("Отгрузка успешно проведена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                UpdateWeatherForecast();
            }
        }

        private void txtRegion_Leave(object sender, EventArgs e)
        {
            UpdateWeatherForecast();
        }

        private async void UpdateWeatherForecast()
        {
            string city = txtRegion.Text.Trim();
            if (string.IsNullOrEmpty(city))
            {
                lblWeatherAlert.Text = "Введите регион и нажмите Enter";
                lblWeatherAlert.ForeColor = Color.Black;
                lblWeatherAlert.BackColor = SystemColors.Control;
                return;
            }

            lblWeatherAlert.Text = "Загрузка погоды...";
            lblWeatherAlert.ForeColor = Color.Gray;
            lblWeatherAlert.BackColor = SystemColors.Control;

            try
            {
                var result = await _weatherService.GetForecastAsync(city);
                if (result == null || !result.Success)
                {
                    lblWeatherAlert.Text = "Не удалось загрузить погоду";
                    lblWeatherAlert.ForeColor = Color.DarkRed;
                    lblWeatherAlert.BackColor = Color.MistyRose;
                    return;
                }

                bool alertNeeded = false;
                string alertText = "";

                for (int i = 0; i < result.Days.Count; i++)
                {
                    var day = result.Days[i];
                    string dayName = i == 0 ? "Сегодня" : i == 1 ? "Завтра" : "Через 2 дня";

                    if (day.TempMin < WeatherSettings.GlobalFrostThreshold)
                    {
                        alertText = $"{dayName} мороз {day.TempMin}°С\r\nтребуется термоконтейнер";
                        alertNeeded = true;
                        break;
                    }
                    else if (day.TempMax > WeatherSettings.GlobalHeatThreshold)
                    {
                        alertText = $"{dayName} жара +{day.TempMax}°C\r\nтребуется термоконтейнер";
                        alertNeeded = true;
                        break;
                    }
                }

                if (!alertNeeded)
                {
                    lblWeatherAlert.Text = "Условия доставки в норме.\r\nСпециальных мер не требуется.";
                    lblWeatherAlert.ForeColor = Color.DarkGreen;
                    lblWeatherAlert.BackColor = Color.Honeydew;
                }
                else
                {
                    lblWeatherAlert.Text = alertText;
                    lblWeatherAlert.ForeColor = Color.DarkRed;
                    lblWeatherAlert.BackColor = Color.MistyRose;
                }
            }
            catch
            {
                lblWeatherAlert.Text = "Ошибка загрузки погоды";
                lblWeatherAlert.ForeColor = Color.DarkRed;
                lblWeatherAlert.BackColor = Color.MistyRose;
            }
        }
    }
}
