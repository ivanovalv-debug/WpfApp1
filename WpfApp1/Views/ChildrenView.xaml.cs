using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class ChildrenView : UserControl
    {
        public ChildrenView(ChildrenViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.LoadDataCommand.Execute(null);
        }
    }
}