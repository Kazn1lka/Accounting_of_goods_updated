namespace WinFormsApp1.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime ShipmentDate { get; init; }
        public int Quantity { get; init; }
        public string Recipient { get; init; }
        public decimal SellingPriceAtShipment { get; init; }
        public string CurrencyAtShipment { get; init; }
        public decimal RateAtShipment { get; init; }
        public string RatesJson { get; init; } = "{}";
        public decimal TotalAmount { get; init; }

        public int UserId { get; init; }
        public User User { get; set; }

        public int ProductId { get; init; }
        public Product Product { get; set; }

        public int? SupplyId { get; init; }
        public Supply Supply { get; set; }

        public ICollection<ShipmentItem> ShipmentItems { get; set; } = new List<ShipmentItem>();
    }
}
