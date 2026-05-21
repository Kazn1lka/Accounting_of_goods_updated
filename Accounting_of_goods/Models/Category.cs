namespace WinFormsApp1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }  // редактируется пользователем — остаётся set

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
