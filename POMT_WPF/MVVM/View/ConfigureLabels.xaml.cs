using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ConfigureLabels.xaml
    /// </summary>
    public partial class ConfigureLabels : Window
    {
        ConfigureLabelsViewModel viewModel;
        public ConfigureLabels()
        {
            InitializeComponent();
            viewModel = new ConfigureLabelsViewModel(false);
            labelDataGrid.ItemsSource = viewModel.Items;
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //Do something
            Close();
        }
        private void AddLabelWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            AddLabelWindow addLabelWindow = new AddLabelWindow(null);
            addLabelWindow.ShowDialog();
            viewModel.UpdateLabelList();
        }
        private void RemLabelWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (labelDataGrid.SelectedItem != null)
            {
                viewModel.RemoveItem(labelDataGrid.SelectedItem);
                viewModel.UpdateLabelList();
            }
        }

        private void labelDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (labelDataGrid.SelectedItem != null)
            {
                AddLabelWindow addLabelWindow = new AddLabelWindow((CatalogItemPetsi)labelDataGrid.SelectedItem);
                addLabelWindow.ShowDialog();
                viewModel.UpdateLabelList();
            }
        }
    }
}
