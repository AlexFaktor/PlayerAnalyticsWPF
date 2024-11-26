using Database.Efc;
using Database.Records;
using Database.Repositories;
using DesktopApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public static UserRecord? CurrentUser;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            base.OnStartup(e);

            // Встановіть головне вікно
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Створення DbContext
            DbInitializer.InitializeDatabase(new AppDbContext());

            // Реєстрація DbContext
            services.AddDbContext<AppDbContext>();

            // Реєстрація репозиторіїв
            services.AddScoped<UserRepository>();
            services.AddScoped<PlayerStatisticRepository>();
            services.AddScoped<GameSessionRepository>();
            services.AddScoped<AchievementRepository>();
            services.AddScoped<FeedbackRepository>();
            services.AddScoped<ReportRepository>();

            // Реєстрація ViewModel
            services.AddSingleton<MainViewModel>();

            // Реєстрація Views
            services.AddSingleton<MainWindow>();
        }
    }
}
