using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for TemplateListView.xaml
    /// </summary>
    public partial class TemplateListView : UserControl
    {
        public TemplateListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Input Bindings seem to not work so Commands cannot be bound directly in XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
