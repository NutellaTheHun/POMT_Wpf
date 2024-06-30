using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelItemWindow.xaml
    /// </summary>
    public partial class LabelItemWindow : Window
    {
        LabelItemViewModel vm;
        public LabelItemWindow(CatalogItemPetsi? item)
        {
            vm = new LabelItemViewModel(item, this);
            DataContext = vm;
            InitializeComponent();
        }
        public void CloseWin() { Close(); }
    }
}
