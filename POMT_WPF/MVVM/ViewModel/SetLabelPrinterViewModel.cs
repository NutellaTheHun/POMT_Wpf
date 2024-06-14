
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class SetLabelPrinterViewModel : ViewModelBase
    {
        public ObservableCollection<string> printerNames = new ObservableCollection<string>();

        public SetLabelPrinterViewModel()
        {
            printerNames = new ObservableCollection<string>(GetPrinterNames());
        }

        List<string> GetPrinterNames()
        {
            List<string> result = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                result.Add(printer);
            }
            return result;
        }
    }
}
