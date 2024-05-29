using System.Collections.ObjectModel;
using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for PetsiOrderWindow.xaml
    /// </summary>
    public partial class PetsiOrderWindow : Window
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();


        public PetsiOrderWindow()
        {
            InitializeComponent();
            DataContext = this;
            Items.Add(new Item("A", "1", "2", "3", "4"));
            Items.Add(new Item());

            //orderFormDataGrid.ItemsSource = Items;
            
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
            Items.Add(new Item());
        }
    }
    public class Item
    {
        public string ItemName { get; set; }
        public string Amount3 { get; set; }
        public string Amount5 { get; set; }
        public string Amount8 { get; set; }
        public string Amount10 { get; set; }

        public Item() { }

        public Item(string itemName, string amount3, string amount5, string amount8, string amount10)
        {
            ItemName = itemName;
            Amount3 = amount3;
            Amount5 = amount5;
            Amount8 = amount8;
            Amount10 = amount10;
        }
    }
}
