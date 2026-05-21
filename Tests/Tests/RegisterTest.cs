using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public sealed class RegisterTest
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [TestMethod]
        public void Register_NewValidUser_ShouldSaveToDatabase()
        {
            var options = GetDbOptions("Test_Register_NewUser");
            var newUser = new User
            {
                Id = 1,
                FirstName = "Иван",
                MiddleName = "Иванов",
                LastName = "Иванович",
                Login = "ivan123",
                PasswordHash = "qwerty",
                RoleId = 1
            };

            using (var db = new ApplicationDbContext(options))
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext(options))
            {
                var savedUser = db.Users.FirstOrDefault(User => User.Login == "ivan123");

                Assert.IsNotNull(savedUser);
            }
        }
        [TestMethod]
        public void Register_DuplicateLogin_ShouldBeDetected()
        {
            var options = GetDbOptions("Test_Register_DuplicateLogin");
            var newUser = new User
            {
                Id = 1,
                FirstName = "Иван",
                MiddleName = "Иванов",
                LastName = "Иванович",
                Login = "ivan123",
                PasswordHash = "qwerty",
                RoleId = 1
            };

            using (var db = new ApplicationDbContext(options))
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            bool isLoginTaken;
            string loginToRegister = "ivan123";

            using (var db = new ApplicationDbContext(options))
            {
                isLoginTaken = db.Users.Any(User => User.Login == loginToRegister);
            }
            Assert.IsTrue(isLoginTaken);
        }
    }
}
