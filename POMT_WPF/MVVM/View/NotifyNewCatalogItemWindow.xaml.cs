using Petsi.Events;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyNewCatalogItemWindow.xaml
    /// </summary>
    public partial class NotifyNewCatalogItemWindow : Window
    {
       // NotifyNewCatalogItemViewModel viewModel;
       public string ItemName { get; set; }
        public NotifyNewCatalogItemWindow(string itemName)
        {
            ItemName = itemName;
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
