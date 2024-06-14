using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        SettingsWindowViewModel viewModel;
        public string selection;
        public SettingsWindow()
        {
            InitializeComponent();
            viewModel = new SettingsWindowViewModel();
            DataContext = viewModel;
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
        private void ConfigureLabelsWin_BtnClk(object sender, RoutedEventArgs e)
        {
            ConfigureLabels ConfigureLabelsWin = new ConfigureLabels();
            ConfigureLabelsWin.Show();
            //Do something

        }
        private void ConfigureTemplatesWin_BtnClk(object sender, RoutedEventArgs e)
        {
            ConfigureTemplates ConfigureTemplatesWin = new ConfigureTemplates();
            ConfigureTemplatesWin.Show();
        }
        private void ManageCatalogWin_BtnClk(object sender, RoutedEventArgs e)
        {
            CatalogListViewWindow clvw = new CatalogListViewWindow();
            clvw.Show();
        }
        private void SetLabelPrinter_BtnClk(object sender, RoutedEventArgs e)
        {
            SetLabelPrinterView view = new SetLabelPrinterView();
            view.ShowDialog();
            if (view.selection != null)
            {
                viewModel.SetLabelPrinter(view.selection);
            }
        }
        private void SetStandardPrinter_BtnClk(object sender, RoutedEventArgs e)
        {
            SetStandardPrinterView view = new SetStandardPrinterView();
            view.ShowDialog();
            if (view.selection != null)
            {
                viewModel.SetStandardPrinter(view.selection);
            }
        }
        private void SetLabelsFilePath_BtnClk(object sender, RoutedEventArgs e)
        {
            viewModel.SetLabelsFilePath();
        }
        private void SetPieTemplate_BtnClk(object sender, RoutedEventArgs e)
        {
            SetPieTemplateView view = new SetPieTemplateView();
            view.ShowDialog();
            if(view.selection != null)
            {
                viewModel.SetPieTemplate(view.selection);
            }  
        }
        private void SetPastryTemplate_BtnClk(object sender, RoutedEventArgs e)
        {
            SetPastryTemplateView view = new SetPastryTemplateView();
            view.ShowDialog();
            if (view.selection != null)
            {
                viewModel.SetPastryTemplate(view.selection);
            }
        }
    }
}
