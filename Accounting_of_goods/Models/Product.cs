namespace WinFormsApp1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public int CurrentStock { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
