namespace WinFormsApp1
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;

        public LoginForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = _userService.Authenticate(txtLogin.Text, txtPassword.Text);

            if (user != null)
            {
                var mainForm = Program.ServiceProvider.GetRequiredService<MainForm>();
                mainForm.SetUser(user);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var regForm = Program.ServiceProvider.GetRequiredService<RegisterForm>();
            regForm.ShowDialog();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
