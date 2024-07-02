using Petsi.Events;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyMultiItemMatchWindow.xaml
    /// </summary>
    public partial class NotifyMultiItemMatchWindow : Window
    {
        private NotifyItemMultiMatchViewModel viewModel;

        //public ObservableCollection<string> MultiItemListNames = new ObservableCollection<string>();
        //public List<CatalogItemPetsi> MultiItemList = new List<CatalogItemPetsi>();
        //string ItemContext { get; set; }
        public NotifyMultiItemMatchWindow(SoiMultiItemEventArgs args)
        {
            viewModel = new NotifyItemMultiMatchViewModel(args, this);
            InitializeComponent();
            DataContext = viewModel;
        }

        /*
        public void UpdateListNames(List<CatalogItemPetsi> overflowList)
        {
            MultiItemList = overflowList;
            foreach (var item in overflowList)
            {
                MultiItemListNames.Add(item.ItemName);
            }
        }
        */
    }
}
