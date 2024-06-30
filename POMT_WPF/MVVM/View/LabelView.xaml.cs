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
        public LabelView()
        {
            ErrorService.Instance().LabelServiceValidateFilePath += ValidateFileServiceErrorWindow;
            InitializeComponent();
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
    }
}
