using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for TemplateItemViewWindow.xaml
    /// </summary>
    public partial class TemplateItemViewWindow : Window
    {
        public TemplateItemViewWindow(List<BackListItem>? templateItemList, string? templateName)
        {
            TemplateItemViewModel viewModel = new TemplateItemViewModel(templateItemList, templateName, this);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
