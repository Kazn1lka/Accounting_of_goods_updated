using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class CatalogTest
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        [TestMethod]
        public void AddProduct_ValidData_ShouldSaveToDatabase()
        {
            var options = GetDbOptions();
            var newProduct = new Product
            {
                Article = "PRD-001",
                Name = "футболка Prada",
                Brand = "Prada",
                PurchasePrice = 55000.00m,
                CurrentStock = 15,
                Size = "XL",
                CategoryId = 1
            };

            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext(options))
            {
                var savedProduct = db.Products.FirstOrDefault(p => p.Article == "PRD-001");

                Assert.IsNotNull(savedProduct);
                Assert.AreEqual("футболка Prada", savedProduct.Name);
                Assert.AreEqual(15, savedProduct.CurrentStock);
                Assert.AreEqual(55000.00m, savedProduct.PurchasePrice);
            }
        }
        [TestMethod]
        public void AddProduct_DuplicateArticle_ShouldBeDetected()
        {
            var options = GetDbOptions();

            using (var db = new ApplicationDbContext(options))
            {
                var firstProduct = new Product
                {
                    Article = "PRD-002",
                    Name = "Оригинальная футболка",
                    Brand = "Prada",
                    PurchasePrice = 5000m,
                    CurrentStock = 10,
                    CategoryId = 1,
                    Size = "M"
                };
                db.Products.Add(firstProduct);
                db.SaveChanges();
            }

            bool isDuplicateFound;
            using (var db = new ApplicationDbContext(options))
            {
                string newArticleToSave = "PRD-002";

                isDuplicateFound = db.Products.Any(p => p.Article == newArticleToSave);
            }

            Assert.IsTrue(isDuplicateFound);
        }
        [TestMethod]
        public void EditProduct_ValidData_ShouldUpdateDatabase()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                var initialProduct = new Product
                {
                    Article = "PRD-002",
                    Name = "Старая футболка Prada",
                    Brand = "Prada",
                    PurchasePrice = 30000.00m,
                    CurrentStock = 10,
                    CategoryId = 1,
                    Size = "M"
                };
                db.Products.Add(initialProduct);
                db.SaveChanges();
            }
            using (var db = new ApplicationDbContext(options))
            {
                var productToEdit = db.Products.FirstOrDefault(p => p.Article == "PRD-002");

                productToEdit.Name = "Новая куртка Prada";
                productToEdit.PurchasePrice = 5000.00m;
                productToEdit.CurrentStock = 25;

                db.SaveChanges();
            }
            using (var db = new ApplicationDbContext(options))
            {
                var updatedProduct = db.Products.FirstOrDefault(p => p.Article == "PRD-002");

                Assert.IsNotNull(updatedProduct);

                Assert.AreEqual("Новая куртка Prada", updatedProduct.Name);
                Assert.AreEqual(5000.00m, updatedProduct.PurchasePrice);
                Assert.AreEqual(25, updatedProduct.CurrentStock);

                Assert.AreEqual("Prada", updatedProduct.Brand);
                Assert.AreEqual("M", updatedProduct.Size);
            }
        }
        [TestMethod]
        public void EditProduct_NonExistentArticle_ShouldReturnNull()
        {
            var options = GetDbOptions();

            Product ghostProduct;
            using (var db = new ApplicationDbContext(options))
            {
                ghostProduct = db.Products.FirstOrDefault(p => p.Article == "PRD-999");
            }

            Assert.IsNull(ghostProduct);
        }
        [TestMethod]
        public void SearchProductByName_HaveSomeProducts_ReturnOneProduct()
        {
            var options = GetDbOptions();
            var newProduct1 = new Product()
            {
                Article = "ABC-001",
                Name = "Красная футболка",
                Brand = "Nike",
                PurchasePrice = 2000.00m,
                CurrentStock = 20,
                CategoryId = 1,
                Size = "L"
            };
            var newProduct2 = new Product()
            {
                Article = "ABC-002",
                Name = "Чёрные джинсы",
                Brand = "Dime",
                PurchasePrice = 2000.00m,
                CurrentStock = 20,
                CategoryId = 2,
                Size = "L"
            };
            var newProduct3 = new Product()
            {
                Article = "ABC-003",
                Name = "Синий свитер",
                Brand = "Adidas",
                PurchasePrice = 2000.00m,
                CurrentStock = 20,
                CategoryId = 3,
                Size = "L"
            };
            string searchText = "Красная футболка";

            using (var db = new ApplicationDbContext(options))
            {
                db.Products.AddRange(newProduct1, newProduct2, newProduct3);
                db.SaveChanges();   
            }

            int foundCount;
            using (var db = new ApplicationDbContext(options))
            {
                searchText = searchText.ToLower();
                var query = db.Products.Where(p =>
                    p.Article.ToLower().Contains(searchText) ||
                    p.Brand.ToLower().Contains(searchText) ||
                    p.Name.ToLower().Contains(searchText) 
                );
                foundCount = query.Count();
            }
            Assert.AreEqual(1, foundCount);
        }
        [TestMethod]
        public void AddProduct_NegativePriceOrStock_ShouldBlockSaving()
        {
            var options = GetDbOptions();
            var invalidProduct = new Product
            {
                Article = "ERR-001",
                Name = "Сломанный товар",
                Brand = "BadBrand",
                PurchasePrice = -500m, 
                CurrentStock = -10,   
                CategoryId = 1,
                Size = "M"
            };
            bool isSaved = false;
            string errorMessage = string.Empty;

            using (var db = new ApplicationDbContext(options))
            {
                if (invalidProduct.PurchasePrice < 0 || invalidProduct.CurrentStock < 0)
                {
                    isSaved = false;
                    errorMessage = "Цена и остаток не могут быть отрицательными";
                }
                else
                {
                    db.Products.Add(invalidProduct);
                    db.SaveChanges();
                    isSaved = true;
                }
            }

            Assert.IsFalse(isSaved);
            Assert.AreEqual("Цена и остаток не могут быть отрицательными", errorMessage);
            using (var db = new ApplicationDbContext(options))
            {
                var productInDb = db.Products.FirstOrDefault(p => p.Article == "ERR-001");

                Assert.IsNull(productInDb);
            }
        }
        [TestMethod]
        public void DeleteProduct_ExistingItem_ShouldRemoveFromDatabase()
        {
            var options = GetDbOptions();

            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(new Product
                {
                    Article = "DEL-001",
                    Name = "Товар под списание",
                    Brand = "DeleteMe",
                    PurchasePrice = 1000m,
                    CurrentStock = 10,
                    CategoryId = 1,
                    Size = "L"
                });
                db.SaveChanges(); 
            }

            using (var db = new ApplicationDbContext(options))
            {
                var productToDelete = db.Products.FirstOrDefault(p => p.Article == "DEL-001");

                if (productToDelete != null)
                {
                    db.Products.Remove(productToDelete);
                    db.SaveChanges();
                }
            }

            using (var db = new ApplicationDbContext(options))
            {
                var deletedProduct = db.Products.FirstOrDefault(p => p.Article == "DEL-001");
                Assert.IsNull(deletedProduct);
            }
        }

        [TestMethod]
        public void CalculateTotalInventoryValue_ShouldReturnCorrectSum()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                var product1 = new Product
                {
                    Article = "REP-001",
                    Name = "Куртка зимняя",
                    Brand = "The North Face",
                    Size = "M",
                    CurrentStock = 10,
                    PurchasePrice = 15000m
                };

                var product2 = new Product
                {
                    Article = "REP-002",
                    Name = "Кроссовки",
                    Brand = "Nike",
                    Size = "42",
                    CurrentStock = 5,
                    PurchasePrice = 10000m
                };

                db.Products.Add(product1);
                db.Products.Add(product2);
                db.SaveChanges();
            }

            decimal totalInventoryValue = 0;
            using (var db = new ApplicationDbContext(options))
            {
                var allProducts = db.Products.ToList();
                totalInventoryValue = allProducts.Sum(p => p.CurrentStock * p.PurchasePrice);
            }

            decimal expectedValue = (10 * 15000m) + (5 * 10000m);

            Assert.AreEqual(expectedValue, totalInventoryValue);
        }
    }
}
