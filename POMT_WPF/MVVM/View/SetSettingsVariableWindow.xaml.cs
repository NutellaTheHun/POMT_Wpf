using System.Windows;
namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SetSettingsVariableWindow.xaml
    /// </summary>
    public partial class SetSettingsVariableWindow : Window
    {
        public string VariableSelection { get; set; }
        public bool selectionMade;
        public SetSettingsVariableWindow(string title,List<string> variableNames)
        {
            InitializeComponent();    
            VariableListBox.ItemsSource = variableNames;
            TitleLabel.Content = title;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            if(VariableListBox.SelectedItem != null)
            {
                VariableSelection = (string)VariableListBox.SelectedItem;
                selectionMade = true;
                Close();
            }
        }
    }
}
