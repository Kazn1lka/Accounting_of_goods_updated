namespace WinFormsApp1
{
    partial class ShipmentForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            lblTitle = new Label();
            cmbProduct = new ComboBox();
            cmbSize = new ComboBox();
            lblAvailableStock = new Label();
            btnAddToCart = new Button();
            dgvCart = new DataGridView();
            btnConfirm = new Button();
            btnCancel = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            lblInnLabel = new Label();
            txtInn = new TextBox();
            btnVerifyInn = new Button();
            lblInnStatus = new Label();
            label6 = new Label();
            txtTotalSum = new TextBox();
            grpWeather = new GroupBox();
            lblRegionTitle = new Label();
            txtRegion = new TextBox();
            lblWeatherAlert = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            grpWeather.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(54, 125);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(473, 44);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Формирование отгрузки";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbProduct
            // 
            cmbProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.Font = new Font("Arial", 7.8F);
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(169, 221);
            cmbProduct.Margin = new Padding(6);
            cmbProduct.MaximumSize = new Size(379, 0);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(314, 32);
            cmbProduct.TabIndex = 1;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            // 
            // cmbSize
            // 
            cmbSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSize.Font = new Font("Arial", 7.8F);
            cmbSize.FormattingEnabled = true;
            cmbSize.Location = new Point(169, 290);
            cmbSize.Margin = new Padding(6);
            cmbSize.MaximumSize = new Size(379, 0);
            cmbSize.Name = "cmbSize";
            cmbSize.Size = new Size(314, 32);
            cmbSize.TabIndex = 2;
            cmbSize.SelectedIndexChanged += cmbSize_SelectedIndexChanged;
            // 
            // lblAvailableStock
            // 
            lblAvailableStock.AutoSize = true;
            lblAvailableStock.Font = new Font("Arial", 7.8F);
            lblAvailableStock.Location = new Point(93, 446);
            lblAvailableStock.Margin = new Padding(6, 0, 6, 0);
            lblAvailableStock.Name = "lblAvailableStock";
            lblAvailableStock.Size = new Size(209, 24);
            lblAvailableStock.TabIndex = 4;
            lblAvailableStock.Text = "Доступно на складе:";
            // 
            // btnAddToCart
            // 
            btnAddToCart.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnAddToCart.Location = new Point(127, 525);
            btnAddToCart.Margin = new Padding(6);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(332, 61);
            btnAddToCart.TabIndex = 5;
            btnAddToCart.Text = "Добавить в список";
            btnAddToCart.UseVisualStyleBackColor = true;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvCart.DefaultCellStyle = dataGridViewCellStyle1;
            dgvCart.Location = new Point(561, 21);
            dgvCart.Margin = new Padding(6);
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersWidth = 82;
            dgvCart.Size = new Size(925, 714);
            dgvCart.TabIndex = 6;
            dgvCart.CellContentClick += dgvCart_CellContentClick;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnConfirm.Location = new Point(1138, 855);
            btnConfirm.Margin = new Padding(6);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(302, 75);
            btnConfirm.TabIndex = 7;
            btnConfirm.Text = "Подтвердить отгрузку";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.Location = new Point(905, 855);
            btnCancel.Margin = new Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(214, 75);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(169, 362);
            textBox1.Margin = new Padding(6, 3, 6, 3);
            textBox1.MaximumSize = new Size(379, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(314, 39);
            textBox1.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 7.8F);
            label1.Location = new Point(93, 224);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(66, 24);
            label1.TabIndex = 10;
            label1.Text = "Товар";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 7.8F);
            label2.Location = new Point(84, 298);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 24);
            label2.TabIndex = 11;
            label2.Text = "Размер";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 7.8F);
            label3.Location = new Point(91, 370);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(75, 24);
            label3.TabIndex = 12;
            label3.Text = "Кол-во";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 7.8F);
            label4.Location = new Point(393, 446);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(40, 24);
            label4.TabIndex = 13;
            label4.Text = "шт.";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(315, 437);
            textBox2.Margin = new Padding(6, 3, 6, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(66, 39);
            textBox2.TabIndex = 14;
            // 
            // lblInnLabel
            // 
            lblInnLabel.AutoSize = true;
            lblInnLabel.Font = new Font("Arial", 7.8F);
            lblInnLabel.Location = new Point(562, 768);
            lblInnLabel.Margin = new Padding(6, 0, 6, 0);
            lblInnLabel.Name = "lblInnLabel";
            lblInnLabel.Size = new Size(175, 24);
            lblInnLabel.TabIndex = 20;
            lblInnLabel.Text = "ИНН получателя:";
            // 
            // txtInn
            // 
            txtInn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtInn.Font = new Font("Arial", 8.25F);
            txtInn.Location = new Point(749, 764);
            txtInn.Margin = new Padding(6, 3, 6, 3);
            txtInn.MaxLength = 12;
            txtInn.Name = "txtInn";
            txtInn.PlaceholderText = "10 или 12 цифр";
            txtInn.Size = new Size(190, 33);
            txtInn.TabIndex = 15;
            txtInn.TextChanged += txtInn_TextChanged;
            txtInn.KeyDown += txtInn_KeyDown;
            txtInn.KeyPress += txtInn_KeyPress;
            // 
            // btnVerifyInn
            // 
            btnVerifyInn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVerifyInn.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnVerifyInn.Location = new Point(986, 761);
            btnVerifyInn.Margin = new Padding(4);
            btnVerifyInn.Name = "btnVerifyInn";
            btnVerifyInn.Size = new Size(149, 43);
            btnVerifyInn.TabIndex = 16;
            btnVerifyInn.Text = "Проверить";
            btnVerifyInn.UseVisualStyleBackColor = true;
            btnVerifyInn.Click += btnVerifyInn_Click;
            // 
            // lblInnStatus
            // 
            lblInnStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblInnStatus.Font = new Font("Arial", 7.8F, FontStyle.Italic);
            lblInnStatus.Location = new Point(562, 808);
            lblInnStatus.Name = "lblInnStatus";
            lblInnStatus.Size = new Size(555, 24);
            lblInnStatus.TabIndex = 21;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(199, 628);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(192, 32);
            label6.TabIndex = 17;
            label6.Text = "Итоговая сумма";
            // 
            // txtTotalSum
            // 
            txtTotalSum.Location = new Point(199, 678);
            txtTotalSum.Margin = new Padding(5);
            txtTotalSum.Name = "txtTotalSum";
            txtTotalSum.ReadOnly = true;
            txtTotalSum.Size = new Size(201, 39);
            txtTotalSum.TabIndex = 18;
            // 
            // grpWeather
            // 
            grpWeather.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            grpWeather.Controls.Add(lblRegionTitle);
            grpWeather.Controls.Add(txtRegion);
            grpWeather.Controls.Add(lblWeatherAlert);
            grpWeather.Font = new Font("Arial", 9F, FontStyle.Bold);
            grpWeather.Location = new Point(52, 743);
            grpWeather.Name = "grpWeather";
            grpWeather.Size = new Size(475, 205);
            grpWeather.TabIndex = 22;
            grpWeather.TabStop = false;
            grpWeather.Text = "Геолокация и погода";
            // 
            // lblRegionTitle
            // 
            lblRegionTitle.AutoSize = true;
            lblRegionTitle.Font = new Font("Arial", 8.25F);
            lblRegionTitle.Location = new Point(12, 30);
            lblRegionTitle.Name = "lblRegionTitle";
            lblRegionTitle.Size = new Size(88, 25);
            lblRegionTitle.TabIndex = 0;
            lblRegionTitle.Text = "Регион:";
            // 
            // txtRegion
            // 
            txtRegion.Font = new Font("Arial", 8.25F);
            txtRegion.Location = new Point(112, 27);
            txtRegion.Name = "txtRegion";
            txtRegion.Size = new Size(349, 33);
            txtRegion.TabIndex = 1;
            // 
            // lblWeatherAlert
            // 
            lblWeatherAlert.BorderStyle = BorderStyle.FixedSingle;
            lblWeatherAlert.Font = new Font("Arial", 8F);
            lblWeatherAlert.Location = new Point(12, 63);
            lblWeatherAlert.Name = "lblWeatherAlert";
            lblWeatherAlert.Padding = new Padding(3);
            lblWeatherAlert.Size = new Size(450, 99);
            lblWeatherAlert.TabIndex = 2;
            lblWeatherAlert.Text = "Введите регион и нажмите Enter";
            // 
            // ShipmentForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1485, 960);
            Controls.Add(txtTotalSum);
            Controls.Add(label6);
            Controls.Add(lblInnStatus);
            Controls.Add(grpWeather);
            Controls.Add(btnVerifyInn);
            Controls.Add(txtInn);
            Controls.Add(lblInnLabel);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(dgvCart);
            Controls.Add(btnAddToCart);
            Controls.Add(lblAvailableStock);
            Controls.Add(cmbSize);
            Controls.Add(cmbProduct);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6);
            MaximizeBox = false;
            Name = "ShipmentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Складской учёт — Оформление отгрузки";
            Load += ShipmentForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            grpWeather.ResumeLayout(false);
            grpWeather.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label          lblTitle;
        private ComboBox       cmbProduct;
        private ComboBox       cmbSize;
        private Button         btnAddToCart;
        private DataGridView   dgvCart;
        private Button         btnConfirm;
        private Button         btnCancel;
        private TextBox        textBox1;
        internal Label         lblAvailableStock;
        private Label          label1;
        private Label          label2;
        private Label          label3;
        internal Label         label4;
        private TextBox        textBox2;
        // ИНН-строка
        private Label          lblInnLabel;
        private TextBox        txtInn;
        private Button         btnVerifyInn;
        private Label          lblInnStatus;
        private GroupBox       grpWeather;
        private Label          lblRegionTitle;
        private TextBox        txtRegion;
        private Label          lblWeatherAlert;
        // Итог
        private Label          label6;
        private TextBox        txtTotalSum;
    }
}