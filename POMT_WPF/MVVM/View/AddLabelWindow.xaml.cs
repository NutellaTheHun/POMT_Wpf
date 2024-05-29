using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddLabelWindow.xaml
    /// </summary>
    public partial class AddLabelWindow : Window
    {
        public AddLabelWindow()
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
        private void AddCutieFilepath_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void AddPieFilepath_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
