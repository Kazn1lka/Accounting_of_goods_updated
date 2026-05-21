namespace WinFormsApp1
{
    partial class LoginForm
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
			txtLogin = new TextBox();
			txtPassword = new TextBox();
			btnLogin = new Button();
			linkRegister = new LinkLabel();
			chkShowPassword = new CheckBox();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.Anchor = AnchorStyles.Top;
			lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			lblTitle.Location = new Point(128, 92);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(190, 29);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Вход в аккаунт";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// txtLogin
			// 
			txtLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLogin.BorderStyle = BorderStyle.FixedSingle;
			txtLogin.Font = new Font("Arial", 8.25F);
			txtLogin.Location = new Point(112, 151);
			txtLogin.Margin = new Padding(4);
			txtLogin.Name = "txtLogin";
			txtLogin.PlaceholderText = "Логин";
			txtLogin.Size = new Size(202, 23);
			txtLogin.TabIndex = 1;
			// 
			// txtPassword
			// 
			txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtPassword.BorderStyle = BorderStyle.FixedSingle;
			txtPassword.Font = new Font("Arial", 8.25F);
			txtPassword.Location = new Point(112, 209);
			txtPassword.Margin = new Padding(4);
			txtPassword.Name = "txtPassword";
			txtPassword.PlaceholderText = "Пароль";
			txtPassword.Size = new Size(202, 23);
			txtPassword.TabIndex = 2;
			txtPassword.UseSystemPasswordChar = true;
			// 
			// btnLogin
			// 
			btnLogin.Anchor = AnchorStyles.Top;
			btnLogin.FlatStyle = FlatStyle.System;
			btnLogin.Font = new Font("Arial", 8.25F);
			btnLogin.Location = new Point(159, 282);
			btnLogin.Margin = new Padding(4);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(100, 31);
			btnLogin.TabIndex = 3;
			btnLogin.Text = "Войти";
			btnLogin.UseVisualStyleBackColor = true;
			btnLogin.Click += btnLogin_Click;
			// 
			// linkRegister
			// 
			linkRegister.Anchor = AnchorStyles.Bottom;
			linkRegister.AutoSize = true;
			linkRegister.Font = new Font("Arial", 8.25F);
			linkRegister.Location = new Point(137, 317);
			linkRegister.Margin = new Padding(4, 0, 4, 0);
			linkRegister.Name = "linkRegister";
			linkRegister.Size = new Size(143, 16);
			linkRegister.TabIndex = 4;
			linkRegister.TabStop = true;
			linkRegister.Text = "Зарегистрироваться";
			linkRegister.LinkClicked += linkRegister_LinkClicked;
			// 
			// chkShowPassword
			// 
			chkShowPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkShowPassword.AutoSize = true;
			chkShowPassword.Location = new Point(164, 240);
			chkShowPassword.Margin = new Padding(4);
			chkShowPassword.Name = "chkShowPassword";
			chkShowPassword.RightToLeft = RightToLeft.Yes;
			chkShowPassword.Size = new Size(150, 24);
			chkShowPassword.TabIndex = 5;
			chkShowPassword.Text = "Показать пароль";
			chkShowPassword.UseVisualStyleBackColor = true;
			chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
			// 
			// LoginForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(415, 441);
			Controls.Add(chkShowPassword);
			Controls.Add(linkRegister);
			Controls.Add(btnLogin);
			Controls.Add(txtPassword);
			Controls.Add(txtLogin);
			Controls.Add(lblTitle);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(4);
			MaximizeBox = false;
			Name = "LoginForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Складской учёт — Вход в аккаунт";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblTitle;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private LinkLabel linkRegister;
        private CheckBox chkShowPassword;
    }
}