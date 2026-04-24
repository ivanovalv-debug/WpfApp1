using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Commands;
using WpfApp1.Data;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private readonly SanatoriumContext _context;
        private Бронирование _selectedBooking;

        public BookingViewModel(SanatoriumContext context)
        {
            _context = context;
            BookingsList = new ObservableCollection<Бронирование>();
            ChildrenList = new ObservableCollection<Ребенок>();
            SessionsList = new ObservableCollection<Смены>();
            OperatorsList = new ObservableCollection<Люди>();

            // Инициализация команд
            LoadDataCommand = new RelayCommand(LoadData);
            AddCommand = new RelayCommand(Add, CanAdd);
            UpdateStatusCommand = new RelayCommand(UpdateStatus, CanUpdate);
        }

        public ObservableCollection<Бронирование> BookingsList { get; set; }
        public ObservableCollection<Ребенок> ChildrenList { get; set; }
        public ObservableCollection<Смены> SessionsList { get; set; }
        public ObservableCollection<Люди> OperatorsList { get; set; }

        public Бронирование SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateStatusCommand { get; }

        private void LoadData()
        {
            BookingsList.Clear();
            ChildrenList.Clear();
            SessionsList.Clear();
            OperatorsList.Clear();

            var bookings = _context.Бронированиеs
                .Include(b => b.FkРебенкаNavigation)
                .Include(b => b.FkСменаNavigation)
                .Include(b => b.FkОператорNavigation)
                .ToList();

            var children = _context.Ребенокs.ToList();
            var sessions = _context.Сменыs.ToList();
            var operators = _context.Людиs.Where(l => l.Роль == "оператор").ToList();

            foreach (var b in bookings) BookingsList.Add(b);
            foreach (var c in children) ChildrenList.Add(c);
            foreach (var s in sessions) SessionsList.Add(s);
            foreach (var o in operators) OperatorsList.Add(o);
        }

        private void Add()
        {
            if (SelectedBooking == null || SelectedBooking.FkРебенка == 0)
                return;

            SelectedBooking.Статус = "новая";
            SelectedBooking.ДатаОформления = DateTime.Now.Date; // .Date убирает время, если там DateOnly

            _context.Бронированиеs.Add(SelectedBooking);
            _context.SaveChanges();
            LoadData();
        }

        private void UpdateStatus()
        {
            if (SelectedBooking == null || SelectedBooking.Id == 0)
                return;

            _context.Бронированиеs.Update(SelectedBooking);
            _context.SaveChanges();
            LoadData();
        }

        private bool CanAdd()
        {
            return SelectedBooking != null &&
                   SelectedBooking.FkРебенка > 0 &&
                   SelectedBooking.FkСмена > 0;
        }

        private bool CanUpdate()
        {
            return SelectedBooking != null && SelectedBooking.Id > 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
