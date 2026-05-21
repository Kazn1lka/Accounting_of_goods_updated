namespace WinFormsApp1
{
    partial class ProductEditForm
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
            lblTitle = new Label();
            txtArticle = new TextBox();
            txtBrand = new TextBox();
            txtName = new TextBox();
            cmbCategory = new ComboBox();
            cmbSize = new ComboBox();
            btnCancel = new Button();
            btnSave = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(240, 65);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(418, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Редактирование данных о товаре";
            // 
            // txtArticle
            // 
            txtArticle.BackColor = SystemColors.Control;
            txtArticle.Font = new Font("Arial", 11.25F);
            txtArticle.Location = new Point(176, 166);
            txtArticle.Margin = new Padding(4);
            txtArticle.Name = "txtArticle";
            txtArticle.Size = new Size(200, 29);
            txtArticle.TabIndex = 2;
            // 
            // txtBrand
            // 
            txtBrand.Font = new Font("Arial", 11.25F);
            txtBrand.Location = new Point(176, 247);
            txtBrand.Margin = new Padding(4);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(200, 29);
            txtBrand.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Font = new Font("Arial", 11.25F);
            txtName.Location = new Point(176, 329);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 29);
            txtName.TabIndex = 4;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Arial", 11.25F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(605, 166);
            cmbCategory.Margin = new Padding(4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(200, 30);
            cmbCategory.TabIndex = 8;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // cmbSize
            // 
            cmbSize.Font = new Font("Arial", 11.25F);
            cmbSize.FormattingEnabled = true;
            cmbSize.Location = new Point(605, 248);
            cmbSize.Margin = new Padding(4);
            cmbSize.Name = "cmbSize";
            cmbSize.Size = new Size(200, 30);
            cmbSize.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Arial", 11.25F);
            btnCancel.Location = new Point(52, 511);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(132, 59);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Arial", 11.25F);
            btnSave.Location = new Point(724, 511);
            btnSave.Margin = new Padding(4);
            btnSave.Name = "btnSave";
            btnSave.RightToLeft = RightToLeft.No;
            btnSave.Size = new Size(166, 59);
            btnSave.TabIndex = 12;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 11.25F);
            label1.Location = new Point(80, 173);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(79, 22);
            label1.TabIndex = 13;
            label1.Text = "Артикул";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 11.25F);
            label2.Location = new Point(80, 254);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(65, 22);
            label2.TabIndex = 14;
            label2.Text = "Бренд";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 11.25F);
            label3.Location = new Point(80, 336);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 22);
            label3.TabIndex = 15;
            label3.Text = "Название";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 11.25F);
            label5.Location = new Point(497, 170);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(99, 22);
            label5.TabIndex = 17;
            label5.Text = "Категория";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 11.25F);
            label6.Location = new Point(497, 252);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(76, 22);
            label6.TabIndex = 18;
            label6.Text = "Размер";
            // 
            // ProductEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(cmbSize);
            Controls.Add(cmbCategory);
            Controls.Add(txtName);
            Controls.Add(txtBrand);
            Controls.Add(txtArticle);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "ProductEditForm";
            Text = "Складской учёт — Редактирование товара";
            Load += ProductEditForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtArticle;
        private TextBox txtBrand;
        private TextBox txtName;
        private ComboBox cmbCategory;
        private ComboBox cmbSize;
        private Button btnCancel;
        private Button btnSave;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
    }
}