using System.Windows;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ExceptionHandlerShowWindow.xaml
    /// </summary>
    public partial class ExceptionHandlerShowWindow : Window
    {
        public string ExceptionMessage { get; set; }
        public ExceptionHandlerShowWindow(string message)
        {
            InitializeComponent();
            ExceptionMessage = message;
            DataContext = this;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
