using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Data;
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

            // Обновляем UI после установки DataContext
            UpdateUIForRole();
        }

        private void UpdateUIForRole()
        {
            // Принудительно обновляем видимость кнопок
            var buttons = FindVisualChildren<Button>(this);
            foreach (var button in buttons)
            {
                if (button.Command is ICommand cmd)
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private void OnViewRequested(object? sender, string viewName)
        {
            var contentArea = FindName("ContentArea") as ContentControl;
            if (contentArea == null) return;

            // Если пользователь - родитель, показываем специальный интерфейс
            if (_viewModel.CurrentUser?.Роль == "родитель")
            {
                if (viewName == "Children" || viewName == "Booking")
                {
                    var parentVm = new ParentViewModel(
                        _serviceProvider.GetService<SanatoriumContext>()!,
                        _viewModel.CurrentUser.Id);
                    contentArea.Content = new ParentView(parentVm);
                    return;
                }
            }

            // Стандартные view для сотрудников
            switch (viewName)
            {
                case "People":
                    contentArea.Content = _serviceProvider.GetService<PeopleView>();
                    break;
                case "Children":
                    contentArea.Content = _serviceProvider.GetService<ChildrenView>();
                    break;
                case "Booking":
                    contentArea.Content = _serviceProvider.GetService<BookingView>();
                    break;
            }
        }

        // Вспомогательный метод для поиска элементов
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(depObj, i);
                if (child is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
            }
        }
    }
}