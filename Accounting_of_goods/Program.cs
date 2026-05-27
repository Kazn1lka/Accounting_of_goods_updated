namespace WinFormsApp1
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Serilog.Log.Logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/app_.txt", rollingInterval: Serilog.RollingInterval.Day)
                .CreateLogger();

            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog(dispose: true);
            });

            services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Transient);

            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<RegisterForm>();
            services.AddTransient<HistoryForm>();
            services.AddTransient<ProductAddForm>();
            services.AddTransient<ProductEditForm>();
            services.AddTransient<ShipmentForm>();
            services.AddTransient<dgvCategories>();
            services.AddTransient<DeliveryForm>();
            services.AddTransient<WriteOffForm>();
            services.AddTransient<CounterpartyForm>();
            services.AddTransient<HeatMapForm>();
            services.AddTransient<HeatMapSettingsForm>();
            services.AddTransient<WeatherForm>();
            services.AddTransient<TempThresholdForm>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<IShipmentService, ShipmentService>();
            services.AddTransient<IWriteOffService, WriteOffService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICounterpartyService, CounterpartyService>();
            services.AddTransient<IHeatMapService, HeatMapService>();
            services.AddTransient<IWeatherService, WeatherService>();
            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<LoginForm>());
        }
    }
}
