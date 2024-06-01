using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for PetsiOrderFormErrorWindow.xaml
    /// </summary>
    public partial class PetsiOrderFormErrorWindow : Window
    {
        public string ErrorMessage { get; set; }
        public PetsiOrderFormErrorWindow(string message)
        {
            InitializeComponent();
            DataContext = this;
            ErrorMessage = message;
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
