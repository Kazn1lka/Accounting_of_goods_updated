namespace WinFormsApp1.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public User Authenticate(string login, string password)
        {
            return _db.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == password);
        }

        public bool IsLoginTaken(string login)
        {
            return _db.Users.Any(u => u.Login == login);
        }

        public void RegisterUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}
