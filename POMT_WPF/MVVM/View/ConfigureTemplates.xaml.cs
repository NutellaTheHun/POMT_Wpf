using POMT_WPF.MVVM.ViewModel;
using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ConfigureTemplates.xaml
    /// </summary>
    public partial class ConfigureTemplates : Window
    {
        ConfigureTemplateViewModel viewModel;

        public ConfigureTemplates()
        {
            InitializeComponent();
            viewModel = new ConfigureTemplateViewModel();
            templateListbox.ItemsSource = viewModel.templateNames;
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //Do something
            Close();
        }
        private void AddTemplate_BtnClk(object sender, RoutedEventArgs e)
        {
            
            TemplateViewWindow tvw = new TemplateViewWindow(null);
            tvw.ShowDialog();
        }
        private void RemTemplate_BtnClk(object sender, RoutedEventArgs e)
        {
            if (templateListbox.SelectedItem != null)
            {
                bool deleteConfirmation = false;
                ConfirmationWindow confirmationWindow = new ConfirmationWindow();
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    viewModel.RemoveTemplate((string)templateListbox.SelectedItem);
                }
            }
        }

        private void templateListbox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string selectedItem = (string)templateListbox.SelectedItem;
            TemplateViewWindow tvw = new TemplateViewWindow(selectedItem);
            tvw.Show();
        }
    }
}
