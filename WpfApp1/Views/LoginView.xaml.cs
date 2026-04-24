using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            _viewModel.LoginSuccessful += OnLoginSuccessful;
            _viewModel.ExitRequested += OnExitRequested;
        }

        private void OnLoginSuccessful(object sender, Люди user)
        {
            // Сохраняем текущего пользователя
            CurrentUser = user;
            this.DialogResult = true;
            this.Close();
        }

        private void OnExitRequested(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public Люди CurrentUser { get; private set; }
    }
}
