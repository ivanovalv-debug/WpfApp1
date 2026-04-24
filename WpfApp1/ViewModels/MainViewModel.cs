using System;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainViewModel
    {
        public event EventHandler<string>? ViewRequested;
        public Люди? CurrentUser { get; set; }

        public ICommand OpenPeopleCommand { get; }
        public ICommand OpenChildrenCommand { get; }
        public ICommand OpenBookingCommand { get; }
        public ICommand OpenInstitutionsCommand { get; }
        public ICommand OpenReportsCommand { get; }
        public ICommand LogoutCommand { get; }

        // Свойства для отображения
        public string UserDisplayName => CurrentUser != null ?
            $"👤 {CurrentUser.Фио}" : "Пользователь";

        public string UserRoleDisplay => CurrentUser != null ?
            GetRoleName(CurrentUser.Роль) : "";

        // Флаги видимости кнопок
        public bool CanManageUsers => CurrentUser?.Роль == "администратор";
        public bool CanViewChildren => CurrentUser?.Роль == "администратор" ||
                                        CurrentUser?.Роль == "оператор" ||
                                        CurrentUser?.Роль == "медработник" ||
                                        CurrentUser?.Роль == "родитель";
        public bool CanViewBookings => CurrentUser?.Роль == "администратор" ||
                                        CurrentUser?.Роль == "оператор" ||
                                        CurrentUser?.Роль == "родитель";
        public bool CanManageInstitutions => CurrentUser?.Роль == "администратор" ||
                                              CurrentUser?.Роль == "оператор";
        public bool CanViewReports => CurrentUser?.Роль == "администратор" ||
                                       CurrentUser?.Роль == "оператор";

        public MainViewModel()
        {
            OpenPeopleCommand = new RelayCommand(OpenPeople, () => CanManageUsers);
            OpenChildrenCommand = new RelayCommand(OpenChildren, () => CanViewChildren);
            OpenBookingCommand = new RelayCommand(OpenBooking, () => CanViewBookings);
            OpenInstitutionsCommand = new RelayCommand(OpenInstitutions, () => CanManageInstitutions);
            OpenReportsCommand = new RelayCommand(OpenReports, () => CanViewReports);
            LogoutCommand = new RelayCommand(Logout);
        }

        private string GetRoleName(string role)
        {
            return role switch
            {
                "администратор" => "🔑 Администратор",
                "оператор" => "📞 Оператор",
                "медработник" => "🏥 Медработник",
                "родитель" => "👨‍👩‍👧 Родитель",
                _ => role
            };
        }

        private void OpenPeople() => ViewRequested?.Invoke(this, "People");
        private void OpenChildren() => ViewRequested?.Invoke(this, "Children");
        private void OpenBooking() => ViewRequested?.Invoke(this, "Booking");
        private void OpenInstitutions() => ViewRequested?.Invoke(this, "Institutions");
        private void OpenReports() => ViewRequested?.Invoke(this, "Reports");

        private void Logout()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}