namespace WinFormsApp1.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; init; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
