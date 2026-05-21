using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class DeliveryTest
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void ProcessDelivery_ExistingProduct_ShouldIncreaseStock()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                var product = new Product
                {
                    Article = "DEL-001",
                    Name = "Новая куртка",
                    Brand = "ТестБренд",
                    Size = "XL",
                    CurrentStock = 10
                };
                db.Products.Add(product);
                db.SaveChanges();
            }

            int incomingQty = 15;
            using (var db = new ApplicationDbContext(options))
            {
                var product = db.Products.FirstOrDefault(p => p.Article == "DEL-001");
                if (product != null)
                {
                    product.CurrentStock += incomingQty;
                    db.SaveChanges();
                }
            }

            using (var db = new ApplicationDbContext(options))
            {
                var updatedProduct = db.Products.FirstOrDefault(p => p.Article == "DEL-001");

                Assert.IsNotNull(updatedProduct);
                Assert.AreEqual(25, updatedProduct.CurrentStock);
            }
        }
        [TestMethod]
        public void ImportData_NewProducts_ShouldAddToDatabase()
        {
            var options = GetDbOptions();

            var importedData = new List<Product>
            {
                new Product { Article = "IMP-001", Name = "Футболка базовая", Brand = "Uniqlo", Size = "S", CurrentStock = 50 },
                new Product { Article = "IMP-002", Name = "Носки спортивные", Brand = "Nike", Size = "M", CurrentStock = 100 }
            };

            using (var db = new ApplicationDbContext(options))
            {
                foreach (var item in importedData)
                {
                    var existing = db.Products.FirstOrDefault(p => p.Article == item.Article);
                    if (existing == null)
                    {
                        db.Products.Add(item);
                    }
                }
                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext(options))
            {
                var allProducts = db.Products.ToList();
                Assert.AreEqual(2, allProducts.Count);
                Assert.IsNotNull(allProducts.FirstOrDefault(p => p.Article == "IMP-001"));
            }
        }

        [TestMethod]
        public void ImportData_ExistingProduct_ShouldUpdateStockInsteadOfDuplicate()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(new Product { Article = "IMP-003", Name = "Шорты", Brand = "Puma", Size = "L", CurrentStock = 10 });
                db.SaveChanges();
            }

            var importedData = new List<Product>
            {
                new Product { Article = "IMP-003", Name = "Шорты", Brand = "Puma", Size = "L", CurrentStock = 20 }
            };

            using (var db = new ApplicationDbContext(options))
            {
                foreach (var item in importedData)
                {
                    var existing = db.Products.FirstOrDefault(p => p.Article == item.Article);
                    if (existing != null)
                    {
                        existing.CurrentStock += item.CurrentStock;
                    }
                    else
                    {
                        db.Products.Add(item);
                    }
                }
                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext(options))
            {
                var allProducts = db.Products.ToList();
                var updatedProduct = db.Products.FirstOrDefault(p => p.Article == "IMP-003");

                Assert.AreEqual(1, allProducts.Count);
                Assert.AreEqual(30, updatedProduct.CurrentStock);
            }
        }
    }
}