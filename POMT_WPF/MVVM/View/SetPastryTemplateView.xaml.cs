using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SetPastryTemplateView.xaml
    /// </summary>
    public partial class SetPastryTemplateView : Window
    {
        SetPastryTemplateViewModel viewModel;
        public string selection;
        public SetPastryTemplateView()
        {
            InitializeComponent();
            viewModel = new SetPastryTemplateViewModel();
            templateListbox.ItemsSource = viewModel.templateNames;
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            //Do something
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            if (templateListbox.SelectedItem != null)
            {
                viewModel.SetPastryTemplate(templateListbox.SelectedItem.ToString());
                selection = (string)templateListbox.SelectedItem;
                Close();
            }
            else
            {
                PetsiOrderFormErrorWindow errorWindow =
                     new PetsiOrderFormErrorWindow("Please select a template.");
                errorWindow.Show();
                return;
            }
        }
    }
}
