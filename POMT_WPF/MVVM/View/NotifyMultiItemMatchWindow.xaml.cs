using Petsi.Events;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyMultiItemMatchWindow.xaml
    /// </summary>
    public partial class NotifyMultiItemMatchWindow : Window
    {
        private NotifyItemMultiMatchViewModel viewModel;

        public NotifyMultiItemMatchWindow(SoiMultiItemEventArgs args)
        {
            viewModel = new NotifyItemMultiMatchViewModel(args, this);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
