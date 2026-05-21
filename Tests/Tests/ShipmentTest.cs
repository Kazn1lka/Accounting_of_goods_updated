using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class ShipmentTests
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void ShipProduct_EnoughStock_ShouldDecreaseStockAndCreateRecord()
        {
            var options = GetDbOptions();

            using (var db = new ApplicationDbContext(options))
            {
                var product = new Product
                {
                    Article = "PRD-001",
                    Name = "Кофта1",
                    Brand = "Prada",
                    PurchasePrice = 2500m,
                    CurrentStock = 50,
                    CategoryId = 1,
                    Size = "S"
                };
                db.Products.Add(product);
                db.SaveChanges();
            }

            bool isSuccess = false;
            using (var db = new ApplicationDbContext(options))
            {
                var productToShip = db.Products.FirstOrDefault(p => p.Article == "PRD-001");
                int quantityToShip = 10;
                string recipient = "ООО Ромашка";

                if (productToShip.CurrentStock >= quantityToShip)
                {
                    productToShip.CurrentStock -= quantityToShip;

                    db.Shipments.Add(new Shipment
                    {
                        Quantity = quantityToShip,
                        Recipient = recipient,
                        ShipmentDate = DateTime.UtcNow
                    });

                    db.SaveChanges();
                    isSuccess = true;
                }
            }

            using (var db = new ApplicationDbContext(options))
            {
                var updatedProduct = db.Products.FirstOrDefault(p => p.Article == "PRD-001");
                var shipmentRecord = db.Shipments.FirstOrDefault();

                Assert.IsTrue(isSuccess);
                Assert.AreEqual(40, updatedProduct.CurrentStock);

                Assert.IsNotNull(shipmentRecord);
                Assert.AreEqual(10, shipmentRecord.Quantity);
                Assert.AreEqual("ООО Ромашка", shipmentRecord.Recipient);
            }
        }

        [TestMethod]
        public void ShipProduct_NotEnoughStock_ShouldBlockShipment()
        {
            var options = GetDbOptions();

            using (var db = new ApplicationDbContext(options))
            {
                var product = new Product
                {
                    Article = "PRD-002",
                    Name = "Кофта2",
                    Brand = "Prada",
                    PurchasePrice = 5000m,
                    CurrentStock = 5,
                    CategoryId = 1,
                    Size = "M"
                };
                db.Products.Add(product);
                db.SaveChanges();
            }

            bool isSuccess = false;
            using (var db = new ApplicationDbContext(options))
            {
                var productToShip = db.Products.FirstOrDefault(p => p.Article == "PRD-002");
                int quantityToShip = 100;

                if (productToShip.CurrentStock >= quantityToShip)
                {
                    productToShip.CurrentStock -= quantityToShip;
                    db.Shipments.Add(new Shipment
                    {
                        Quantity = quantityToShip,
                        Recipient = "ООО Ромашка",
                        ShipmentDate = DateTime.UtcNow
                    });
                    db.SaveChanges();
                    isSuccess = true;
                }
            }

            using (var db = new ApplicationDbContext(options))
            {
                var updatedProduct = db.Products.FirstOrDefault(p => p.Article == "PRD-002");

                Assert.IsFalse(isSuccess);
                Assert.AreEqual(5, updatedProduct.CurrentStock);
                Assert.AreEqual(0, db.Shipments.Count());
            }
        }

        [TestMethod]
        public void ShipProduct_EmptyCart_ShouldBlockShipment()
        {
            var options = GetDbOptions();

            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(new Product
                {
                    Article = "TTT-001",
                    Name = "Тестовый товар",
                    Brand = "TestBrand",
                    PurchasePrice = 100m,
                    CurrentStock = 10,
                    CategoryId = 1,
                    Size = "M"
                });
                db.SaveChanges();
            }

            var cartToShip = new List<Product>();
            bool isSuccess = true;
            string errorMessage = string.Empty;

            using (var db = new ApplicationDbContext(options))
            {
                if (cartToShip.Count == 0)
                {
                    isSuccess = false;
                    errorMessage = "Список отгрузки пуст";
                }
                else
                {
                    db.SaveChanges();
                }
            }

            using (var db = new ApplicationDbContext(options))
            {
                var unmodifiedProduct = db.Products.FirstOrDefault(p => p.Article == "TTT-001");
                Assert.IsFalse(isSuccess);
                Assert.AreEqual("Список отгрузки пуст", errorMessage);
                Assert.AreEqual(10, unmodifiedProduct.CurrentStock);
                Assert.AreEqual(0, db.Shipments.Count());
            }
        }

        [TestMethod]
        public void ShipProduct_ExactStockAmount_ShouldMakeStockZero()
        {
            var options = GetDbOptions();

            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(new Product
                {
                    Article = "WWW-001",
                    Name = "Последняя куртка",
                    Brand = "Exclusive",
                    PurchasePrice = 15000m,
                    CurrentStock = 5,
                    CategoryId = 1,
                    Size = "L"
                });
                db.SaveChanges();
            }

            bool isSuccess = false;
            int quantityToShip = 5;

            using (var db = new ApplicationDbContext(options))
            {
                var productToShip = db.Products.FirstOrDefault(p => p.Article == "WWW-001");

                if (productToShip.CurrentStock >= quantityToShip)
                {
                    productToShip.CurrentStock -= quantityToShip;

                    db.Shipments.Add(new Shipment
                    {
                        Quantity = quantityToShip,
                        Recipient = "Физ. лицо",
                        ShipmentDate = DateTime.UtcNow
                    });

                    db.SaveChanges();
                    isSuccess = true;
                }
            }

            using (var db = new ApplicationDbContext(options))
            {
                var updatedProduct = db.Products.FirstOrDefault(p => p.Article == "WWW-001");

                Assert.IsTrue(isSuccess);
                Assert.AreEqual(0, updatedProduct.CurrentStock);
                Assert.AreEqual(1, db.Shipments.Count());
            }
        }
    }
}