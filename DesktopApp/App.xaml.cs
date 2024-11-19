using Database.Efc;
using Database.Records;
using Database.Repositories;
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
            // Реєстрація DbContext
            services.AddDbContext<AppDbContext>();

            // Реєстрація репозиторіїв
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<PlayerStatistic>, PlayerStatisticRepository>();
            services.AddScoped<IRepository<GameSession>, GameSessionRepository>();
            services.AddScoped<IRepository<Achievement>, AchievementRepository>();
            services.AddScoped<IRepository<Feedback>, FeedbackRepository>();
            services.AddScoped<IRepository<Report>, ReportRepository>();

            // Реєстрація ViewModel
            services.AddScoped<MainViewModel>();

            // Реєстрація Views
            services.AddScoped<MainWindow>();
        }

    }

}
