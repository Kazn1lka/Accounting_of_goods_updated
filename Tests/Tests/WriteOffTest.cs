using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class WriteOffTest
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void WriteOff_ExpiredProducts_ShouldSetStockToZero()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                var expiredProduct = new Product
                {
                    Article = "DEF-001",
                    Name = "Худи оверсайз (заводской брак)",
                    Brand = "Balenciaga",
                    Size = "L",
                    CurrentStock = 10,
                    ExpiryDate = DateTime.Today.AddDays(-1)
                };
                db.Products.Add(expiredProduct);
                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext(options))
            {
                var today = DateTime.Today;
                var productsToDrop = db.Products
                    .Where(p => p.ExpiryDate != null && p.ExpiryDate < today)
                    .ToList();

                foreach (var p in productsToDrop)
                {
                    p.CurrentStock = 0; 
                }
                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext(options))
            {
                var savedProduct = db.Products.FirstOrDefault(p => p.Article == "DEF-001");

                Assert.IsNotNull(savedProduct);
                Assert.AreEqual(0, savedProduct.CurrentStock);
            }
        }
    }
}