using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddNaturalNameView.xaml
    /// </summary>
    public partial class AddNaturalNameView : Window
    {
        public bool ControlBool;
        public string naturalName;
        public AddNaturalNameView()
        {
            InitializeComponent();
        }
        private void Accept_ButtonClick(object sender, RoutedEventArgs e)
        {
            /*
            if(naturalNameTextBox.Text != "")
            {
                ControlBool = true;
                Close();
            }*/
        }

        private void Reject_ButtonClick(Object sender, RoutedEventArgs e)
        {
            ControlBool = false;
            Close();
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void naturalNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
           /* naturalName = naturalNameTextBox.Text;*/
        }
    }
}
