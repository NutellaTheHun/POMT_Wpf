using System.ComponentModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddNaturalNameWindow.xaml
    /// </summary>
    public partial class AddNaturalNameWindow : Window
    {
        public bool ControlBool;

        private string _alternativeName;
        public string AlternativeName
        {
            get
            {
                return _alternativeName;
            }
            set 
            { 
                if (_alternativeName != value)
                {
                    _alternativeName = value; 
                    OnPropertyChanged(nameof(AlternativeName));
                } 
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddNaturalNameWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ControlBool = false;
            Close();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if(AlternativeName != null && AlternativeName != "") { ControlBool = true; }
            Close();
        }
    }
}
