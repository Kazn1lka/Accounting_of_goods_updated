namespace WinFormsApp1
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim(); 
            string lastName = txtLastName.Text.Trim();   
            string middleName = txtMiddleName.Text.Trim(); 
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

           
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните Имя, Фамилию, Логин и Пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
               
                bool userExists = db.Users.Any(u => u.Login == login);
                if (userExists)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                User newUser = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = string.IsNullOrEmpty(middleName) ? null : middleName,
                    Login = login,
                    PasswordHash = password, 
                    RoleId = 2
                };

               
                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Аккаунт успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
