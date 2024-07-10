using Petsi.Utils;
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
        string StartupPath {  get; set; }
        string SquareConfigFp = System.AppDomain.CurrentDomain.BaseDirectory + "/petsiDir/squareConfig.txt";
        public SquareKeyMissingWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text != "" && StartupPath != "")
            {
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(textBox.Text);
                File.WriteAllText(SquareConfigFp, sb.ToString());

                PetsiConfig.GetInstance().SetVariable(Identifiers.SETTING_STARTUP, StartupPath);

                System.Windows.Application.Current.Shutdown();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Select startup location";
            string sSelectedPath = "";
          
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sSelectedPath = fbd.SelectedPath;
            }
            if (sSelectedPath != "")
            {

                StartupPath = sSelectedPath;
            }
        }
    }
}
