using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemViewWindow.xaml
    /// </summary>
    public partial class CatalogItemViewWindow : Window
    {
        CatalogItemViewModel viewModel;
        public CatalogItemViewWindow(CatalogItemPetsi? item)
        {
            InitializeComponent();
            viewModel = new CatalogItemViewModel(item);
            DataContext = viewModel;
            NaturalNamesListBox.ItemsSource = viewModel.NaturalNames;
            CategoryComboBox.ItemsSource = viewModel.CategoryNames;
            InitCategoryComboBoxSelection();
        }

        private void InitCategoryComboBoxSelection()
        {
            CategoryComboBox.SelectedItem = viewModel.SetSelectedItem();
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //Error Handle form
            Close();
        }

        private void Delete_BtnClk(object sender, RoutedEventArgs e)
        {
            //Are you sure window?
            //delete
            Close();
        }

        private void addNaturalNameButton_Click(object sender, RoutedEventArgs e)
        {
            AddNaturalNameView view = new AddNaturalNameView();
            view.ShowDialog();
            if(view.ControlBool)
            {
                viewModel.AddNaturalName(view.naturalName);
            }
        }

        private void setCutieFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SetCutieFile();
        }

        private void setLabelFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SetStandardLabelFile();
        }

        private void deleteNaturalNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (NaturalNamesListBox.SelectedItem != null)
            {
                bool deleteConfirmation = false;
                ConfirmationWindow confirmationWindow = new ConfirmationWindow();
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    viewModel.RemoveNaturalName((string)NaturalNamesListBox.SelectedItem);
                }
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            viewModel.SetCategory(CategoryComboBox.SelectedItem.ToString());
        }
    }
}
