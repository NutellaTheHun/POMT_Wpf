using Petsi.Events;
using Petsi.Services;
using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            ErrorService.Instance().SoiNewItem += NotifyUserNewItem;
            ErrorService.Instance().SoiMultiItem += NotifyUserMultiItemMatch;
            ErrorService.RaiseMainWindowEvents();
        }

        public void NotifyUserNewItem(object sender, EventArgs e)
        {
            NotifyNewCatalogItemView view = new NotifyNewCatalogItemView((SoiNewItemEventArgs)e);
            view.Show();
        }

        public void NotifyUserMultiItemMatch(object sender, EventArgs e)
        {
            NotifyMultiItemMatchWindow view = new NotifyMultiItemMatchWindow((SoiMultiItemEventArgs)e);
            view.Show();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ViewModel.FilterSearchBar(txtFilter.Text);
            //dashboardDataGrid.ItemsSource = ViewModel._orders;
        }
    }
}

