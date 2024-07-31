using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for GeneralErrorWindow.xaml
    /// </summary>
    public partial class GeneralErrorWindow : Window
    {
        public string ErrorMessage { get; set; }
        public GeneralErrorWindow(string message)
        {
            InitializeComponent();
            DataContext = this;
            ErrorMessage = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
