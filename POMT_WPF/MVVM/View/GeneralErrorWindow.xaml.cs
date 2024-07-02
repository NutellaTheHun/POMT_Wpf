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
    }
}
