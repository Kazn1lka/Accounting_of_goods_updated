
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Services;
using WinFormsApp1.Models;
using Accounting_of_goods.Models;

namespace Accounting_of_goodsTests.Tests
{
    [TestClass]
    public class HeatMapServiceTests
    {
        private ApplicationDbContext _dbContext;
        private HeatMapService _service;
        private HeatMapSettings _testSettings;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _service = new HeatMapService(_dbContext);

            _testSettings = new HeatMapSettings
            {
                Mode = HeatMapMode.Expiry,
                GreenThresholdDays = 90,
                YellowThresholdDays = 20,
                OrangeThresholdDays = 7
            };
        }

        [TestCleanup]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [TestMethod]
        public void GetCells_ShouldGroupSameArticleAndSize_AndSumQuantity()
        {
          
            var category = new Category { Id = 1, Name = "Тестовая категория" };
            _dbContext.Categories.Add(category);

            
            var product = new Product { Id = 1, CategoryId = 1, Article = "TSHIRT-01", Brand = "Nike", Name = "Футболка", Size = "M" };
            _dbContext.Products.Add(product);

            _dbContext.Supplies.AddRange(new List<Supply>
            {
                new Supply { Id = 1, ProductId = 1, Quantity = 30, PurchasePrice = 100, SellingPrice = 150, CurrencyAtSupply = "RUB", ExpiryDate = DateTime.UtcNow.AddDays(100) },
                new Supply { Id = 2, ProductId = 1, Quantity = 20, PurchasePrice = 100, SellingPrice = 150, CurrencyAtSupply = "RUB", ExpiryDate = DateTime.UtcNow.AddDays(120) }
            });
            _dbContext.SaveChanges();

            var cells = _service.GetCells(_testSettings);

            Assert.AreEqual(1, cells.Count);
            Assert.AreEqual(50, cells[0].Quantity);
        }

        [TestMethod]
        public void GetCells_ExpiryMode_ShouldAssignCorrectColors()
        {
            var category = new Category { Id = 1, Name = "Тестовая категория" };
            _dbContext.Categories.Add(category);

            var p1 = new Product { Id = 1, CategoryId = 1, Article = "G", Brand = "B", Name = "N1", Size = "S" };
            var p2 = new Product { Id = 2, CategoryId = 1, Article = "O", Brand = "B", Name = "N2", Size = "M" };
            var p3 = new Product { Id = 3, CategoryId = 1, Article = "R", Brand = "B", Name = "N3", Size = "L" };
            _dbContext.Products.AddRange(p1, p2, p3);

            _dbContext.Supplies.AddRange(new List<Supply>
            {
                new Supply { Id = 1, ProductId = 1, Quantity = 10, CurrencyAtSupply = "RUB", ExpiryDate = DateTime.UtcNow.AddDays(100) }, 
                new Supply { Id = 2, ProductId = 2, Quantity = 10, CurrencyAtSupply = "RUB", ExpiryDate = DateTime.UtcNow.AddDays(15) },  
                new Supply { Id = 3, ProductId = 3, Quantity = 10, CurrencyAtSupply = "RUB", ExpiryDate = DateTime.UtcNow.AddDays(3) }    
            });
            _dbContext.SaveChanges();

            var cells = _service.GetCells(_testSettings);

            Assert.AreEqual(HeatCellColor.Green, cells.First(c => c.Article == "G").Color);
            Assert.AreEqual(HeatCellColor.Orange, cells.First(c => c.Article == "O").Color);
            Assert.AreEqual(HeatCellColor.Red, cells.First(c => c.Article == "R").Color);
        }

        [TestMethod]
        public void GetStaleCount_ShouldReturnCorrectCount()
        {
            var category = new Category { Id = 1, Name = "Тестовая категория" };
            _dbContext.Categories.Add(category);

            var p1 = new Product { Id = 1, CategoryId = 1, Article = "A1", Brand = "B", Name = "N", Size = "S" };
            _dbContext.Products.Add(p1);

            _dbContext.Supplies.Add(new Supply { Id = 1, ProductId = 1, Quantity = 5, CurrencyAtSupply = "RUB", ExpiryDate = DateTime.UtcNow.AddDays(5) });
            _dbContext.SaveChanges();

            int result = _service.GetStaleCount(thresholdDays: 7);

            Assert.AreEqual(1, result);
        }
    }
}