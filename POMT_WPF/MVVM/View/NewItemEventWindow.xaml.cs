using Petsi.Events;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NewItemEventWindow.xaml
    /// </summary>
    public partial class NewItemEventWindow : Window
    {
        NewItemEventWindowViewModel viewModel;
        public NewItemEventWindow(SoiNewItemEventArgs args)
        {
            viewModel = new NewItemEventWindowViewModel(args, this);
            InitializeComponent();
            DataContext = viewModel;
            NotifyNewCatalogItemWindow win = new NotifyNewCatalogItemWindow(args.NewItem.ItemName);
            win.Owner = Application.Current.MainWindow;
            win.Show();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FilterSearchBar(SearchTextBox.Text);
            catalogListDataGrid.ItemsSource = viewModel.Items;
        }
    }
}
