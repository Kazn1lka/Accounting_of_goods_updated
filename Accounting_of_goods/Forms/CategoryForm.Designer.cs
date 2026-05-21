namespace WinFormsApp1
{
    partial class dgvCategories
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblTitle = new Label();
			dataGridView1 = new DataGridView();
			txtNewCategory = new TextBox();
			btnCreateCategory = new Button();
			btnClose = new Button();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.Anchor = AnchorStyles.Top;
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			lblTitle.Location = new Point(318, 48);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(284, 29);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Справочник категорий";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// dataGridView1
			// 
			dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Location = new Point(14, 106);
			dataGridView1.Margin = new Padding(4);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersWidth = 82;
			dataGridView1.Size = new Size(442, 426);
			dataGridView1.TabIndex = 1;
			dataGridView1.CellContentClick += dataGridView1_CellContentClick;
			// 
			// txtNewCategory
			// 
			txtNewCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtNewCategory.Font = new Font("Arial", 11.25F);
			txtNewCategory.Location = new Point(545, 214);
			txtNewCategory.Margin = new Padding(4);
			txtNewCategory.Name = "txtNewCategory";
			txtNewCategory.PlaceholderText = "Название новой категории";
			txtNewCategory.Size = new Size(259, 29);
			txtNewCategory.TabIndex = 2;
			// 
			// btnCreateCategory
			// 
			btnCreateCategory.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCreateCategory.Font = new Font("Arial", 11.25F);
			btnCreateCategory.Location = new Point(586, 274);
			btnCreateCategory.Margin = new Padding(4);
			btnCreateCategory.Name = "btnCreateCategory";
			btnCreateCategory.Size = new Size(172, 47);
			btnCreateCategory.TabIndex = 3;
			btnCreateCategory.Text = "Добавить";
			btnCreateCategory.UseVisualStyleBackColor = true;
			btnCreateCategory.Click += btnCreateCategory_Click;
			// 
			// btnClose
			// 
			btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnClose.Font = new Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			btnClose.Location = new Point(14, 539);
			btnClose.Margin = new Padding(4);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(175, 46);
			btnClose.TabIndex = 4;
			btnClose.Text = "На главный экран";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// dgvCategories
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(914, 600);
			Controls.Add(btnClose);
			Controls.Add(btnCreateCategory);
			Controls.Add(txtNewCategory);
			Controls.Add(dataGridView1);
			Controls.Add(lblTitle);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(4);
			MaximizeBox = false;
			Name = "dgvCategories";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Складской учёт \u001f— Справочник категорий";
			Load += dgvCategories_Load;
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblTitle;
        private DataGridView dataGridView1;
        private TextBox txtNewCategory;
        private Button btnCreateCategory;
        private Button btnClose;
    }
}
