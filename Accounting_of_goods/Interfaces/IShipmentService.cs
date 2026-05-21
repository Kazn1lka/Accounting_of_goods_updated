namespace WinFormsApp1.Interfaces
{
    public interface IShipmentService
    {
        List<string> GetProductNames();
        List<string> GetSizesForProduct(string productName);
        object GetProductDetails(string productName, string size);
        void ProcessShipment(int userId, string recipient, List<ShipmentItemDto> items);
    }

    public class ShipmentItemDto
    {
        public string Article { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
