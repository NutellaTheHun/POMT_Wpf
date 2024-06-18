using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelWindow.xaml
    /// </summary>
    public partial class LabelWindow : Window
    {
        DateTime dt1;
        DateTime dt2;
        bool standardLabel;
        bool smallLabel;
        bool roundLabel;
        public LabelWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Print_ButtonClick(Object sender, RoutedEventArgs e)
        {

        }
        private void Standard_ButtonClick(Object sender, RoutedEventArgs e)
        {
            LabelService ls = (LabelService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_LABEL);
            ls.PrintStandard(null);
        }
        private void Small_ButtonClick(Object sender, RoutedEventArgs e)
        {
           
        }
        private void Round_ButtonClick(Object sender, RoutedEventArgs e)
        {
          
        }
    }
}
