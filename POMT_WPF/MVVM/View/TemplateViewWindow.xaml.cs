using Petsi.Units;
using POMT_WPF.MVVM.View.Controls;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for TemplateViewWindow.xaml
    /// </summary>
    public partial class TemplateViewWindow : Window
    {
        public TemplateViewModel viewModel { get; set; }
        bool isPastryTemplate;

        public TemplateViewWindow(string? inputTemplateName)
        {
            InitializeComponent();

            viewModel = new TemplateViewModel(inputTemplateName);
            //templateNameTextBox.Text = viewModel.TemplateName;
            DataContext = this;
            templateViewDataGrid.ItemsSource = viewModel.TemplateItems;
            isPastryTemplate = true;
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            if (viewModel.IsValidTemplate())
            {
                viewModel.SaveTemplate();
                Close();
            }
            else
            {
                PetsiOrderFormErrorWindow errorWindow =
                   new PetsiOrderFormErrorWindow("Template must have a name,\n must have 1 item,\n and all items must have a validated item name");
                errorWindow.Show();
                return;
            }

        }
        private void AddLineItem_BtnClk(object sender, RoutedEventArgs e)
        {
            viewModel.Add(new BackListItem());
        }

        private void PiePastryToggle_Click(object sender, RoutedEventArgs e)
        {
            isPastryTemplate = (isPastryTemplate == false);
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (templateViewDataGrid.SelectedItem != null)
            {
                bool deleteConfirmation = false;
                ConfirmationWindow confirmationWindow = new ConfirmationWindow();
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    viewModel.TemplateItems.Remove((BackListItem)templateViewDataGrid.SelectedItem);
                }
            }
        }

        private void ItemNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            //# D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;
            if (!viewModel.IsValidItemName(itemNameTextBox.Text))
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

        private void itemNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //#D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;
            if (comboBox.SelectedItem != null)
            {
                itemNameTextBox.Text = comboBox.SelectedItem.ToString();
                TextFillTextBox idTextBox = grid.FindName("testcatalogObjId") as TextFillTextBox;
                if (viewModel.ValidateItemName(itemNameTextBox.Text))
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

        private void ItemNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextFillTextBox itemNameTextBox = (TextFillTextBox)sender;

            if (!viewModel.ValidateItemName(itemNameTextBox.Text))
            {
                Grid grid = itemNameTextBox.Parent as Grid;
                ComboBox itemNameCb = grid.FindName("itemNameComboBox") as ComboBox;
                string itemName = itemNameTextBox.Text;
                List<CatalogItemPetsi> results = viewModel.GetItemMatchResults(itemName);

                itemNameCb.ItemsSource = results.Select(x => x.ItemName);

                if (results.Count != 0)
                {
                    itemNameCb.IsDropDownOpen = true;
                }

            }
        }

        private void pageDisplayNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextFillTextBox pageDisplayNameTextBox = (TextFillTextBox)sender;

            if (templateViewDataGrid.SelectedItem != null)
            {
                viewModel.SetPageDisplayName((BackListItem)templateViewDataGrid.SelectedItem, pageDisplayNameTextBox.Text);
            }
        }
    }
}
