using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfApp1.Data;
using WpfApp1.Models;
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;
        public static Люди? CurrentUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddDbContext<SanatoriumContext>(options =>
                options.UseSqlServer(
                    "Data Source=dbsrv\\vip2025;Initial Catalog='Курсовой проект Иванова ЛВ Детский санаторий';Integrated Security=True;Trust Server Certificate=True;"),
                ServiceLifetime.Scoped);

            services.AddTransient<MainViewModel>();
            services.AddTransient<PeopleViewModel>();
            services.AddTransient<ChildrenViewModel>();
            services.AddTransient<BookingViewModel>();
            services.AddTransient<LoginViewModel>();

            services.AddTransient<MainWindow>();
            services.AddTransient<PeopleView>();
            services.AddTransient<ChildrenView>();
            services.AddTransient<BookingView>();
            services.AddTransient<LoginView>();

            _serviceProvider = services.BuildServiceProvider();

            // Показываем окно входа
            ShowLoginWindow();
        }

        private void ShowLoginWindow()
        {
            var loginWindow = _serviceProvider!.GetRequiredService<LoginView>();

            if (loginWindow.ShowDialog() == true)
            {
                // Успешный вход - сохраняем пользователя
                CurrentUser = loginWindow.CurrentUser;

                // Показываем главное окно с учетом роли
                ShowMainWindow();
            }
            else
            {
                // Выход из приложения
                Shutdown();
            }
        }

        private void ShowMainWindow()
        {
            var mainWindow = _serviceProvider!.GetRequiredService<MainWindow>();

            // Передаем информацию о текущем пользователе
            if (mainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.CurrentUser = CurrentUser;
            }

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
            base.OnExit(e);
        }
    }
}
