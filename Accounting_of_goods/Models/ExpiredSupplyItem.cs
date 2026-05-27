namespace WinFormsApp1.Models
{
    public class ExpiredSupplyItem
    {
        public int SupplyId { get; set; }
        public int ProductId { get; set; }
        public string Article { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string Size { get; set; } = "";
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int DaysExpired { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
