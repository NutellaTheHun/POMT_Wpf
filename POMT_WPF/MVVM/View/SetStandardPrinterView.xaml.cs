using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SetStandardPrinterView.xaml
    /// </summary>
    public partial class SetStandardPrinterView : Window
    {
        SetStandardPrinterViewModel viewModel;
        public string selection;
        public SetStandardPrinterView()
        {
            InitializeComponent();
            viewModel = new SetStandardPrinterViewModel();
            printerListbox.ItemsSource = viewModel.printers;

        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            //Do something
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            if (printerListbox.SelectedItem != null)
            {
                selection = (string)printerListbox.SelectedItem;
                Close();
            }
            else
            {
                PetsiOrderFormErrorWindow errorWindow =
                     new PetsiOrderFormErrorWindow("Please select a template.");
                errorWindow.Show();
                return;
            }
        }
    }
}
