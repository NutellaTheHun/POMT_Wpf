using System.Collections.ObjectModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for TemplateViewWindow.xaml
    /// </summary>
    public partial class TemplateViewWindow : Window
    {
        public ObservableCollection<TemplateItem> TemplateItems { get; set; } = new ObservableCollection<TemplateItem>();
        public TemplateViewWindow()
        {
            InitializeComponent();
            DataContext = this;
            TemplateItems.Add(new TemplateItem("sadasas", "ssss"));
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
        private void AddLineItem_BtnClk(object sender, RoutedEventArgs e)
        {
            TemplateItems.Add(new TemplateItem());
        }
    }

    public class TemplateItem
    {
        public string ItemName { get; set; }
        public string Id { get; set; }
        public TemplateItem() { }
        public TemplateItem(string itemName, string id) { ItemName = itemName; Id = id; }   
    }
}
