using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Data;
using WpfApp1.Models;
using WpfApp1.Commands;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly SanatoriumContext _context;

        public MainViewModel(SanatoriumContext context)
        {
            _context = context;
        }

        public ICommand OpenPeopleCommand { get; }
        public ICommand OpenChildrenCommand { get; }
        public ICommand OpenBookingCommand { get; }

        public event EventHandler<string> ViewRequested;

        private void ShowView(string viewName)
        {
            ViewRequested?.Invoke(this, viewName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();
    }
}