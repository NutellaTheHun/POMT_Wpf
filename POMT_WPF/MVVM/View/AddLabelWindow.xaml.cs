using Petsi.Units;
using POMT_WPF.MVVM.View.Controls;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddLabelWindow.xaml
    /// </summary>
    public partial class AddLabelWindow : Window
    {
        AddLabelViewModel viewModel;
        bool isNewItem;
        public AddLabelWindow(CatalogItemPetsi? item)
        {
            InitializeComponent();
            viewModel = new AddLabelViewModel(item);
            DataContext = viewModel;
            if (item == null) { isNewItem = true; }
            else { isNewItem = false; }
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            if (viewModel.ValidateFields())
            {
                if (isNewItem && viewModel.HasDuplicate())
                {
                    GeneralErrorWindow errorWindow =
                   new GeneralErrorWindow("Item already has labels assigned, please modify the existing item.");
                    errorWindow.Show();
                    return;
                }
                else
                {
                    viewModel.UpdateCatalogItem();
                    Close();
                } 
            }
            else
            {
                GeneralErrorWindow errorWindow =
                   new GeneralErrorWindow("Item name must be valid and have atleast one label set.");
                errorWindow.Show();
                return;
            }
        }
        private void AddCutieFilepath_ButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.SetCutieFile();
        }
        private void AddPieFilepath_ButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.SetStandardLabelFile();
        }

        private void itemNameComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //#D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;

            if (!viewModel.ValidateItem((string)comboBox.SelectedItem))
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
                itemNameTextBox.Background = brush;
            }
            else
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#CCD7E1");
                itemNameTextBox.Background = brush;
            }
        }

        private void itemNameComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //#D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;
            if (comboBox.SelectedItem != null)
            {
                ItemNameTextBox.Text = comboBox.SelectedItem.ToString();
                //TextFillTextBox idTextBox = grid.FindName("testcatalogObjId") as TextFillTextBox;
                if (viewModel.ValidateItem(ItemNameTextBox.Text))
                {

                }
                else
                {
                    BrushConverter brushConverter = new BrushConverter();
                    Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
                    itemNameTextBox.Background = brush;
                }
            }
        }

        private void ItemNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            bool isValid = viewModel.ValidateItem(ItemNameTextBox.Text);
            if (isValid)
            {

            }
            else
            {
                Grid grid = ItemNameTextBox.Parent as Grid;
                ComboBox itemNameCb = grid.FindName("itemNameComboBox") as ComboBox;
                string itemName = ItemNameTextBox.Text;
                List<CatalogItemPetsi> results = viewModel.GetItemMatchResults(itemName);

                itemNameComboBox.ItemsSource = results.Select(x => x.ItemName);

                if (results.Count != 0)
                {
                    itemNameCb.IsDropDownOpen = true;
                }
            }
        }

        private void deleteStandardLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearStandardLabel();
        }

        private void deleteCutieLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearCutieLabel();
        }
    }
}
