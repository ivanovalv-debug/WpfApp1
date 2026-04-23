using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class PeopleView : UserControl
    {
        public PeopleView(PeopleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.LoadDataCommand.Execute(null);
        }
    }
}