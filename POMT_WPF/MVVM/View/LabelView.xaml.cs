using Petsi.Events;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelView.xaml
    /// </summary>
    public partial class LabelView : UserControl
    {
        LabelViewModel ViewModel;
        public LabelView()
        {
            ErrorService.Instance().LabelServiceValidateFilePath += ValidateFileServiceErrorWindow;
            InitializeComponent();
            ViewModel = new LabelViewModel();
        }

        private void PrintStandardLabelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null)
            { PetsiOrderFormErrorWindow error = new PetsiOrderFormErrorWindow("Please select a date."); return; }
            ViewModel.PrintStandard(datePicker.SelectedDate.Value);
        }

        private void PrintSmallLabelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null)
            { PetsiOrderFormErrorWindow error = new PetsiOrderFormErrorWindow("Please select a date."); return; }
            ViewModel.PrintSmall(datePicker.SelectedDate.Value);
        }

        private void PrintRoundLabelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null)
            { PetsiOrderFormErrorWindow error = new PetsiOrderFormErrorWindow("Please select a date."); return; }
            ViewModel.PrintRound(datePicker.SelectedDate.Value);
        }
        private void ValidateFileServiceErrorWindow(object sender, EventArgs e)
        {
            LabelServiceValidateFpEventArgs args = (LabelServiceValidateFpEventArgs)e;
            CatalogService cmp = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi item = cmp.GetCatalogItemById(args.CatalogId);
            PetsiOrderFormErrorWindow errorWindow = new PetsiOrderFormErrorWindow(
                "Item: " + item.ItemName + " filepath: " + args.Filepath + " for " + args.PieType + " could not be validated. Please verify that the item's file assoicated with the label currently exists or is correct.");
            errorWindow.Show();
        }

        private void ConfigureLabelsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
