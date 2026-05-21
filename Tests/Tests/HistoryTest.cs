using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class HistoryTest
    {
        private DbContextOptions<ApplicationDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void ExportData_FilterByBrand_ShouldReturnOnlySelectedBrand()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(new Product { Article = "A1", Name = "Кроссовки", Brand = "Nike", Size = "42", CurrentStock = 10 });
                db.Products.Add(new Product { Article = "A2", Name = "Худи", Brand = "Nike", Size = "L", CurrentStock = 5 });
                db.Products.Add(new Product { Article = "A3", Name = "Футболка", Brand = "Prada", Size = "M", CurrentStock = 2 });
                db.SaveChanges();
            }

            List<Product> exportList;
            string brandToFilter = "Nike";

            using (var db = new ApplicationDbContext(options))
            {
                exportList = db.Products
                    .Where(p => p.Brand == brandToFilter)
                    .ToList();
            }

            Assert.AreEqual(2, exportList.Count);
            Assert.IsTrue(exportList.All(p => p.Brand == "Nike"));
            Assert.IsFalse(exportList.Any(p => p.Brand == "Prada"));
        }

        [TestMethod]
        public void ExportData_EmptyStock_ShouldReturnNothing()
        {
            var options = GetDbOptions();
            using (var db = new ApplicationDbContext(options))
            {
                db.Products.Add(new Product { Article = "A1", Name = "Джинсы", Brand = "Levi's", Size = "32", CurrentStock = 0 });
                db.SaveChanges();
            }

            List<Product> exportList;

            using (var db = new ApplicationDbContext(options))
            {
                exportList = db.Products
                    .Where(p => p.CurrentStock > 0)
                    .ToList();
            }

            Assert.AreEqual(0, exportList.Count);
        }
    }
}