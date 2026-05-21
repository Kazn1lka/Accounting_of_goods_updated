namespace WinFormsApp1.Models
{
    public class ShipmentItem
    {
        public int Id { get; set; }

        public int ShipmentId { get; init; }
        public Shipment Shipment { get; set; }

        public int ProductId { get; init; }
        public Product Product { get; set; }

        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }
}
