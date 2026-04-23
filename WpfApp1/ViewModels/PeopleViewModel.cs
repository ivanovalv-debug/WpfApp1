using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Data;
using WpfApp1.Models;
using WpfApp1.Commands;

namespace WpfApp1.ViewModels
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        private readonly SanatoriumContext _context;
        private Люди _selectedPerson;
        private string _searchText;

        public PeopleViewModel(SanatoriumContext context)
        {
            _context = context;
            PeopleList = new ObservableCollection<Люди>();

            LoadDataCommand = new RelayCommand(LoadData);
            AddCommand = new RelayCommand(Add, CanAdd);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        public ObservableCollection<Люди> PeopleList { get; set; }

        public Люди SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        private void LoadData()
        {
            PeopleList.Clear();
            // ИСПОЛЬЗУЕМ Людиs (с суффиксом s!)
            var data = _context.Людиs.ToList();
            foreach (var item in data) PeopleList.Add(item);
        }

        private void FilterData()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) { LoadData(); return; }
            PeopleList.Clear();
            var data = _context.Людиs
                .Where(p => p.Фио.Contains(SearchText) || p.Логин.Contains(SearchText))
                .ToList();
            foreach (var item in data) PeopleList.Add(item);
        }

        private void Add()
        {
            if (SelectedPerson == null) SelectedPerson = new Люди();
            if (string.IsNullOrWhiteSpace(SelectedPerson.Фио)) return;

            _context.Людиs.Add(SelectedPerson);
            _context.SaveChanges();
            LoadData();
            Clear();
        }

        private void Update()
        {
            if (SelectedPerson == null || SelectedPerson.Id == 0) return;
            _context.Людиs.Update(SelectedPerson);
            _context.SaveChanges();
            LoadData();
        }

        private void Delete()
        {
            if (SelectedPerson == null || SelectedPerson.Id == 0) return;
            _context.Людиs.Remove(SelectedPerson);
            _context.SaveChanges();
            LoadData();
            Clear();
        }

        private void Clear()
        {
            SelectedPerson = new Люди();
            OnPropertyChanged(nameof(SelectedPerson));
        }

        private bool CanAdd() => SelectedPerson != null && !string.IsNullOrWhiteSpace(SelectedPerson.Фио);
        private bool CanUpdate() => SelectedPerson != null && SelectedPerson.Id > 0;
        private bool CanDelete() => SelectedPerson != null && SelectedPerson.Id > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}