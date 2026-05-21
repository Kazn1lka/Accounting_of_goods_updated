namespace WinFormsApp1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<Supply> Supplies { get; set; }
        public DbSet<ShipmentItem> ShipmentItems { get; set; }
        public DbSet<WriteOff> WriteOffs { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DarkstoreDb;Username=postgres;Password=29zxur05ca07");
            }
        }
    }
}
