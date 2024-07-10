using Petsi.Events;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelView.xaml
    /// </summary>
    public partial class LabelView : UserControl
    {
        public LabelView()
        {
            InitializeComponent();
            ErrorService.Instance().LabelServiceValidateFilePath += ValidateFileServiceErrorWindow;
            ErrorService.Instance().LabelPrinterNotFoundEvent += PrinterNotFoundErrorWindow;
            ErrorService.Instance().LabelPrinterNotFoundEvent += PrinterNotFoundErrorWindow;
            ErrorService.Instance().LabelFilePathNotSetEvent += LabelFilePathsNotSetErrorWindow;
            ErrorService.RaiseLabelEvents();
        }
        private void ValidateFileServiceErrorWindow(object sender, EventArgs e)
        {
            LabelServiceValidateFpEventArgs args = (LabelServiceValidateFpEventArgs)e;
            
            GeneralErrorWindow errorWindow = new GeneralErrorWindow(
                "Item: " + args.ItemName + " filepath: " + args.Filepath + " for " + args.PieType + " could not be validated.\n Please verify that the item's file assoicated with the label currently exists or is correct.");
            errorWindow.Owner = System.Windows.Application.Current.MainWindow;
            errorWindow.Show();
        }
        private void PrinterNotFoundErrorWindow(object sender, EventArgs e)
        {
            GeneralErrorWindow errorWindow = new GeneralErrorWindow("Printer not found, please check settings for correct printer.");
            errorWindow.Owner = System.Windows.Application.Current.MainWindow;
            errorWindow.Show();
        }
        private void LabelFilePathsNotSetErrorWindow(object sender, EventArgs e)
        {
            GeneralErrorWindow errorWindow = new GeneralErrorWindow("Standard or Cutie Label Filepath in settings not set.");
            errorWindow.Owner = System.Windows.Application.Current.MainWindow;
            errorWindow.Show();
        }
        private void InputLabelNotFoundErrorWindow(object sender, EventArgs e)
        {
            LabelServiceInputLabelNotFoundArgs args = (LabelServiceInputLabelNotFoundArgs)e;
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi item = cs.GetCatalogItemById(args.ItemId);
            GeneralErrorWindow errorWindow = new GeneralErrorWindow("Item " + item.ItemName + " doesn't have a label to print, please check the label configuration for the item.");

            errorWindow.Owner = System.Windows.Application.Current.MainWindow;
            errorWindow.Show();
        }
    }
}
