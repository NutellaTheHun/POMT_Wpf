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
    /// Interaction logic for TemplateItemViewWindow.xaml
    /// </summary>
    public partial class TemplateItemViewWindow : Window
    {
        CatalogService cs;
            
        public TemplateItemViewWindow(List<BackListItem>? templateItemList, string? templateName)
        {
            TemplateItemViewModel viewModel = new TemplateItemViewModel(templateItemList, templateName, this);
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            InitializeComponent();
            DataContext = viewModel;
            SaveCheckMark.Visibility = Visibility.Hidden;

            TemplateItemViewEvents.Instance.SaveSuccessful += ShowSaveCheckMark;
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

        private void ShowSaveCheckMark(object sender, EventArgs e)
        {
            SaveCheckMark.Visibility =Visibility.Visible;
        }
    }
}
