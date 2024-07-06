using Petsi.Events;
using Petsi.Services;
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
            ErrorService.RaiseLabelEvents();
        }
        private void ValidateFileServiceErrorWindow(object sender, EventArgs e)
        {
            LabelServiceValidateFpEventArgs args = (LabelServiceValidateFpEventArgs)e;
            
            GeneralErrorWindow errorWindow = new GeneralErrorWindow(
                "Item: " + args.ItemName + " filepath: " + args.Filepath + " for " + args.PieType + " could not be validated.\n Please verify that the item's file assoicated with the label currently exists or is correct.");
            errorWindow.Show();
        }
    }
}
