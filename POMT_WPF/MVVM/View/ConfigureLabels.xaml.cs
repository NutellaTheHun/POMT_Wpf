using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ConfigureLabels.xaml
    /// </summary>
    public partial class ConfigureLabels : Window
    {
        public ConfigureLabels()
        {
            InitializeComponent();
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
