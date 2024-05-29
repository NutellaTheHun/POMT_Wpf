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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        DateTime dt1;
        DateTime dt2;
        bool frontList;
        bool backList;
        bool wholesaleAgg;
        bool wholesale;
        public ReportWindow()
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

        private void selectFrontList_ButtonClick(Object sender, RoutedEventArgs e)
        {

        }
        private void selectBackList_ButtonClick(Object sender, RoutedEventArgs e)
        {

        }
        private void selectWsAggList_ButtonClick(Object sender, RoutedEventArgs e)
        {

        }
        private void selectWsList_ButtonClick(Object sender, RoutedEventArgs e)
        {

        }
    }
}
