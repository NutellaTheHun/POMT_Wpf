using Petsi.Services;
using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for TemplateViewWindow.xaml
    /// </summary>
    public partial class TemplateViewWindow : Window
    {
        public TemplateViewModel tvm;

        public TemplateViewWindow(string? inputTemplateName)
        {
            InitializeComponent();
            
            tvm = new TemplateViewModel(inputTemplateName);
            DataContext = this;
            templateViewDataGrid.ItemsSource = tvm.TemplateItems;
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //validation
            //save
            Close();
        }
        private void AddLineItem_BtnClk(object sender, RoutedEventArgs e)
        {
            tvm.Add(new BackListItem());
        }
    }
}
