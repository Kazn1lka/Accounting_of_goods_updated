namespace WinFormsApp1.Models
{
    /// <summary>Сводная информация о контрагенте после проверки по ИНН.</summary>
    public record CounterpartyInfo
    {
        public string Inn { get; init; }
        public string Kpp { get; init; }
        public string Ogrn { get; init; }
        public string FullName { get; init; }
        public string ShortName { get; init; }
        public string Status { get; init; }         // ACTIVE / LIQUIDATING / LIQUIDATED / REORGANIZING / BANKRUPT
        public string StatusDescription { get; init; }
        public string Address { get; init; }
        public string DirectorName { get; init; }

        // Результаты проверок «чёрных списков»
        public bool? IsTaxDebtor { get; init; }          // null = не удалось проверить
        public bool? IsBankrupt { get; init; }
        public bool? HasDisqualifiedDirectors { get; init; }

        public string TaxDebtorCheckError { get; init; }
        public string BankruptCheckError { get; init; }
        public string DisqualifiedCheckError { get; init; }
    }
}
