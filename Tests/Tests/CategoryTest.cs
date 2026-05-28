using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public sealed class CategoryTests
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void AddCategory_DuplicateName_ShouldBlockSaving()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                db.Categories.Add(new Category { Id = 1, Name = "Обувь" });
                db.SaveChanges();
            }
            string newCategoryName = "обувь";

            bool isSaved = false;
            string errorMessage = string.Empty;
            using (var db = new ApplicationDbContext(options))
            {
                bool categoryExists = db.Categories.Any(c => c.Name.ToLower() == newCategoryName.ToLower());
                if (categoryExists)
                {
                    isSaved = false;
                    errorMessage = "Категория с таким названием уже существует";
                }
                else
                {
                    db.Categories.Add(new Category { Id = 2, Name = newCategoryName });
                    db.SaveChanges();
                    isSaved = true;
                }
            }
            Assert.IsFalse(isSaved);
            Assert.AreEqual("Категория с таким названием уже существует", errorMessage);
            using (var db = new ApplicationDbContext(options))
            {
                int categoryCount = db.Categories.Count();
                Assert.AreEqual(1, categoryCount);
            }
        }

        [TestMethod]
        public void FilterHistory_ByDateAndText_ShouldReturnCorrectRecords()
        {
            var options = GetDbOptions();
            var today = DateTime.UtcNow;

            using (var db = new ApplicationDbContext(options))
            {
                db.Roles.Add(new Role { Id = 1, Name = "Admin" });
                db.Users.Add(new User { Id = 1, RoleId = 1, Login = "admin", PasswordHash = "hash", FirstName = "A", LastName = "B" });
                db.Categories.Add(new Category { Id = 1, Name = "Shoes" });
                db.Products.Add(new Product { Id = 1, CategoryId = 1, Article = "001", Name = "Sneakers", Brand = "Nike", Size = "42", PurchasePrice = 1000 });
                db.SaveChanges();

                db.Shipments.AddRange(
                    new Shipment { UserId = 1, ProductId = 1, Quantity = 5, Recipient = "ООО Альфа", ShipmentDate = today.AddDays(-5), CurrencyAtShipment = "RUB", RateAtShipment = 1m },
                    new Shipment { UserId = 1, ProductId = 1, Quantity = 10, Recipient = "Иван Иванов", ShipmentDate = today, CurrencyAtShipment = "RUB", RateAtShipment = 1m },
                    new Shipment { UserId = 1, ProductId = 1, Quantity = 2, Recipient = "Иван Петров", ShipmentDate = today, CurrencyAtShipment = "RUB", RateAtShipment = 1m },
                    new Shipment { UserId = 1, ProductId = 1, Quantity = 20, Recipient = "ЗАО Бета", ShipmentDate = today.AddDays(5), CurrencyAtShipment = "RUB", RateAtShipment = 1m }
                );
                db.SaveChanges();
            }

            DateTime filterStartDate = today.AddDays(-1);
            DateTime filterEndDate = today.AddDays(1);
            string textFilter = "Иван";

            int foundCount = 0;

            using (var db = new ApplicationDbContext(options))
            {
                var query = db.Shipments.AsQueryable();

                query = query.Where(s => s.ShipmentDate >= filterStartDate && s.ShipmentDate <= filterEndDate);

                if (!string.IsNullOrWhiteSpace(textFilter))
                {
                    query = query.Where(s => s.Recipient.Contains(textFilter));
                }

                foundCount = query.Count();
            }

            Assert.AreEqual(2, foundCount);
        }
    }
}