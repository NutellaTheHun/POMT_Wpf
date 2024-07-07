using Petsi.Events.ItemEvents;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderItemView.xaml
    /// </summary>
    public partial class OrderItemView : UserControl
    {
        OrderItemViewEvents events;
        CatalogService cs;
        public OrderItemView()
        {
            events = OrderItemViewEvents.Instance;
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            InitializeComponent();

            events.RecipientInvalid += HighlightRecipient;
            events.FulfillmentInvalid += HighlightFulfillment;
            events.TimeInvalid += HighlightTime;
            events.FrequencyInvalid += HighlightFrequency;
            events.PhoneInvalid += HighlightPhone;
            events.DatePickerLessThanInvalid += HighlightDatePicker;
            events.DatePickerInvalid += HighlightDatePicker; 
            events.DelAddressInvalid += HighlightDelAddress;
            events.OrderTypeInvalid += HighlightOrderType;
            events.LineItemsInvalid += HighlightLineItems;

            events.SaveSuccess += ShowCheckMark;

            OrderDatePicker.SelectedDate = null;
            SaveCheckMark.Visibility = Visibility.Hidden;
        }
        private void ItemNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            TextBox itemNameTextBox = sender as TextBox;

            if (itemNameTextBox.Text != "")
            {
                ComboBox itemNameCb = (itemNameTextBox.Parent as Grid).FindName("ItemNameComboBox") as ComboBox;

                List<CatalogItemPetsi> results = cs.GetItemNameValidationResults(itemNameTextBox.Text);
                itemNameCb.ItemsSource = results.Select(x => x.ItemName);
                if (results.Count != 0)
                {
                    itemNameCb.IsDropDownOpen = true;
                }
            }
        }
        private void HighlightRecipient(object sender, EventArgs e) { SetBorderThickness(RecipientErrBdr, 2); }
        private void HighlightFulfillment(object sender, EventArgs e) { SetBorderThickness(FulfillmentErrBdr, 2); }
        private void HighlightOrderType(object sender, EventArgs e) { SetBorderThickness(OrderTypeErrBdr, 2); }
        private void HighlightFrequency(object sender, EventArgs e) { SetBorderThickness(FrequencyErrBdr, 2); }
        private void HighlightLineItems(object sender, EventArgs e) { SetBorderThickness(LineItemsErrBdr, 2); }
        private void HighlightDelAddress(object sender, EventArgs e) { SetBorderThickness(DelAddrErrBdr, 2); }
        private void HighlightPhone(object sender, EventArgs e) { SetBorderThickness(PhoneErrBdr, 2); }
        private void HighlightDatePicker(object sender, EventArgs e) { SetBorderThickness(DatePickerErrBdr, 2); }
        private void HighlightTime(object sender, EventArgs e) { SetBorderThickness(TimeErrBdr, 2); }
        private void ShowCheckMark(object sender, EventArgs e) { SaveCheckMark.Visibility = Visibility.Visible; }

        private void SetBorderThickness(Border border, int val) { if(border.BorderThickness.Left != val) border.BorderThickness = new Thickness(val, val, val, val); }

        private void Recipient_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(RecipientErrBdr, 0); }

        private void Phone_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(PhoneErrBdr, 0);}

        private void OrderDatePicker_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(DatePickerErrBdr, 0); }

        private void Time_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(TimeErrBdr, 0); }

        private void OrderTypeComboBox_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(OrderTypeErrBdr, 0); }

        private void FulfillmentTypeComboBox_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(FulfillmentErrBdr, 0); }

        private void OrderFrequencyTypeComboBox_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(FrequencyErrBdr, 0); }

        private void orderFormDataGrid_GotFocus(object sender, RoutedEventArgs e){ SetBorderThickness(LineItemsErrBdr, 0); }
    }
}
