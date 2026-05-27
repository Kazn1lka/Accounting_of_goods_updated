namespace Accounting_of_goods
{
    partial class DeliveryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            cmbProduct = new ComboBox();
            cmbSize = new ComboBox();
            numQty = new NumericUpDown();
            numPrice = new NumericUpDown();
            dtpExpiry = new DateTimePicker();
            btnImport = new Button();
            btnCancel = new Button();
            btnConfirmDelivery = new Button();
            dgvPreview = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnAddToList = new Button();
            numSellingPrice = new NumericUpDown();
            label6 = new Label();
            lblSupplierInn = new Label();
            txtSupplierInn = new TextBox();
            btnVerifyInn = new Button();
            lblInnStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSellingPrice).BeginInit();
            SuspendLayout();
            // 
            // cmbProduct
            // 
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(221, 110);
            cmbProduct.Margin = new Padding(5, 6, 5, 6);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(244, 40);
            cmbProduct.TabIndex = 0;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            // 
            // cmbSize
            // 
            cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSize.FormattingEnabled = true;
            cmbSize.Location = new Point(221, 193);
            cmbSize.Margin = new Padding(5, 6, 5, 6);
            cmbSize.Name = "cmbSize";
            cmbSize.Size = new Size(244, 40);
            cmbSize.TabIndex = 1;
            // 
            // numQty
            // 
            numQty.Location = new Point(221, 286);
            numQty.Margin = new Padding(5, 6, 5, 6);
            numQty.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numQty.Name = "numQty";
            numQty.Size = new Size(247, 39);
            numQty.TabIndex = 2;
            // 
            // numPrice
            // 
            numPrice.Location = new Point(221, 381);
            numPrice.Margin = new Padding(5, 6, 5, 6);
            numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(249, 39);
            numPrice.TabIndex = 3;
            // 
            // dtpExpiry
            // 
            dtpExpiry.Location = new Point(221, 541);
            dtpExpiry.Margin = new Padding(5, 6, 5, 6);
            dtpExpiry.Name = "dtpExpiry";
            dtpExpiry.Size = new Size(244, 39);
            dtpExpiry.TabIndex = 4;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(234, 720);
            btnImport.Margin = new Padding(5, 6, 5, 6);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(234, 56);
            btnImport.TabIndex = 5;
            btnImport.Text = "Импорт из файла";
            btnImport.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1402, 883);
            btnCancel.Margin = new Padding(5, 6, 5, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 56);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfirmDelivery
            // 
            btnConfirmDelivery.Location = new Point(1552, 883);
            btnConfirmDelivery.Margin = new Padding(5, 6, 5, 6);
            btnConfirmDelivery.Name = "btnConfirmDelivery";
            btnConfirmDelivery.Size = new Size(213, 56);
            btnConfirmDelivery.TabIndex = 8;
            btnConfirmDelivery.Text = "Оприходовать";
            btnConfirmDelivery.UseVisualStyleBackColor = true;
            btnConfirmDelivery.Click += btnConfirmDelivery_Click;
            // 
            // dgvPreview
            // 
            dgvPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPreview.Location = new Point(544, 139);
            dgvPreview.Margin = new Padding(5, 6, 5, 6);
            dgvPreview.Name = "dgvPreview";
            dgvPreview.RowHeadersWidth = 51;
            dgvPreview.Size = new Size(1235, 622);
            dgvPreview.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 115);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 32);
            label1.TabIndex = 10;
            label1.Text = "Товар";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 206);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(94, 32);
            label2.TabIndex = 11;
            label2.Text = "Размер";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 297);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(92, 32);
            label3.TabIndex = 12;
            label3.Text = "Кол-во";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 388);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(165, 32);
            label4.TabIndex = 13;
            label4.Text = "Цена закупки";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(42, 549);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(146, 32);
            label5.TabIndex = 14;
            label5.Text = "Срок акту-и";
            // 
            // btnAddToList
            // 
            btnAddToList.Location = new Point(166, 634);
            btnAddToList.Margin = new Padding(5, 6, 5, 6);
            btnAddToList.Name = "btnAddToList";
            btnAddToList.Size = new Size(304, 56);
            btnAddToList.TabIndex = 16;
            btnAddToList.Text = "Добавить в список";
            btnAddToList.UseVisualStyleBackColor = true;
            btnAddToList.Click += btnAddToList_Click;
            // 
            // numSellingPrice
            // 
            numSellingPrice.Location = new Point(221, 466);
            numSellingPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numSellingPrice.Name = "numSellingPrice";
            numSellingPrice.Size = new Size(247, 39);
            numSellingPrice.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 466);
            label6.Name = "label6";
            label6.Size = new Size(177, 32);
            label6.TabIndex = 18;
            label6.Text = "Цена продажи";
            // 
            // lblSupplierInn
            // 
            lblSupplierInn.AutoSize = true;
            lblSupplierInn.Location = new Point(55, 849);
            lblSupplierInn.Name = "lblSupplierInn";
            lblSupplierInn.Size = new Size(210, 32);
            lblSupplierInn.TabIndex = 19;
            lblSupplierInn.Text = "ИНН поставщика:";
            // 
            // txtSupplierInn
            // 
            txtSupplierInn.BorderStyle = BorderStyle.FixedSingle;
            txtSupplierInn.Location = new Point(271, 849);
            txtSupplierInn.MaxLength = 12;
            txtSupplierInn.Name = "txtSupplierInn";
            txtSupplierInn.PlaceholderText = "10 или 12 цифр";
            txtSupplierInn.Size = new Size(244, 39);
            txtSupplierInn.TabIndex = 20;
            txtSupplierInn.TextChanged += txtSupplierInn_TextChanged;
            txtSupplierInn.KeyDown += txtSupplierInn_KeyDown;
            txtSupplierInn.KeyPress += txtSupplierInn_KeyPress;
            // 
            // btnVerifyInn
            // 
            btnVerifyInn.Location = new Point(531, 849);
            btnVerifyInn.Name = "btnVerifyInn";
            btnVerifyInn.Size = new Size(154, 43);
            btnVerifyInn.TabIndex = 21;
            btnVerifyInn.Text = "Проверить";
            btnVerifyInn.UseVisualStyleBackColor = true;
            btnVerifyInn.Click += btnVerifyInn_Click;
            // 
            // lblInnStatus
            // 
            lblInnStatus.AutoSize = true;
            lblInnStatus.Font = new Font("Arial", 8.5F, FontStyle.Bold);
            lblInnStatus.ForeColor = Color.Gray;
            lblInnStatus.Location = new Point(625, 28);
            lblInnStatus.Name = "lblInnStatus";
            lblInnStatus.Size = new Size(0, 27);
            lblInnStatus.TabIndex = 22;
            // 
            // DeliveryForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1799, 960);
            Controls.Add(label6);
            Controls.Add(numSellingPrice);
            Controls.Add(btnAddToList);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPreview);
            Controls.Add(btnConfirmDelivery);
            Controls.Add(btnCancel);
            Controls.Add(btnImport);
            Controls.Add(dtpExpiry);
            Controls.Add(numPrice);
            Controls.Add(numQty);
            Controls.Add(cmbSize);
            Controls.Add(cmbProduct);
            Controls.Add(lblInnStatus);
            Controls.Add(btnVerifyInn);
            Controls.Add(txtSupplierInn);
            Controls.Add(lblSupplierInn);
            Margin = new Padding(5, 6, 5, 6);
            Name = "DeliveryForm";
            Text = "Складской учет – Поставки";
            Load += DeliveryForm_Load;
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSellingPrice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbProduct;
        private ComboBox cmbSize;
        private NumericUpDown numQty;
        private NumericUpDown numPrice;
        private DateTimePicker dtpExpiry;
        private Button btnImport;
        private Button btnCancel;
        private Button btnConfirmDelivery;
        private DataGridView dgvPreview;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnAddToList;
        private NumericUpDown numSellingPrice;
        private Label label6;
        private Label lblSupplierInn;
        private TextBox txtSupplierInn;
        private Button btnVerifyInn;
        private Label lblInnStatus;
    }
}