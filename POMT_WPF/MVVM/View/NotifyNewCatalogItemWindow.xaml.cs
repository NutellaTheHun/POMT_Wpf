using Petsi.Events;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyNewCatalogItemWindow.xaml
    /// </summary>
    public partial class NotifyNewCatalogItemWindow : Window
    {
        NotifyNewCatalogItemViewModel viewModel;
        public NotifyNewCatalogItemWindow(SoiNewItemEventArgs args)
        {
            viewModel = new NotifyNewCatalogItemViewModel(args, this);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
