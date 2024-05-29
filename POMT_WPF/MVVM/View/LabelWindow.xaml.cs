using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
           
        }
        private void Small_ButtonClick(Object sender, RoutedEventArgs e)
        {
           
        }
        private void Round_ButtonClick(Object sender, RoutedEventArgs e)
        {
          
        }
    }
}
