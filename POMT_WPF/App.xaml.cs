using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Square.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POMT_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SquareClientFactory scf = new SquareClientFactory();

            OrderModelPetsi omp = new OrderModelPetsi();
            CatalogModelPetsi cmp = new CatalogModelPetsi();

            CategoryService categoryService = new CategoryService();
            CatalogService catalogIdService = new CatalogService();
            LabelService labelService = new LabelService();

            ReportDirector director = new ReportDirector();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            SquareOrderInput soi = new SquareOrderInput(scf);
            if(!scf.BuildFailed)
            {
                sci.Execute().Wait();
                soi.Execute().Wait();
            }
            else
            {
                ErrorService.Instance().RaiseSquareKeyMissing();
            }
        }

        //https://stackoverflow.com/questions/53500915/how-to-select-all-text-in-textbox-wpf-when-focused
        private void TextBox_GotKeyboardFocus(Object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Dispatcher.BeginInvoke(new Action(() => tb.SelectAll()));
        }
        
    }

}
