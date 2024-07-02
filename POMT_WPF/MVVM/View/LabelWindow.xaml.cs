using Petsi.Events;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelWindow.xaml
    /// </summary>
    public partial class LabelWindow : Window
    {
        LabelViewModel viewModel;
        DateTime targetDate;

        enum LabelTypes
        {
            Standard,
            Small,
            Round
        }
        LabelTypes selectedType;

        public LabelWindow()
        {
            ErrorService.Instance().LabelServiceValidateFilePath += ValidateFileServiceErrorWindow;
            InitializeComponent();
            viewModel = new LabelViewModel();
            DataContext = this;
            
        }
        private void Standard_ButtonClick(Object sender, RoutedEventArgs e)
        {
            selectedType = LabelTypes.Standard;
        }
        private void Small_ButtonClick(Object sender, RoutedEventArgs e)
        {
            selectedType = LabelTypes.Small;
        }
        private void Round_ButtonClick(Object sender, RoutedEventArgs e)
        {
            selectedType = LabelTypes.Round;
        }
        private void Print_ButtonClick(Object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null) 
            { GeneralErrorWindow error = new GeneralErrorWindow("Please select a date."); return; }

            switch (selectedType)
            {
                case LabelTypes.Standard:
                    //viewModel.PrintStandard(targetDate);
                break;

                case LabelTypes.Small:
                    //viewModel.PrintSmall(targetDate);
                break;

                case LabelTypes.Round:
                   // viewModel.PrintRound(targetDate);
                break;

                default:
                    SystemLogger.Log("LABELWINDOW: PRINT_BUTTONCLICK SWITCHCASE DEFAULT");
                break;
            }
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValidateFileServiceErrorWindow(object sender, EventArgs e)
        {
            LabelServiceValidateFpEventArgs args = (LabelServiceValidateFpEventArgs)e;
            CatalogService cmp = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi item = cmp.GetCatalogItemById(args.CatalogId);

            GeneralErrorWindow errorWindow = new GeneralErrorWindow(
                "Item: " + item.ItemName + " filepath: " + args.Filepath + " for " + args.PieType +" could not be validated. Please verify that the item's file assoicated with the label currently exists or is correct.");
            errorWindow.Show();
        }   
    }
}
