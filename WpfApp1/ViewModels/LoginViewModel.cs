using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Data;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel
    {
        private readonly SanatoriumContext _context;
        private string _login;
        private string _password;
        private string _errorMessage;

        public LoginViewModel(SanatoriumContext context)
        {
            _context = context;
            LoginCommand = new RelayCommand(Login);
            ExitCommand = new RelayCommand(Exit);
        }

        public string LoginText
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(LoginText));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand ExitCommand { get; }

        public event EventHandler<Люди> LoginSuccessful;
        public event EventHandler ExitRequested;

        private void Login(object parameter)
        {
            if (string.IsNullOrWhiteSpace(LoginText) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Введите логин и пароль";
                return;
            }

            // Поиск пользователя
            var user = _context.Людиs
                .FirstOrDefault(u => u.Логин == LoginText && u.Пароль == Password);

            if (user == null)
            {
                ErrorMessage = "Неверный логин или пароль";
                return;
            }

            // Успешный вход
            LoginSuccessful?.Invoke(this, user);
        }

        private void Exit(object parameter)
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
