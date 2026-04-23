using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainViewModel viewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _serviceProvider = serviceProvider;
            DataContext = _viewModel;
            _viewModel.ViewRequested += OnViewRequested;
        }

        private void OnViewRequested(object sender, string viewName)
        {
            ContentControl contentArea = FindName("ContentArea") as ContentControl;

            switch (viewName)
            {
                case "People":
                    contentArea.Content = _serviceProvider.GetRequiredService<PeopleView>();
                    break;
                case "Children":
                    contentArea.Content = _serviceProvider.GetRequiredService<ChildrenView>();
                    break;
                case "Booking":
                    contentArea.Content = _serviceProvider.GetRequiredService<BookingView>();
                    break;
            }
        }
    }
}