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

        /// <summary>
        /// WARNING This filepath is hardcoded For PetsiConfig and The SquareClientFactory.cs
        /// </summary>
        string SquareConfigFp = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\squareConfig.txt";
        public SquareKeyMissingWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if(SquareTextBox.Text != "" && StartupTextBox.Text != "")
            {
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(SquareTextBox.Text);
                File.WriteAllText(SquareConfigFp, sb.ToString());

                PetsiConfig.GetInstance().SetVariable(Identifiers.SETTING_STARTUP, StartupTextBox.Text);
                PetsiConfig.GetInstance().SetVariable(Identifiers.SETTING_STARTUP_STATUS, Identifiers.SETTING_STARTUP_STATUS_PENDING);
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
                StartupTextBox.Text = sSelectedPath;
            }
        }
    }
}
