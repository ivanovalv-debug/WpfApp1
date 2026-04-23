using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WpfApp1.Data;
using WpfApp1.Views;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            
            services.AddDbContext<SanatoriumContext>(options =>
                options.UseSqlServer(
                    "Server=DBSRV\\VIP2025;Database=Курсовой проект Иванова ЛВ Детский санаторий;Integrated Security=True;TrustServerCertificate=True;"),
                ServiceLifetime.Scoped);

            
            services.AddTransient<MainViewModel>();
            services.AddTransient<PeopleViewModel>();
            services.AddTransient<ChildrenViewModel>();
            services.AddTransient<BookingViewModel>();

            
            services.AddTransient<MainWindow>();
            services.AddTransient<PeopleView>();
            services.AddTransient<ChildrenView>();
            services.AddTransient<BookingView>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            (_serviceProvider as IDisposable)?.Dispose();
            base.OnExit(e);
        }
    }
}
