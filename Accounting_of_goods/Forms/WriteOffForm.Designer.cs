namespace WinFormsApp1
{
    partial class WriteOffForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cmbProduct = new ComboBox();
            cmbSize = new ComboBox();
            lblAvailable = new Label();
            numQty = new NumericUpDown();
            txtReason = new TextBox();
            btnWriteOff = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            SuspendLayout();
            // 
            // cmbProduct
            // 
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(211, 48);
            cmbProduct.Margin = new Padding(5, 5, 5, 5);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(322, 40);
            cmbProduct.TabIndex = 0;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            // 
            // cmbSize
            // 
            cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSize.FormattingEnabled = true;
            cmbSize.Location = new Point(211, 128);
            cmbSize.Margin = new Padding(5, 5, 5, 5);
            cmbSize.Name = "cmbSize";
            cmbSize.Size = new Size(322, 40);
            cmbSize.TabIndex = 1;
            cmbSize.SelectedIndexChanged += cmbSize_SelectedIndexChanged;
            // 
            // lblAvailable
            // 
            lblAvailable.AutoSize = true;
            lblAvailable.Location = new Point(211, 208);
            lblAvailable.Margin = new Padding(5, 0, 5, 0);
            lblAvailable.Name = "lblAvailable";
            lblAvailable.Size = new Size(27, 32);
            lblAvailable.TabIndex = 2;
            lblAvailable.Text = "0";
            // 
            // numQty
            // 
            numQty.Location = new Point(211, 288);
            numQty.Margin = new Padding(5, 5, 5, 5);
            numQty.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numQty.Name = "numQty";
            numQty.Size = new Size(325, 39);
            numQty.TabIndex = 3;
            // 
            // txtReason
            // 
            txtReason.Location = new Point(211, 368);
            txtReason.Margin = new Padding(5, 5, 5, 5);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(322, 39);
            txtReason.TabIndex = 4;
            // 
            // btnWriteOff
            // 
            btnWriteOff.Location = new Point(374, 448);
            btnWriteOff.Margin = new Padding(5, 5, 5, 5);
            btnWriteOff.Name = "btnWriteOff";
            btnWriteOff.Size = new Size(162, 64);
            btnWriteOff.TabIndex = 5;
            btnWriteOff.Text = "Списать";
            btnWriteOff.UseVisualStyleBackColor = true;
            btnWriteOff.Click += btnWriteOff_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(195, 448);
            btnCancel.Margin = new Padding(5, 5, 5, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(162, 64);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 53);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(85, 32);
            label1.TabIndex = 7;
            label1.Text = "Товар:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 133);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(99, 32);
            label2.TabIndex = 8;
            label2.Text = "Размер:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 208);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(125, 32);
            label3.TabIndex = 9;
            label3.Text = "Доступно:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 291);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(149, 32);
            label4.TabIndex = 10;
            label4.Text = "Количество:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(32, 373);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(118, 32);
            label5.TabIndex = 11;
            label5.Text = "Причина:";
            // 
            // WriteOffForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(601, 560);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnWriteOff);
            Controls.Add(txtReason);
            Controls.Add(numQty);
            Controls.Add(lblAvailable);
            Controls.Add(cmbSize);
            Controls.Add(cmbProduct);
            Margin = new Padding(5, 5, 5, 5);
            Name = "WriteOffForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Списание товара";
            Load += WriteOffForm_Load;
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label lblAvailable;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnWriteOff;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}