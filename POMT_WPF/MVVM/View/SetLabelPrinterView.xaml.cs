using POMT_WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SetLabelPrinterView.xaml
    /// </summary>
    public partial class SetLabelPrinterView : Window
    {
        SetLabelPrinterViewModel viewModel;
        public string selection;
        public SetLabelPrinterView()
        {
            InitializeComponent();
            viewModel = new SetLabelPrinterViewModel();
            printerListbox.ItemsSource = viewModel.printerNames;
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            //Do something
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            if(printerListbox.SelectedItem != null)
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
