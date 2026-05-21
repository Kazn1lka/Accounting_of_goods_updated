namespace WinFormsApp1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Article { get; set; }       // редактируется
        public string Brand { get; set; }         // редактируется
        public string Name { get; set; }          // редактируется
        public string Size { get; set; }          // редактируется
        public decimal PurchasePrice { get; set; }
        public int CurrentStock { get; set; }     // изменяется при отгрузке/списании
        public decimal SellingPrice { get; set; } // редактируется
        public DateTime? ExpiryDate { get; set; } // редактируется

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
