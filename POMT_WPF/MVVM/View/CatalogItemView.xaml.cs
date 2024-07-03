using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemView.xaml
    /// </summary>
    public partial class CatalogItemView : UserControl
    {
        public CatalogItemView()
        {
            InitializeComponent();
        }
        private void SetBorderThickness(Border border, int val) { border.BorderThickness = new Thickness(val, val, val, val); }

        private void HighlightItemName(object sender, EventArgs e) { SetBorderThickness(ItemNameErrBdr, 2); }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SetBorderThickness(ItemNameErrBdr, 0);
        }
    }
}
