namespace WinFormsApp1.Models
{
    public class WriteOff
    {
        public int Id { get; set; }
        public int ProductId { get; init; }
        public Product Product { get; set; }
        public int UserId { get; init; }
        public User User { get; set; }
        public int Quantity { get; init; }
        public string Reason { get; init; }
        public DateTime WriteOffDate { get; init; }
        public string CurrencyAtWriteOff { get; init; } = "RUB";
        public decimal RateAtWriteOff { get; init; } = 1m;
        public string RatesJson { get; init; } = "{}";
    }
}
