
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class SetStandardPrinterViewModel : ViewModelBase
    {
        public ObservableCollection<string> printers = new ObservableCollection<string>();

        public SetStandardPrinterViewModel()
        {
            printers = new ObservableCollection<string>(GetPrinterNames());
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
