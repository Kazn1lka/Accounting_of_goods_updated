namespace WinFormsApp1.Interfaces
{
    public interface IDeliveryService
    {
        List<string> GetProductNames();
        object GetProductSizes(string productName);
        void ProcessDelivery(List<DeliveryItemDto> items);
    }
    public class DeliveryItemDto
    {
        public string Article { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; } 
        public DateTime ExpiryDate { get; set; }
    }
}
