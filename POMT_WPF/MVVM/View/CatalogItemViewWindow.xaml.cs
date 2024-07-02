using Petsi.Units;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Media;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemViewWindow.xaml
    /// </summary>
    public partial class CatalogItemViewWindow : Window
    {
        CatalogItemViewModel viewModel;
        bool isEditable;
        bool needsValidation;
        bool isNew;
        public CatalogItemViewWindow(CatalogItemPetsi? item)
        {
            InitializeComponent();

            if (item == null) 
            { 
                isEditable = true;
                AllowEditing(isEditable);
                needsValidation = true;
                editToggleButton.Visibility = Visibility.Hidden;
            }
            else
            {
                isEditable = false;
                AllowEditing(isEditable);
            }
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
            if (needsValidation)
            {
                if (viewModel.ValidateCatalogItem())
                {
                    viewModel.UpdateItem();
                    Close();
                }
                else
                {
                    GeneralErrorWindow errorWindow = new GeneralErrorWindow("Invalid catalog item entry.");
                    errorWindow.Show();
                }
            }
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
            if (view.ControlBool)
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

        #region checkEvents

        private void regularCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //viewModel.UpdateSizeSetting(Identifiers.SIZE_REGULAR, true);
        }

        private void cutieCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //viewModel.UpdateSizeSetting(Identifiers.SIZE_CUTIE, true);
        }

        private void largeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
           // viewModel.UpdateSizeSetting(Identifiers.SIZE_LARGE, true);
        }

        private void mediumCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //viewModel.UpdateSizeSetting(Identifiers.SIZE_MEDIUM, true);
        }

        private void smallCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //viewModel.UpdateSizeSetting(Identifiers.SIZE_SMALL, true);
        }

        private void regularCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
           // viewModel.UpdateSizeSetting(Identifiers.SIZE_REGULAR, false);
        }

        private void cutieCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //viewModel.UpdateSizeSetting(Identifiers.SIZE_CUTIE, false);
        }

        private void largeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
           // viewModel.UpdateSizeSetting(Identifiers.SIZE_LARGE, false);
        }

        private void mediumCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
          //  viewModel.UpdateSizeSetting(Identifiers.SIZE_MEDIUM, false);
        }

        private void smallCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
           // viewModel.UpdateSizeSetting(Identifiers.SIZE_SMALL, false);
        }

        private void potm_checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            viewModel.SetIsPOTM(false);
        }

        private void potm_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.SetIsPOTM(true);
        }

        #endregion

        private void veganMapping_Click(object sender, RoutedEventArgs e)
        {
            CatalogItemVeganMapView veganMapView = new CatalogItemVeganMapView();
            veganMapView.ShowDialog();
            if (veganMapView.selection != null)
            {
                viewModel.SetVeganPieAssociation((CatalogItemPetsi)veganMapView.selection);
            }
        }

        private void TextFillTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //# D64933 chili red
            //NEED FIX will return false if pre-existing name is changed and returned to original
            if (viewModel.ValidateCatalogName(ItemNameTextBox.Text))
            {
                viewModel.SetItemName(ItemNameTextBox.Text);
            }
            else
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
                ItemNameTextBox.Background = brush;
            }
        }

        private void editToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.IsReadOnly = (viewModel.IsReadOnly == false);

            isEditable = (isEditable == false);

            if (!needsValidation) { if (isEditable) needsValidation = true; }

            AllowEditing(isEditable);
        }

        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            
            ConfirmationWindow confirmationWindow = new ConfirmationWindow();
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                ObsCatalogModelSingleton.RemoveItem(viewModel.cItem);
                Close();
            }
        }
        private void AllowEditing(bool b)
        {
            ItemNameTextBox.IsHitTestVisible = b;
            CategoryComboBox.IsHitTestVisible = b;
            smallCheckBox.IsHitTestVisible = b;
            mediumCheckBox.IsHitTestVisible = b;
            largeCheckBox.IsHitTestVisible = b;
            regularCheckBox.IsHitTestVisible = b;
            cutieCheckBox.IsHitTestVisible = b;
            potm_checkBox.IsHitTestVisible = b;
            addNaturalNameButton.IsHitTestVisible = b;
            deleteNaturalNameButton.IsHitTestVisible = b;
            setLabelFile.IsHitTestVisible = b;
            setCutieFile.IsHitTestVisible = b;
            veganMapping.IsHitTestVisible = b;
        }

        public void ErrorEventSetItemName(string itemContext) { viewModel.ItemName = itemContext; }
    }
}
