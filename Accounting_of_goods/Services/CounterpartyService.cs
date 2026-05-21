namespace WinFormsApp1.Services
{
    /// <summary>
    /// Сервис проверки контрагента по ИНН.
    /// Использует API dadata.ru для получения реквизитов и открытые данные ФНС для проверки статуса.
    /// </summary>
    public class CounterpartyService : ICounterpartyService
    {
        // API dadata.ru — бесплатный план: 10 000 запросов/сут.
        // Токен можно сменить в настройках без перекомпиляции через app.config / env-переменную.
        private static readonly string DadataToken =
            Environment.GetEnvironmentVariable("DADATA_TOKEN") ?? "YOUR_DADATA_TOKEN_HERE";

        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        // ---- Публичный интерфейс -----------------------------------------------

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
            // 1. Получаем основные реквизиты через dadata
            CounterpartyInfo info = await FetchFromDadataAsync(inn);

            // 2. Параллельно запускаем три дополнительных проверки
            var debtorTask     = CheckTaxDebtorAsync(inn);
            var bankruptTask   = CheckBankruptAsync(inn);
            var disqualTask    = CheckDisqualifiedAsync(inn);

            await Task.WhenAll(debtorTask, bankruptTask, disqualTask);

            // 3. Собираем итог
            return info with
            {
                IsTaxDebtor              = debtorTask.Result.result,
                TaxDebtorCheckError      = debtorTask.Result.error,
                IsBankrupt               = bankruptTask.Result.result,
                BankruptCheckError       = bankruptTask.Result.error,
                HasDisqualifiedDirectors = disqualTask.Result.result,
                DisqualifiedCheckError   = disqualTask.Result.error,
            };
        }

        // ---- Алгоритм контрольных цифр ИНН -------------------------------------

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

        // ---- dadata.ru: получение реквизитов -----------------------------------

        private static async Task<CounterpartyInfo> FetchFromDadataAsync(string inn)
        {
            // Если токен не задан — возвращаем базовый объект без дополнительных данных
            if (DadataToken == "YOUR_DADATA_TOKEN_HERE")
                return new CounterpartyInfo { Inn = inn, StatusDescription = "Токен dadata не настроен — данные недоступны." };

            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Post,
                    "https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/party");
                req.Headers.Add("Authorization", $"Token {DadataToken}");
                req.Content = new StringContent(
                    $"{{\"query\":\"{inn}\",\"count\":1}}",
                    Encoding.UTF8, "application/json");

                var resp = await _http.SendAsync(req);
                if (!resp.IsSuccessStatusCode)
                    return new CounterpartyInfo { Inn = inn, StatusDescription = $"Ошибка API dadata: {(int)resp.StatusCode}" };

                string json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var suggestions = doc.RootElement.GetProperty("suggestions");
                if (suggestions.GetArrayLength() == 0)
                    return new CounterpartyInfo { Inn = inn, StatusDescription = "Компания не найдена в базе ЕГРЮЛ/ЕГРИП." };

                var d = suggestions[0].GetProperty("data");

                string rawStatus = TryGet(d, "state", "status");
                string address   = TryGetPath(d, "address", "value");
                string director  = TryGetPath(d, "management", "name");
                string ogrn      = TryGet(d, "ogrn");
                string kpp       = TryGet(d, "kpp");
                string fullName  = TryGet(d, "name", "full_with_opf");
                string shortName = TryGet(d, "name", "short_with_opf");

                string statusDesc = rawStatus switch
                {
                    "ACTIVE"        => "✅ Действующая",
                    "LIQUIDATING"   => "⚠️ В процессе ликвидации",
                    "LIQUIDATED"    => "❌ Ликвидирована",
                    "REORGANIZING"  => "🔄 Реорганизация",
                    "BANKRUPT"      => "🚫 Банкротство",
                    _               => rawStatus ?? "Неизвестно"
                };

                return new CounterpartyInfo
                {
                    Inn             = inn,
                    Kpp             = kpp,
                    Ogrn            = ogrn,
                    FullName        = fullName,
                    ShortName       = shortName,
                    Status          = rawStatus,
                    StatusDescription = statusDesc,
                    Address         = address,
                    DirectorName    = director,
                };
            }
            catch (Exception ex)
            {
                return new CounterpartyInfo { Inn = inn, StatusDescription = $"Ошибка при запросе: {ex.Message}" };
            }
        }

        // ---- Проверка: налоговый должник (ФНС open-data) -----------------------
        // ФНС публикует CSV-файлы с ИНН должников на сайте оперативных данных.
        // Для простоты используем публичный эндпоинт search.nalog.ru (доступен без ключа).

        private static async Task<(bool? result, string error)> CheckTaxDebtorAsync(string inn)
        {
            try
            {
                // ФНС: сервис «Прозрачный бизнес» — информация о задолженности
                string url = $"https://pb.nalog.ru/company-ul.json?query={inn}&mode=1";
                string json = await _http.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);

                // Если в ответе есть поле "debt" > 0 — должник
                if (doc.RootElement.TryGetProperty("companyUl", out var arr) && arr.GetArrayLength() > 0)
                {
                    var company = arr[0];
                    if (company.TryGetProperty("debt", out var debt))
                    {
                        bool isDebtor = debt.GetDecimal() > 0;
                        return (isDebtor, null);
                    }
                    return (false, null); // долгов не обнаружено
                }
                return (false, null);
            }
            catch
            {
                // API недоступно — не блокируем форму, просто помечаем как «не проверено»
                return (null, "Сервис ФНС временно недоступен");
            }
        }

        // ---- Проверка: банкротство (Федресурс) ---------------------------------

        private static async Task<(bool? result, string error)> CheckBankruptAsync(string inn)
        {
            try
            {
                // Публичное API Единого федерального реестра сведений о банкротстве (ЕФРСБ)
                string url = $"https://bankrot.fedresurs.ru/api/v1/bankrupts?inn={inn}&limit=1";
                var resp = await _http.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                    return (null, $"ЕФРСБ: HTTP {(int)resp.StatusCode}");

                string json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("total", out var total))
                    return (total.GetInt32() > 0, null);

                // Если total не нашли, проверяем items
                if (doc.RootElement.TryGetProperty("items", out var items))
                    return (items.GetArrayLength() > 0, null);

                return (false, null);
            }
            catch
            {
                return (null, "Реестр банкротств временно недоступен");
            }
        }

        // ---- Проверка: дисквалифицированные руководители (ФНС) -----------------

        private static async Task<(bool? result, string error)> CheckDisqualifiedAsync(string inn)
        {
            try
            {
                // ФНС: реестр дисквалифицированных лиц (проверяем по ИНН организации)
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

        // ---- Вспомогательные методы --------------------------------------------

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
