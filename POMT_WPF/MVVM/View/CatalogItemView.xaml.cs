using Petsi.Events.ItemEvents;
using Petsi.Managers;
using Petsi.Services;
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
        private string originalItemName;
        private bool existingItem;
        public CatalogItemView()
        {
            events = CatalogItemViewEvents.Instance;
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            
            InitializeComponent();

            SaveCheckMark.Visibility = Visibility.Hidden;

            events.ItemNameInvalid += HighlightItemName;
            events.CategoryNameInvalid += HighlightCategoryName;
            events.CategorySizesInvalid += HighlightSizes;
            events.SaveSuccessful += ShowCheckMark;
        }
        private void SetBorderThickness(Border border, int val) { if(border.BorderThickness.Left != val) border.BorderThickness = new Thickness(val, val, val, val); }

        private void HighlightItemName(object sender, EventArgs e) { SetBorderThickness(ItemNameErrBdr, 2); }
        private void HighlightSizes(object sender, EventArgs e) { SetBorderThickness(ItemSizesErrBdr, 2); }
        private void HighlightCategoryName(object sender, EventArgs e) { SetBorderThickness(CategoryNameErrBdr, 2); }
        private void ShowCheckMark(object sender, EventArgs e) { SaveCheckMark.Visibility = Visibility.Visible; }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SetBorderThickness(ItemNameErrBdr, 0);
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SetBorderThickness(CategoryNameErrBdr, 0);
        }

        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            SetBorderThickness(ItemSizesErrBdr, 0);
        }
    }
}
