using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyCatalogValidateMultiItemView.xaml
    /// </summary>
    public partial class NotifyCatalogValidateMultiItemView : Window
    {
        public ObservableCollection<string> MultiItemListNames = new ObservableCollection<string>();
        public List<CatalogItemPetsi> MultiItemList = new List<CatalogItemPetsi>();
        string ItemContext { get; set; }
        public NotifyCatalogValidateMultiItemView()
        {
            InitializeComponent();
            multiItemListBox.ItemsSource = MultiItemListNames;
        }

        public void UpdateListNames(List<CatalogItemPetsi> overflowList)
        {
            MultiItemList = overflowList;
            foreach (var item in overflowList)
            {
                MultiItemListNames.Add(item.ItemName);
            }
        }

        public void SetItemContext(string itemContext) { ItemContext = itemContext; }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {      
            CatalogItemViewWindow view = new CatalogItemViewWindow(null);
            //Get itemContext to new catalogItem somehow ?
            view.ErrorEventSetItemName(ItemContext);
            view.ShowDialog();

            //update omp with new catalog item
            //ObsOrderModelSingleton.UpdateMultiLineMatchEvent();
            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            omp.UpdateMultiLineMatchEvent();
            Close();
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (multiItemListBox.SelectedItem != null)
            {
                string selectedItemName = (string)multiItemListBox.SelectedItem;
                CatalogItemPetsi matchItem = MultiItemList.FirstOrDefault(x => x.ItemName == selectedItemName);
                matchItem.NaturalNames.Add(ItemContext);

                //ObsCatalogModelSingleton.ModifyItem(matchItem);
                //CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
                //cmp.ModifyItem(matchItem);

                //ObsOrderModelSingleton.UpdateMultiLineMatchEvent();
                OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
                omp.UpdateMultiLineMatchEvent();
                Close();
            }
        }
    }
}
