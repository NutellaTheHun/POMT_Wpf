using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Petsi.Events.ItemEvents;
using POMT_WPF.Core;
using System.Collections.ObjectModel;
using POMT_WPF.MVVM.View;
using POMT_WPF.MVVM.ObsModels;

namespace POMT_WPF.MVVM.ViewModel
{
    public class OrderItemViewModel : ViewModelBase
    {
        private PetsiOrder Order;

        #region Properties

        /// <summary>
        /// Controls the type of fulfillment date control in the view. If orderType is wholesale => DOTW combo box, else -> datepicker
        /// </summary>
        private bool _isWeekly;
        public bool IsWeekly 
        { 
            get { return _isWeekly;}
            set 
            { 
                if(_isWeekly != value)
                {
                    _isWeekly = value;
                    IsNotWeekly = !IsWeekly;
                    OnPropertyChanged(nameof(IsWeekly));
                }
            }
        }
        /// <summary>
        /// Inverse binding of IsWholesale, View binding uses boolToVis converter, simpler than making a converter that combines boolToVis+invertBool
        /// </summary>
        private bool _isNotWeekly;
        public bool IsNotWeekly
        {
            get { return _isNotWeekly; }
            set
            {
                if (_isNotWeekly != value)
                {
                    _isNotWeekly = value;
                    OnPropertyChanged(nameof(IsNotWeekly));
                }
            }
        }

        public string Recipient 
        {
            get { return Order.Recipient; }
            set
            {
                if (value != Order.Recipient)
                {
                    Order.Recipient = value;
                    OnPropertyChanged(nameof(Recipient));
                }
            }
        }
        public string Email
        {
            get { return Order.Email; }
            set
            {
                if (value != Order.Email)
                {
                    Order.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string PhoneNumber
        {
            get { return Order.PhoneNumber; }
            set
            {
                if (value != Order.PhoneNumber)
                {
                    Order.PhoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
       
        public string DeliveryAddr
        {
            get { return Order.DeliveryAddress;  }
            set
            {
                if (value != Order.DeliveryAddress)
                {
                    Order.DeliveryAddress = value;
                    OnPropertyChanged(nameof(DeliveryAddr));
                }
            }
        }

        public string Notes
        {
            get { return Order.Note; }
            set
            {
                if (value != Order.Note)
                {
                    Order.Note = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }

        public string OrderType
        {
            get { return Order.OrderType;  }
            set
            {
                if (value != Order.OrderType)
                {
                    Order.OrderType = value;   
                    OnPropertyChanged(nameof(OrderType));
                }
            }
        }

        public string FulfillmentType
        {
            get { return Order.FulfillmentType;}
            set
            {
                if (value != Order.FulfillmentType)
                {
                    Order.FulfillmentType = value;
                    OnPropertyChanged(nameof(FulfillmentType));
                }
            }
        }

        private string _orderFrequency;
        public string OrderFrequency
        {
            get { if (_orderFrequency != null) return _orderFrequency; return ""; }
            set
            {
                if (value != _orderFrequency)
                {
                    _orderFrequency = value;
                    if (OrderFrequency == Identifiers.ORDER_FREQUENCY_WEEKLY) { Order.IsPeriodic = true; Order.IsOneShot = false; IsWeekly = true; }
                    else if (OrderFrequency == Identifiers.ORDER_FREQUENCY_ONE_TIME) { Order.IsOneShot = true; Order.IsPeriodic = false; IsWeekly = false; }
                    OnPropertyChanged(nameof(OrderFrequency));
                }
            }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        private DateTime? _fulfillmentDate;
        public DateTime? FulfillmentDate
        {
            get { if (_fulfillmentDate != null) return _fulfillmentDate; return default; }
            set
            {
                if (value != _fulfillmentDate)
                {
                    _fulfillmentDate = value;
                    UpdateFulfillmentDayOfWeek(FulfillmentDayOfWeek, FulfillmentDate);
                    OnPropertyChanged(nameof(FulfillmentDate));
                }
            }
        }
      

        private string _fulfillmentDayOfWeek;
        public string FulfillmentDayOfWeek
        {
            get { return _fulfillmentDayOfWeek;}
            set
            {
                if (value != _fulfillmentDayOfWeek)
                {
                    _fulfillmentDayOfWeek = value;
                    UpdateFulfillmentDate(FulfillmentDayOfWeek, FulfillmentDate);
                    OnPropertyChanged(nameof(FulfillmentDayOfWeek));
                }
            }
        }

        private bool _canDelete;
        public bool CanDelete
        {
            get { return _canDelete; }
            set
            {
                if(value != _canDelete)
                {
                    _canDelete = value;
                    OnPropertyChanged(nameof(CanDelete));
                }
            }
        }

        private bool _canSave;
        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                if (value != _canSave)
                {
                    _canSave = value;
                    OnPropertyChanged(nameof(CanSave));
                }
            }
        }

        private bool _canFreeze;
        public bool CanFreeze
        {
            get { return _canFreeze; }
            set
            {
                if (value != _canFreeze)
                {
                    _canFreeze = value;
                    OnPropertyChanged(nameof(CanFreeze));
                }
            }
        }

        private bool _canModify;
        public bool CanModify
        {
            get { return _canModify; }
            set
            {
                if (value != _canModify)
                {
                    _canModify = value;
                    OnPropertyChanged(nameof(CanModify));
                }
            }
        }
        public bool IsFrozen
        {
            get { return (Order.IsFrozen != null) ? Order.IsFrozen : false; }
            set 
            {
                if(value != Order.IsFrozen)
                {
                    Order.IsFrozen = value;
                    OnPropertyChanged(nameof(IsFrozen));
                }
            }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get { return (_isEdit != null) ? _isEdit : false; }
            set
            {
                if (value != _isEdit)
                {
                    _isEdit = value;
                    OnPropertyChanged(nameof(IsEdit));
                }
            }
        }

        private int _totalAmount3;
        public int TotalAmount3
        {
            get { return _totalAmount3; }
            set
            {
                if (value != _totalAmount3)
                {
                    _totalAmount3 = value;
                    OnPropertyChanged(nameof(TotalAmount3));
                }
            }
        }

        private int _totalAmount5;
        public int TotalAmount5
        {
            get { return _totalAmount5; }
            set
            {
                if (value != _totalAmount5)
                {
                    _totalAmount5 = value;
                    OnPropertyChanged(nameof(TotalAmount5));
                }
            }
        }

        private int _totalAmount8;
        public int TotalAmount8
        {
            get { return _totalAmount8; }
            set
            {
                if (value != _totalAmount8)
                {
                    _totalAmount8 = value;
                    OnPropertyChanged(nameof(TotalAmount8));
                }
            }
        }

        private int _totalAmount10;
        public int TotalAmount10
        {
            get { return _totalAmount10; }
            set
            {
                if (value != _totalAmount10)
                {
                    _totalAmount10 = value;
                    OnPropertyChanged(nameof(TotalAmount10));
                }
            }
        }

        private int _totalAmountReg;
        public int TotalAmountReg
        {
            get { return _totalAmountReg; }
            set
            {
                if (value != _totalAmountReg)
                {
                    _totalAmountReg = value;
                    OnPropertyChanged(nameof(TotalAmountReg));
                }
            }
        }

        public ObservableCollection<PetsiOrderLineItem> LineItems { get; set; }
        public ObservableCollection<string> OrderTypes { get; set; }
        public ObservableCollection<string> OrderFrequencies { get; set; }
        public ObservableCollection<string> FulfillmentTypes { get; set; }
        public ObservableCollection<string> DaysOfWeek { get; set; }
        #endregion

        #region Commands

        public RelayCommand BackCommand {  get; set; }
        public RelayCommand EditCommand {  get; set; }
        public RelayCommand AddLineCommand {  get; set; }
        public RelayCommand DeleteLineCommand {  get; set; }
        public RelayCommand SaveOrderCommand {  get; set; }
        public RelayCommand DeleteOrderCommand {  get; set; }

        #endregion

        public OrderItemViewModel(PetsiOrder? orderContext)
        {
            Order = new PetsiOrder(orderContext);

            if(orderContext == null)
            {
                Order.OrderId = PetsiOrder.GenerateOrderId();
                Order.IsUserEntered = true;
                Order.InputOriginType = Identifiers.USER_ENTERED_INPUT;
                IsEdit = true;
                CanDelete = false;
                CanSave = true;
                CanFreeze = true;
                CanModify = true;
                LineItems = new ObservableCollection<PetsiOrderLineItem>();
            }
            else
            {
                Time = DateTime.Parse(orderContext.OrderDueDate).ToShortTimeString();
                FulfillmentDate = DateTime.Parse(orderContext.OrderDueDate);

                if (orderContext.IsPeriodic) { OrderFrequency = Identifiers.ORDER_FREQUENCY_WEEKLY; }
                else if (orderContext.IsOneShot) { OrderFrequency = Identifiers.ORDER_FREQUENCY_ONE_TIME; }

                if(Order.IsUserEntered)
                {
                    CanDelete = true;
                    CanModify = true;
                }
                else
                {
                    CanDelete = false;
                    CanModify = false;
                }

                IsEdit = false;

                LineItems = new ObservableCollection<PetsiOrderLineItem>(orderContext.LineItems);
                
            }

            UpdateColumnTotals(this, EventArgs.Empty);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            OrderTypes = new ObservableCollection<string>(orderModel.GetOrderTypes());
            OrderFrequencies = new ObservableCollection<string>() { Identifiers.ORDER_FREQUENCY_WEEKLY, Identifiers.ORDER_FREQUENCY_ONE_TIME };
            FulfillmentTypes = new ObservableCollection<string>() { Identifiers.FULFILLMENT_PICKUP, Identifiers.FULFILLMENT_DELIVERY };
            DaysOfWeek = InitDayOfWeekSelection();

            LineItems.CollectionChanged += (s, e) => Order.LineItems = LineItems.ToList();

            IsWeekly = Order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE;
            IsNotWeekly = !IsWeekly;

            BackCommand = new RelayCommand(o => { MainViewModel.Instance().BackOrderView(); });
            EditCommand = new RelayCommand(o => { ToggleEditing(); });
            AddLineCommand = new RelayCommand(o => { AddLine(); });
            DeleteLineCommand = new RelayCommand(o => { DeleteLine(o); });
            SaveOrderCommand = new RelayCommand(o => { SaveOrder();  });
            DeleteOrderCommand = new RelayCommand(o => { DeleteOrder();  });

            OrderLineItemEvents.Instance.OnQuantityChange += UpdateColumnTotals;
        }

        private ObservableCollection<string> InitDayOfWeekSelection()
        {
            return new ObservableCollection<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        }

        private void ToggleEditing()
        {
            foreach(var item in LineItems)
            {
                item.IsReadOnly = !IsEdit;
            }
        }
        private void AddLine()
        {
            LineItems.Add(new PetsiOrderLineItem());
        }
        
        private void DeleteLine(object o)
        {
            if (o is PetsiOrderLineItem lineItem)
            {
                ConfirmationWindow confirmationWindow = new ConfirmationWindow(null);
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    int count = LineItems.Count;

                    //Setting quantities to 0 before removing triggers the update quantity event to properly update the column totals
                    lineItem.AmountRegular = 0;
                    lineItem.Amount3 = 0;
                    lineItem.Amount5 = 0;
                    lineItem.Amount8 = 0;
                    lineItem.Amount10 = 0;

                    LineItems.Remove(lineItem);
                    if (LineItems.Count != count - 1)
                    {
                        SystemLogger.Log("Delete Line Command failed to remove");
                    }
                }
                
            }
        }

        private void SaveOrder()
        {
            if (FulfillmentDate != null)
            {
                try
                {
                    Order.OrderDueDate = DateTime.Parse(FulfillmentDate.Value.ToShortDateString() + " " + Time).ToString();
                }
                catch (FormatException e) 
                {
                    OrderItemViewEvents.RaiseTimeInvalidEvent();
                    return;
                }
            }
            if(IsValidOrder())
            {
                ObsOrderModelSingleton.Instance.UpdateOrder(Order);
                OrderItemViewEvents.RaiseSaveSuccessEvent();
            }
        }

        private void DeleteOrder()
        {     
            ConfirmationWindow confirmationWindow = new ConfirmationWindow(null);
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                ObsOrderModelSingleton.Instance.RemoveOrder(Order);
                MainViewModel.Instance().BackOrderView();
            }   
        }

        private void UpdateColumnTotals(object sender, EventArgs e)
        {
            int amount3Sum = 0;
            int amount5Sum = 0;
            int amount8Sum = 0;
            int amount10Sum = 0;
            int amountRegSum = 0;
            foreach (var lineItem in LineItems)
            {
                amount3Sum += lineItem.Amount3;
                amount5Sum += lineItem.Amount5;
                amount8Sum += lineItem.Amount8;
                amount10Sum += lineItem.Amount10;
                amountRegSum += lineItem.AmountRegular;
            }
            TotalAmount3 = amount3Sum;
            TotalAmount5 = amount5Sum;
            TotalAmount8 = amount8Sum;
            TotalAmount10 = amount10Sum;
            TotalAmountReg = amountRegSum;
        }
        private bool IsValidOrder()
        {
            bool controlbool = true;

            //Make events to highlight the relevant fields red? Already want events to modify order state based on changs, like to order type
            // weekly -> DOTW combo box, oneTime -> datepicker, ect.
            
            if(Recipient == null) { controlbool = false; OrderItemViewEvents.RaiseRecipientInvalidEvent(); }
            if(FulfillmentType == null) { controlbool = false; OrderItemViewEvents.RaiseFulfillmentInvalidEvent(); }
            if(OrderType == null) { controlbool = false; OrderItemViewEvents.RaiseOrderTypeInvalidEvent(); }
            if(OrderFrequency == "") { controlbool = false; OrderItemViewEvents.RaiseFrequencyInvalidEvent(); }

            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if (!IsValidLineItems(cs)) { controlbool = false; OrderItemViewEvents.RaiseLineItemsInvalidEvent(); }

            if(OrderType != Identifiers.ORDER_TYPE_WHOLESALE && FulfillmentType == Identifiers.FULFILLMENT_DELIVERY) 
            {
                if(DeliveryAddr == null) { controlbool = false; OrderItemViewEvents.RaiseDelAddressEvent(); }
                if(PhoneNumber == null) { controlbool = false; OrderItemViewEvents.RaisePhoneInvalidEvent(); }
            }

            //if(OrderFrequency == Identifiers.ORDER_FREQUENCY_ONE_TIME && FulfillmentDate == default) { controlbool = false; OrderItemViewEvents.RaiseDatePickerInvalidEvent(); }
            if(OrderFrequency == Identifiers.ORDER_FREQUENCY_ONE_TIME && FulfillmentDate < DateTime.Today) { controlbool = false; OrderItemViewEvents.RaiseDatePickerLessThanEvent(); }

            if(FulfillmentDate == null || FulfillmentDayOfWeek == null) { controlbool = false; OrderItemViewEvents.RaiseDatePickerInvalidEvent(); }

            return controlbool;
        }

        private bool IsValidLineItems(CatalogService cs)
        {
            if(LineItems.Count == 0) { return false; }
            foreach (PetsiOrderLineItem lineItem in LineItems)
            {
                string id = cs.GetCatalogObjectId(lineItem.ItemName);

                if (id == "") 
                { return false; }

                if(lineItem.CatalogObjectId == "") { lineItem.CatalogObjectId = id; }

                if (lineItem.CatalogObjectId != id) 
                { return false; }

                if (lineItem.ItemName == "" || lineItem.ItemName == null) 
                { return false; }

                if(lineItem.AmountRegular != 0 && !lineItem.IsValidSize(Identifiers.SIZE_REGULAR)) 
                { return false; }
                if(lineItem.Amount3 != 0 && !lineItem.IsValidSize(Identifiers.SIZE_CUTIE)) 
                { return false; }
                if(lineItem.Amount5 != 0 && !lineItem.IsValidSize(Identifiers.SIZE_SMALL)) 
                { return false; }
                if(lineItem.Amount8 != 0 && !lineItem.IsValidSize(Identifiers.SIZE_MEDIUM)) 
                { return false; }
                if(lineItem.Amount10 != 0 && !lineItem.IsValidSize(Identifiers.SIZE_LARGE)) 
                { return false; }

                if (lineItem.AmountRegular == 0
                       && lineItem.Amount3 == 0
                       && lineItem.Amount5 == 0
                       && lineItem.Amount8 == 0
                      && lineItem.Amount10 == 0) 
                { return false; }
            }
            return true;
        }

        /// <summary>
        /// When FulfillmentDayofWeek is set, update FulfillmentDate to reflect new day, based on current setting of FulfillmentDate.
        /// </summary>
        /// <param name="fulfillmentDayOfWeek"></param>
        /// <param name="fulfillmentDate"></param>
        private void UpdateFulfillmentDate(string fulfillmentDayOfWeek, DateTime? fulfillmentDate)
        {
            if (fulfillmentDate == default || fulfillmentDate == null)
            {
                fulfillmentDate = DateTime.Today;
            }

            if (!Enum.TryParse(fulfillmentDayOfWeek, true, out DayOfWeek targetDayOfWeek))
            {
                SystemLogger.Log("OrderItemView FulfillmentDate update Invalid day of the week.");
            }

            int currentDayOfWeek = (int)fulfillmentDate.Value.DayOfWeek;
            int targetDayOfWeekInt = (int)targetDayOfWeek;

            int dayDifference = targetDayOfWeekInt - currentDayOfWeek;
            FulfillmentDate = fulfillmentDate.Value.AddDays(dayDifference);
        }
        /// <summary>
        /// When FulfillmentDate is set, update FulfillmentDate to reflect the date, converts the date to day of week.
        /// </summary>
        /// <param name="fulfillmentDayOfWeek"></param>
        /// <param name="fulfillmentDate"></param>
        private void UpdateFulfillmentDayOfWeek(string fulfillmentDayOfWeek, DateTime? fulfillmentDate)
        {
            FulfillmentDayOfWeek = fulfillmentDate.Value.DayOfWeek.ToString();
        }
    }
}
