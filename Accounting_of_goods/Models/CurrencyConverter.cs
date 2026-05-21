namespace Accounting_of_goods
{
    public static class CurrencyConverter
    {
        public static string CurrentCurrency { get; set; } = "RUB";
        public static decimal CurrentRate { get; private set; } = 1m;
        public static string CurrentRatesJson { get; private set; } = "{\"RUB\": 1}";

        private static readonly HttpClient client = new HttpClient();

        public static async Task EnsureRatesLoadedAsync()
        {
            if (CurrentRatesJson == "{\"RUB\": 1}")
            {
                await ChangeCurrencyAsync(CurrentCurrency);
            }
        }

        public static async Task ChangeCurrencyAsync(string targetCurrency)
        {
            if (targetCurrency == "RUB" && CurrentRatesJson != "{\"RUB\": 1}")
            {
                CurrentCurrency = "RUB";
                CurrentRate = 1m;
                return;
            }

            try
            {
             
                string url = "https://open.er-api.com/v6/latest/RUB";
                string response = await client.GetStringAsync(url);

                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement rates = doc.RootElement.GetProperty("rates");
                    CurrentRatesJson = rates.GetRawText();
                    if (rates.TryGetProperty(targetCurrency, out JsonElement rateElement))
                    {
                        CurrentRate = rateElement.GetDecimal();
                        CurrentCurrency = targetCurrency;
                    }
                }
            }
            catch (Exception ex)
            {
              
                System.Windows.Forms.MessageBox.Show($"РћС€РёР±РєР° Р·Р°РіСЂСѓР·РєРё РєСѓСЂСЃР°: {ex.Message}", "РћС€РёР±РєР°",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                CurrentCurrency = "RUB";
                CurrentRate = 1m;
            }
        }

        public static decimal ConvertPrice(decimal priceInRub)
        {
            return Math.Round(priceInRub * CurrentRate, 2);
        }
    }
}
