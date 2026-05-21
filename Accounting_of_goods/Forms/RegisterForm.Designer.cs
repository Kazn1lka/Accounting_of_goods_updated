namespace WinFormsApp1
{
    partial class RegisterForm
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
			txtFirstName = new TextBox();
			txtLogin = new TextBox();
			txtPassword = new TextBox();
			btnCancel = new Button();
			btnRegister = new Button();
			txtLastName = new TextBox();
			txtMiddleName = new TextBox();
			chkShowPassword = new CheckBox();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.Anchor = AnchorStyles.Top;
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			lblTitle.Location = new Point(196, 102);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(237, 29);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Создание аккаунта";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// txtFirstName
			// 
			txtFirstName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtFirstName.BorderStyle = BorderStyle.FixedSingle;
			txtFirstName.Font = new Font("Arial", 9F);
			txtFirstName.Location = new Point(196, 171);
			txtFirstName.Margin = new Padding(4);
			txtFirstName.Name = "txtFirstName";
			txtFirstName.PlaceholderText = "Имя";
			txtFirstName.Size = new Size(214, 25);
			txtFirstName.TabIndex = 1;
			// 
			// txtLogin
			// 
			txtLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLogin.BorderStyle = BorderStyle.FixedSingle;
			txtLogin.Font = new Font("Arial", 9F);
			txtLogin.Location = new Point(196, 334);
			txtLogin.Margin = new Padding(4);
			txtLogin.Name = "txtLogin";
			txtLogin.PlaceholderText = "Логин";
			txtLogin.Size = new Size(214, 25);
			txtLogin.TabIndex = 2;
			// 
			// txtPassword
			// 
			txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtPassword.BorderStyle = BorderStyle.FixedSingle;
			txtPassword.Font = new Font("Arial", 9F);
			txtPassword.Location = new Point(196, 392);
			txtPassword.Margin = new Padding(4);
			txtPassword.Name = "txtPassword";
			txtPassword.PlaceholderText = "Пароль";
			txtPassword.Size = new Size(214, 25);
			txtPassword.TabIndex = 3;
			txtPassword.UseSystemPasswordChar = true;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnCancel.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			btnCancel.Location = new Point(28, 504);
			btnCancel.Margin = new Padding(4);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(114, 60);
			btnCancel.TabIndex = 4;
			btnCancel.Text = "Отмена";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// btnRegister
			// 
			btnRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnRegister.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
			btnRegister.Location = new Point(348, 504);
			btnRegister.Margin = new Padding(4);
			btnRegister.Name = "btnRegister";
			btnRegister.Size = new Size(232, 60);
			btnRegister.TabIndex = 5;
			btnRegister.Text = "Зарегистрироваться";
			btnRegister.UseVisualStyleBackColor = true;
			btnRegister.Click += btnRegister_Click;
			// 
			// txtLastName
			// 
			txtLastName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLastName.BorderStyle = BorderStyle.FixedSingle;
			txtLastName.Font = new Font("Arial", 9F);
			txtLastName.Location = new Point(196, 229);
			txtLastName.Margin = new Padding(4);
			txtLastName.Name = "txtLastName";
			txtLastName.PlaceholderText = "Фамилия";
			txtLastName.Size = new Size(214, 25);
			txtLastName.TabIndex = 6;
			// 
			// txtMiddleName
			// 
			txtMiddleName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtMiddleName.BorderStyle = BorderStyle.FixedSingle;
			txtMiddleName.Font = new Font("Arial", 9F);
			txtMiddleName.Location = new Point(196, 284);
			txtMiddleName.Margin = new Padding(4);
			txtMiddleName.Name = "txtMiddleName";
			txtMiddleName.PlaceholderText = "Отчество";
			txtMiddleName.Size = new Size(214, 25);
			txtMiddleName.TabIndex = 7;
			// 
			// chkShowPassword
			// 
			chkShowPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkShowPassword.AutoSize = true;
			chkShowPassword.Location = new Point(266, 429);
			chkShowPassword.Margin = new Padding(4);
			chkShowPassword.Name = "chkShowPassword";
			chkShowPassword.Size = new Size(150, 24);
			chkShowPassword.TabIndex = 8;
			chkShowPassword.Text = "Показать пароль";
			chkShowPassword.UseVisualStyleBackColor = true;
			chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
			// 
			// RegisterForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(604, 600);
			Controls.Add(chkShowPassword);
			Controls.Add(txtMiddleName);
			Controls.Add(txtLastName);
			Controls.Add(btnRegister);
			Controls.Add(btnCancel);
			Controls.Add(txtPassword);
			Controls.Add(txtLogin);
			Controls.Add(txtFirstName);
			Controls.Add(lblTitle);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(4);
			MaximizeBox = false;
			Name = "RegisterForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Складской учёт — Регистрация";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblTitle;
        private TextBox txtFirstName;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnCancel;
        private Button btnRegister;
        private TextBox txtLastName;
        private TextBox txtMiddleName;
        private CheckBox chkShowPassword;
    }
}