using Petsi.Events.ItemEvents;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemView.xaml
    /// </summary>
    public partial class CatalogItemView : UserControl
    {
        CatalogItemViewEvents events;
        CatalogService cs;
        public CatalogItemView()
        {
            events = CatalogItemViewEvents.Instance;
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            
            InitializeComponent();

            events.ItemNameInvalid += HighlightItemName;
        }
        private void SetBorderThickness(Border border, int val) { border.BorderThickness = new Thickness(val, val, val, val); }

        private void HighlightItemName(object sender, EventArgs e) { SetBorderThickness(ItemNameErrBdr, 2); }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SetBorderThickness(ItemNameErrBdr, 0);
        }
        private void ItemNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox itemNameTextBox = sender as TextBox;

            if (itemNameTextBox.Text != "")
            {
                ComboBox itemNameCb = (itemNameTextBox.Parent as Grid).FindName("ItemNameComboBox") as ComboBox;

                List<CatalogItemPetsi> results = cs.GetItemNameValidationResults(itemNameTextBox.Text);
                itemNameCb.ItemsSource = results.Select(x => x.ItemName);
                if (results.Count != 0)
                {
                    itemNameCb.IsDropDownOpen = true;
                }
            }
        }
    }
}
