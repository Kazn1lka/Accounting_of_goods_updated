using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WinFormsApp1.Interfaces;

namespace WinFormsApp1.Services
{
    public class CounterpartyService : ICounterpartyService
    {
        private static readonly string DadataToken = "6827c9916b40b35282284945ddfbe9aa6eb1bc5";
        private static readonly string DadataSecret = "c5ed74682d541612d8e3902c6c126b95084d9b41";

        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        public (bool isValid, string error) ValidateInn(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
                return (false, "ИНН не может быть пустым.");

            inn = inn.Trim();

            if (!inn.All(char.IsDigit))
                return (false, "ИНН должен содержать только цифры.");

            return inn.Length switch
            {
                10 => ValidateInn10(inn),
                12 => ValidateInn12(inn),
                _ => (false, $"ИНН должен содержать 10 (юр. лицо) или 12 (ИП) цифр. Введено: {inn.Length}.")
            };
        }

        public async Task<CounterpartyInfo> CheckByInnAsync(string inn)
        {
            CounterpartyInfo info = await FetchFromDadataAsync(inn);

            var debtorTask = CheckTaxDebtorAsync(inn);
            var bankruptTask = CheckBankruptAsync(inn);
            var disqualTask = CheckDisqualifiedAsync(inn);

            await Task.WhenAll(debtorTask, bankruptTask, disqualTask);

            return info with
            {
                IsTaxDebtor = debtorTask.Result.result,
                TaxDebtorCheckError = debtorTask.Result.error,
                IsBankrupt = bankruptTask.Result.result,
                BankruptCheckError = bankruptTask.Result.error,
                HasDisqualifiedDirectors = disqualTask.Result.result,
                DisqualifiedCheckError = disqualTask.Result.error
            };
        }

        private static (bool, string) ValidateInn10(string inn)
        {
            int[] coeff = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            int control = (coeff.Select((c, i) => c * (inn[i] - '0')).Sum() % 11) % 10;
            return control == (inn[9] - '0')
                ? (true, null)
                : (false, "Контрольная цифра ИНН не совпадает. Проверьте введённые данные.");
        }

        private static (bool, string) ValidateInn12(string inn)
        {
            int[] coeff1 = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            int[] coeff2 = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            int c1 = (coeff1.Select((c, i) => c * (inn[i] - '0')).Sum() % 11) % 10;
            int c2 = (coeff2.Select((c, i) => c * (inn[i] - '0')).Sum() % 11) % 10;
            return c1 == (inn[10] - '0') && c2 == (inn[11] - '0')
                ? (true, null)
                : (false, "Контрольные цифры ИНН не совпадают. Проверьте введённые данные.");
        }

        private static async Task<CounterpartyInfo> FetchFromDadataAsync(string inn)
        {
            if (string.IsNullOrEmpty(DadataToken) || DadataToken == "YOUR_DADATA_TOKEN_HERE")
                return new CounterpartyInfo { Inn = inn, StatusDescription = "Токен dadata не настроен — данные недоступны." };

            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Post, "https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/party");
                req.Headers.Add("Authorization", $"Token {DadataToken}");
                req.Headers.Add("Accept", "application/json");
                req.Headers.TryAddWithoutValidation("User-Agent", "WinFormsApp1/1.0");
                req.Content = new StringContent($"{{\"query\":\"{inn}\",\"count\":1}}", Encoding.UTF8, "application/json");

                var resp = await _http.SendAsync(req);
                if (!resp.IsSuccessStatusCode)
                {
                    string errorContent = await resp.Content.ReadAsStringAsync();
                    return new CounterpartyInfo { Inn = inn, StatusDescription = $"Ошибка API dadata: {(int)resp.StatusCode}. Ответ API: {errorContent}" };
                }

                string json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var suggestions = doc.RootElement.GetProperty("suggestions");
                if (suggestions.GetArrayLength() == 0)
                    return new CounterpartyInfo { Inn = inn, StatusDescription = "Компания не найдена в базе ЕГРЮЛ/ЕГРИП." };

                var d = suggestions[0].GetProperty("data");

                string rawStatus = TryGet(d, "state", "status");
                string address = TryGetPath(d, "address", "value");
                string director = TryGetPath(d, "management", "name");
                string Ogrn = TryGet(d, "ogrn");
                string Kpp = TryGet(d, "kpp");
                string FullName = TryGet(d, "name", "full_with_opf");
                string ShortName = TryGet(d, "name", "short_with_opf");

                string statusDesc = rawStatus switch
                {
                    "ACTIVE" => "✅ Действующая",
                    "LIQUIDATING" => "⚠️ В процессе ликвидации",
                    "LIQUIDATED" => "❌ Ликвидирована",
                    "REORGANIZING" => "🔄 Реорганизация",
                    "BANKRUPT" => "🚫 Банкротство",
                    _ => rawStatus ?? "Неизвестно"
                };

                return new CounterpartyInfo
                {
                    Inn = inn,
                    Kpp = Kpp,
                    Ogrn = Ogrn,
                    FullName = FullName,
                    ShortName = ShortName,
                    Status = rawStatus,
                    StatusDescription = statusDesc,
                    Address = address,
                    DirectorName = director
                };
            }
            catch (Exception ex)
            {
                return new CounterpartyInfo { Inn = inn, StatusDescription = $"Ошибка при запросе: {ex.Message}" };
            }
        }

        private static async Task<(bool? result, string error)> CheckTaxDebtorAsync(string inn)
        {
            try
            {
                string url = $"https://pb.nalog.ru/company-ul.json?query={inn}&mode=1";
                string json = await _http.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("companyUl", out var arr) && arr.GetArrayLength() > 0)
                {
                    var company = arr[0];
                    if (company.TryGetProperty("debt", out var debt))
                    {
                        bool isDebtor = debt.GetDecimal() > 0;
                        return (isDebtor, null);
                    }
                    return (false, null);
                }
                return (false, null);
            }
            catch
            {
                return (null, "Сервис ФНС временно недоступен");
            }
        }

        private static async Task<(bool? result, string error)> CheckBankruptAsync(string inn)
        {
            try
            {
                string url = $"https://bankrot.fedresurs.ru/api/v1/bankrupts?inn={inn}&limit=1";
                var resp = await _http.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                    return (null, $"ЕФРСБ: HTTP {(int)resp.StatusCode}");

                string json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("total", out var total))
                    return (total.GetInt32() > 0, null);

                if (doc.RootElement.TryGetProperty("items", out var items))
                    return (items.GetArrayLength() > 0, null);

                return (false, null);
            }
            catch
            {
                return (null, "Реестр банкротств временно недоступен");
            }
        }

        private static async Task<(bool? result, string error)> CheckDisqualifiedAsync(string inn)
        {
            try
            {
                string url = $"https://service.nalog.ru/disqualified.json?q={inn}";
                string json = await _http.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("rows", out var rows))
                    return (rows.GetArrayLength() > 0, null);

                return (false, null);
            }
            catch
            {
                return (null, "Реестр дисквалифицированных лиц временно недоступен");
            }
        }

        private static string TryGet(JsonElement el, params string[] path)
        {
            try
            {
                JsonElement cur = el;
                foreach (var key in path)
                {
                    if (!cur.TryGetProperty(key, out cur)) return null;
                }
                return cur.ValueKind == JsonValueKind.Null ? null : cur.GetString();
            }
            catch { return null; }
        }

        private static string TryGetPath(JsonElement el, string obj, string prop)
            => TryGet(el, obj, prop);
    }
}
