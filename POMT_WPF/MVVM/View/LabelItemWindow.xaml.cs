using Petsi.Events.ItemEvents;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelItemWindow.xaml
    /// </summary>
    public partial class LabelItemWindow : Window
    {
        private LabelItemViewModel vm;
        private CatalogService cs;
        private List<CatalogItemPetsi> labeledItems;
        public LabelItemWindow(CatalogItemPetsi? item, List<CatalogItemPetsi> itemsWithLabels)
        {
            vm = new LabelItemViewModel(item, this, itemsWithLabels);
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            labeledItems = itemsWithLabels;
            DataContext = vm;
            InitializeComponent();

            SaveCheckMark.Visibility = Visibility.Hidden;

            if (item != null)
            {
                ItemNameTextBox.IsReadOnly = true;
            }

            LabelItemViewEvents.Instance.ItemNameInvalid += HighlightItemName;
            LabelItemViewEvents.Instance.SaveSuccess += ShowSaveCheckMark;
        }
        private void ItemNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox itemNameTextBox = sender as TextBox;

            if (itemNameTextBox.Text != "")
            {
                ComboBox itemNameCb = (itemNameTextBox.Parent as Grid).FindName("ItemNameComboBox") as ComboBox;

                List<CatalogItemPetsi> results = cs.GetItemNameValidationResults(itemNameTextBox.Text);
                if (results != null || results.Count != 0)
                {
                    List<CatalogItemPetsi> copy = new List<CatalogItemPetsi>(results);
                    foreach (CatalogItemPetsi item in copy)
                    {
                        //We dont want items with a label path to show in drop box, no duplicates
                        foreach (CatalogItemPetsi labeledItem in labeledItems)
                        {
                            if (labeledItem.CatalogObjectId == item.CatalogObjectId) { results.Remove(item); break; }
                        }
                    }
                }

                itemNameCb.ItemsSource = results.Select(x => x.ItemName);
                if (results.Count != 0)
                {
                    itemNameCb.IsDropDownOpen = true;
                }
            }
        }

        private void SetBorderThickness(Border border, int val) { if (border.BorderThickness.Left != val) border.BorderThickness = new Thickness(val, val, val, val); }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SetBorderThickness(ItemNameErrBdr, 0);
        }

        private void HighlightItemName(object sender, EventArgs e) { SetBorderThickness(ItemNameErrBdr, 2); }
        private void ShowSaveCheckMark(object sender, EventArgs e) { SaveCheckMark.Visibility = Visibility.Visible; }

    }
}
