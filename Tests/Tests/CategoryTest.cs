using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

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
                db.Shipments.AddRange(
                    new Shipment { Quantity = 5, Recipient = "ООО Альфа", ShipmentDate = today.AddDays(-5) },
                    new Shipment { Quantity = 10, Recipient = "Иван Иванов", ShipmentDate = today },
                    new Shipment { Quantity = 2, Recipient = "Иван Петров", ShipmentDate = today },
                    new Shipment { Quantity = 20, Recipient = "ЗАО Бета", ShipmentDate = today.AddDays(5) }
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