using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class BookingView : UserControl
    {
        public BookingView(BookingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.LoadDataCommand.Execute(null);
        }
    }
}
