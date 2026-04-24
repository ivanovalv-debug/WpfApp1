using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Data;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class ParentViewModel : INotifyPropertyChanged
    {
        private readonly SanatoriumContext _context;
        private readonly int _parentId;

        public ParentViewModel(SanatoriumContext context, int parentId)
        {
            _context = context;
            _parentId = parentId;

            MyChildren = new ObservableCollection<Ребенок>();
            MyBookings = new ObservableCollection<Бронирование>();

            LoadData();
        }

        public ObservableCollection<Ребенок> MyChildren { get; set; }
        public ObservableCollection<Бронирование> MyBookings { get; set; }

        private void LoadData()
        {
            // Показываем только детей этого родителя
            var children = _context.Ребенокs
                .Where(c => c.FkРодитель == _parentId)
                .Include(c => c.Бронированиеs)
                .ToList();

            MyChildren.Clear();
            foreach (var child in children)
            {
                MyChildren.Add(child);

                // Добавляем бронирования детей
                foreach (var booking in child.Бронированиеs)
                {
                    MyBookings.Add(booking);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
