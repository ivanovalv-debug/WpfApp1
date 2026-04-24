using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class ParentView : UserControl
    {
        public ParentView(ParentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
