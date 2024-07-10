using System.IO;
using System.Text;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SquareKeyMissingWindow.xaml
    /// </summary>
    public partial class SquareKeyMissingWindow : Window
    {
        string Key {  get; set; }
        string configFp = System.AppDomain.CurrentDomain.BaseDirectory + "/petsiDir/squareConfig.txt";
        public SquareKeyMissingWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(textBox.Text);
                File.WriteAllText(configFp, sb.ToString());
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
