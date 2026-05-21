namespace WinFormsApp1.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string login, string password);
        bool IsLoginTaken(string login);
        void RegisterUser(User user);
    }
}
