using Petsi.Events;
using Petsi.Units;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyNewCatalogItemView.xaml
    /// </summary>
    public partial class NotifyNewCatalogItemView : Window
    {
        string ItemName { get; set; }
        CatalogItemPetsi Item { get; set; }
        public NotifyNewCatalogItemView(SoiNewItemEventArgs args)
        {
            InitializeComponent();
            Item = args.NewItem;
            ItemName = Item.ItemName;
            DataContext = this;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void viewItemBtn_Click(object sender, RoutedEventArgs e)
        {
            CatalogItemViewWindow view = new CatalogItemViewWindow(Item);
            view.Show();
            Close();
        }
    }
}
