namespace WinFormsApp1.Models
{
    public record CounterpartyInfo
    {
        public string Inn { get; init; }
        public string Kpp { get; init; }
        public string Ogrn { get; init; }
        public string FullName { get; init; }
        public string ShortName { get; init; }
        public string Status { get; init; }
        public string StatusDescription { get; init; }
        public string Address { get; init; }
        public string DirectorName { get; init; }

        public bool? IsTaxDebtor { get; init; }
        public bool? IsBankrupt { get; init; }
        public bool? HasDisqualifiedDirectors { get; init; }

        public string TaxDebtorCheckError { get; init; }
        public string BankruptCheckError { get; init; }
        public string DisqualifiedCheckError { get; init; }
    }
}
