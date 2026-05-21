using System;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public sealed class AuthTest
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void Login_CorrectCredentials_ShouldAuthorise()
        {
            var options = GetDbOptions();
            var newUser = new User
            {
                Id = 2,
                FirstName = "Admin",
                MiddleName = "qwe",
                LastName = "qwe",
                Login = "admin",
                PasswordHash = "qwerty",
                RoleId = 2
            };

            using (var db = new ApplicationDbContext(options))
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            string login = "admin";
            string password = "qwerty";

            User authorizedUser;
            using (var db = new ApplicationDbContext(options))
            {
                authorizedUser = db.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == password);
            }

            Assert.IsNotNull(authorizedUser);
            Assert.AreEqual("admin", authorizedUser.Login);
        }

        [TestMethod]
        public void Login_WrongPassword_ShouldDenyAccess()
        {
            var options = GetDbOptions();
            var newUser = new User
            {
                Id = 1,
                FirstName = "Admin",
                MiddleName = "qwe",
                LastName = "qwe",
                Login = "admin",
                PasswordHash = "qwerty",
                RoleId = 2
            };
            using (var db = new ApplicationDbContext(options))
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            string login = "admin";
            string password = "wrong_password";
            User authorizedUser;
            using (var db = new ApplicationDbContext(options))
            {
                authorizedUser = db.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == password);
            }

            Assert.IsNull(authorizedUser);
        }
    }
}