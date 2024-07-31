using Petsi.Events;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyTableBuilderOverFlowWindow.xaml
    /// </summary>
    public partial class NotifyTableBuilderOverFlowWindow : Window
    {
        private NotifyTableBuilderOverFlowViewModel viewModel;
        public NotifyTableBuilderOverFlowWindow(TBOverflowEventArgs args)
        {
            viewModel = new NotifyTableBuilderOverFlowViewModel(args, this);
            InitializeComponent();
            DataContext = viewModel;
        }

        public void AddItems(TBOverflowEventArgs e)
        {
            viewModel.AddItems(e);
        }
    }
}
