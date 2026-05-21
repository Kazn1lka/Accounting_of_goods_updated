namespace Accounting_of_goods.Models
{
    public class Supply
    {
        public int Id { get; set; }
        public int ProductId { get; init; }
        public Product Product { get; set; }
        public int Quantity { get; set; }  // уменьшается при FIFO-списании в сервисах
        public decimal PurchasePrice { get; init; }
        public decimal RateAtSupply { get; init; }
        public string CurrencyAtSupply { get; init; }
        public DateTime SupplyDate { get; init; } = DateTime.UtcNow;
        public decimal SellingPrice { get; init; }
        public DateTime? ExpiryDate { get; init; }
    }
}
