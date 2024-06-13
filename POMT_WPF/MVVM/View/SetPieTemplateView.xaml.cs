using POMT_WPF.MVVM.ViewModel;
using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SetBackListTemplateView.xaml
    /// </summary>
    public partial class SetPieTemplateView : Window
    {
        SetPieTemplateViewModel viewModel;
        public string selection;
        public SetPieTemplateView()
        {
            InitializeComponent();
            viewModel = new SetPieTemplateViewModel();
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
                viewModel.SetPieTemplate(templateListbox.SelectedItem.ToString());
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
