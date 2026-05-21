namespace WinFormsApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; init; }
        public string FirstName { get; init; }
        public string? MiddleName { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; set; }  // меняется при смене пароля

        public int RoleId { get; init; }
        public Role Role { get; set; }

        public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
