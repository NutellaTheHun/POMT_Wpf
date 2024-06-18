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
            viewModel = new ConfigureLabelsViewModel();
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
            AddLabelWindow addLabelWindow = new AddLabelWindow();
            addLabelWindow.Show();
        }
        private void RemLabelWindow_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
