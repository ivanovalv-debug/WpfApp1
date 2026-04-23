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
    public class ChildrenViewModel : INotifyPropertyChanged
    {
        private readonly SanatoriumContext _context;
        private Ребенок _selectedChild;

        public ChildrenViewModel(SanatoriumContext context)
        {
            _context = context;
            ChildrenList = new ObservableCollection<Ребенок>();
            ParentsList = new ObservableCollection<Люди>();

            
        }

        public ObservableCollection<Ребенок> ChildrenList { get; set; }
        public ObservableCollection<Люди> ParentsList { get; set; }

        public Ребенок SelectedChild
        {
            get => _selectedChild;
            set
            {
                _selectedChild = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        private void LoadData()
        {
            ChildrenList.Clear();
            ParentsList.Clear();

            // ИСПОЛЬЗУЕМ Ребенокs и Людиs (с суффиксом s!)
            var children = _context.Ребенокs
                .Include(c => c.FkРодительNavigation)
                .ToList();

            var parents = _context.Людиs.Where(l => l.Роль == "родитель").ToList();

            foreach (var c in children) ChildrenList.Add(c);
            foreach (var p in parents) ParentsList.Add(p);
        }

        private void Add()
        {
            if (SelectedChild == null || string.IsNullOrWhiteSpace(SelectedChild.Фио)) return;
            _context.Ребенокs.Add(SelectedChild);
            _context.SaveChanges();
            LoadData();
            Clear();
        }

        private void Update()
        {
            if (SelectedChild == null || SelectedChild.Id == 0) return;
            _context.Ребенокs.Update(SelectedChild);
            _context.SaveChanges();
            LoadData();
        }

        private void Delete()
        {
            if (SelectedChild == null || SelectedChild.Id == 0) return;
            _context.Ребенокs.Remove(SelectedChild);
            _context.SaveChanges();
            LoadData();
            Clear();
        }

        private void Clear()
        {
            SelectedChild = new Ребенок();
            OnPropertyChanged(nameof(SelectedChild));
        }

        private bool CanAdd() => SelectedChild != null && !string.IsNullOrWhiteSpace(SelectedChild.Фио) && SelectedChild.FkРодитель > 0;
        private bool CanUpdate() => SelectedChild != null && SelectedChild.Id > 0;
        private bool CanDelete() => SelectedChild != null && SelectedChild.Id > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}