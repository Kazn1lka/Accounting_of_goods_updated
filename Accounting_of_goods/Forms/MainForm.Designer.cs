namespace WinFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            сменитьАккаунтToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            btnManageCategories = new ToolStripMenuItem();
            категорииToolStripMenuItem1 = new ToolStripMenuItem();
            btnShipment = new Button();
            btnHistory = new Button();
            txtSearch = new TextBox();
            dg99 = new DataGridView();
            btnAddProduct = new Button();
            btnSupply = new Button();
            cmbCurrency = new ComboBox();
            label2 = new Label();
            dgvProducts = new DataGridView();
            btnWriteOff = new Button();
            btnHeatMap = new Button();
            btnWeather = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg99).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 11.25F);
            label1.Location = new Point(624, 110);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 35);
            label1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, btnManageCategories });
            menuStrip1.Location = new Point(15, 14);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(11, 3, 0, 3);
            menuStrip1.Size = new Size(287, 42);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { сменитьАккаунтToolStripMenuItem, выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(90, 36);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // сменитьАккаунтToolStripMenuItem
            // 
            сменитьАккаунтToolStripMenuItem.Name = "сменитьАккаунтToolStripMenuItem";
            сменитьАккаунтToolStripMenuItem.Size = new Size(333, 44);
            сменитьАккаунтToolStripMenuItem.Text = "Сменить аккаунт";
            сменитьАккаунтToolStripMenuItem.Click += сменитьАккаунтToolStripMenuItem_Click;
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(333, 44);
            выходToolStripMenuItem.Text = "Выход";
            выходToolStripMenuItem.Click += выходToolStripMenuItem_Click;
            // 
            // btnManageCategories
            // 
            btnManageCategories.DropDownItems.AddRange(new ToolStripItem[] { категорииToolStripMenuItem1 });
            btnManageCategories.Name = "btnManageCategories";
            btnManageCategories.Size = new Size(184, 36);
            btnManageCategories.Text = "Справочники";
            // 
            // категорииToolStripMenuItem1
            // 
            категорииToolStripMenuItem1.Name = "категорииToolStripMenuItem1";
            категорииToolStripMenuItem1.Size = new Size(261, 44);
            категорииToolStripMenuItem1.Text = "Категории";
            категорииToolStripMenuItem1.Click += категорииToolStripMenuItem_Click;
            // 
            // btnShipment
            // 
            btnShipment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShipment.FlatAppearance.BorderColor = Color.Black;
            btnShipment.Font = new Font("Arial", 11.25F);
            btnShipment.Location = new Point(1124, 106);
            btnShipment.Margin = new Padding(6);
            btnShipment.Name = "btnShipment";
            btnShipment.Size = new Size(257, 45);
            btnShipment.TabIndex = 3;
            btnShipment.Text = "Оформить отгрузку";
            btnShipment.UseVisualStyleBackColor = true;
            btnShipment.Click += btnShipment_Click;
            // 
            // btnHistory
            // 
            btnHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHistory.FlatAppearance.BorderColor = Color.Black;
            btnHistory.Font = new Font("Arial", 11.25F);
            btnHistory.Location = new Point(1124, 166);
            btnHistory.Margin = new Padding(6);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(257, 45);
            btnHistory.TabIndex = 4;
            btnHistory.Text = "История отгрузок";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Arial", 11.25F);
            txtSearch.Location = new Point(21, 110);
            txtSearch.Margin = new Padding(6);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Поиск ";
            txtSearch.Size = new Size(566, 42);
            txtSearch.TabIndex = 5;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dg99
            // 
            dg99.AllowUserToAddRows = false;
            dg99.AllowUserToDeleteRows = false;
            dg99.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dg99.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dg99.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dg99.DefaultCellStyle = dataGridViewCellStyle1;
            dg99.Location = new Point(-2748, -520);
            dg99.Margin = new Padding(6);
            dg99.Name = "dg99";
            dg99.ReadOnly = true;
            dg99.RowHeadersWidth = 82;
            dg99.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg99.Size = new Size(1682, 787);
            dg99.TabIndex = 6;
            dg99.CellContentClick += dgvProducts_CellContentClick;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddProduct.FlatAppearance.BorderColor = Color.Black;
            btnAddProduct.Font = new Font("Arial", 11.25F);
            btnAddProduct.Location = new Point(1124, 107);
            btnAddProduct.Margin = new Padding(6);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(257, 45);
            btnAddProduct.TabIndex = 7;
            btnAddProduct.Text = "Добавить товар";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnSupply
            // 
            btnSupply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSupply.FlatAppearance.BorderColor = Color.Black;
            btnSupply.Font = new Font("Arial", 11.25F);
            btnSupply.Location = new Point(1404, 166);
            btnSupply.Margin = new Padding(6);
            btnSupply.Name = "btnSupply";
            btnSupply.Size = new Size(257, 45);
            btnSupply.TabIndex = 8;
            btnSupply.Text = "Поставки";
            btnSupply.UseVisualStyleBackColor = true;
            btnSupply.Click += btnSupply_Click;
            // 
            // cmbCurrency
            // 
            cmbCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCurrency.FormattingEnabled = true;
            cmbCurrency.Items.AddRange(new object[] { "RUB", "USD", "EUR" });
            cmbCurrency.Location = new Point(764, 109);
            cmbCurrency.Margin = new Padding(5);
            cmbCurrency.Name = "cmbCurrency";
            cmbCurrency.Size = new Size(111, 40);
            cmbCurrency.TabIndex = 9;
            cmbCurrency.SelectedIndexChanged += cmbCurrency_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(624, 115);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(125, 33);
            label2.TabIndex = 10;
            label2.Text = "Валюта:";
            // 
            // dgvProducts
            // 
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(21, 250);
            dgvProducts.Margin = new Padding(5);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.Size = new Size(1926, 691);
            dgvProducts.TabIndex = 11;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            // 
            // btnWriteOff
            // 
            btnWriteOff.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnWriteOff.FlatAppearance.BorderColor = Color.Black;
            btnWriteOff.Font = new Font("Arial", 11.25F);
            btnWriteOff.Location = new Point(1404, 105);
            btnWriteOff.Margin = new Padding(6);
            btnWriteOff.Name = "btnWriteOff";
            btnWriteOff.Size = new Size(257, 45);
            btnWriteOff.TabIndex = 12;
            btnWriteOff.Text = "Списание";
            btnWriteOff.UseVisualStyleBackColor = true;
            btnWriteOff.Click += btnWriteOff_Click;
            // 
            // btnHeatMap
            // 
            btnHeatMap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHeatMap.FlatAppearance.BorderColor = Color.Black;
            btnHeatMap.Font = new Font("Arial", 11.25F);
            btnHeatMap.Location = new Point(1684, 106);
            btnHeatMap.Margin = new Padding(6);
            btnHeatMap.Name = "btnHeatMap";
            btnHeatMap.Size = new Size(257, 45);
            btnHeatMap.TabIndex = 13;
            btnHeatMap.Text = "Тепловая карта";
            btnHeatMap.UseVisualStyleBackColor = true;
            btnHeatMap.Click += btnHeatMap_Click;
            // 
            // btnWeather
            // 
            btnWeather.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnWeather.FlatAppearance.BorderColor = Color.Black;
            btnWeather.Font = new Font("Arial", 11.25F);
            btnWeather.Location = new Point(1684, 166);
            btnWeather.Margin = new Padding(6);
            btnWeather.Name = "btnWeather";
            btnWeather.Size = new Size(257, 45);
            btnWeather.TabIndex = 14;
            btnWeather.Text = "Погода ";
            btnWeather.UseVisualStyleBackColor = true;
            btnWeather.Click += btnWeather_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1966, 960);
            Controls.Add(btnWeather);
            Controls.Add(btnHeatMap);
            Controls.Add(btnWriteOff);
            Controls.Add(dgvProducts);
            Controls.Add(label2);
            Controls.Add(cmbCurrency);
            Controls.Add(btnSupply);
            Controls.Add(btnAddProduct);
            Controls.Add(dg99);
            Controls.Add(txtSearch);
            Controls.Add(btnHistory);
            Controls.Add(btnShipment);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Sizable;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(6);
            MaximizeBox = true;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Складской учёт  — Главный экран";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg99).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private Button btnShipment;
        private Button btnHistory;
        private TextBox txtSearch;
        private DataGridView dg99;
        private ToolStripMenuItem сменитьАккаунтToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem btnManageCategories;
        private ToolStripMenuItem категорииToolStripMenuItem1;
        private Button btnAddProduct;
        private Button btnSupply;
        private ComboBox cmbCurrency;
        private Label label2;
        private DataGridView dgvProducts;
        private Button btnWriteOff;
        private Button btnHeatMap;
        private Button btnWeather;
    }
}