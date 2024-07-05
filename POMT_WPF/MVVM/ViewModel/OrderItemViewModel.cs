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
                    if (OrderFrequency == Identifiers.ORDER_FREQUENCY_WEEKLY) { Order.IsPeriodic = true; }
                    else if (OrderFrequency == Identifiers.ORDER_FREQUENCY_ONE_TIME) { Order.IsOneShot = true; }
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

        private DateTime _fulfillmentDate;
        public DateTime FulfillmentDate
        {
            get { if (_fulfillmentDate != null) return _fulfillmentDate; return default; }
            set
            {
                if (value != _fulfillmentDate)
                {
                    _fulfillmentDate = value;
                    OnPropertyChanged(nameof(FulfillmentDate));
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

            UpdateColumnTotals();

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            OrderTypes = new ObservableCollection<string>(orderModel.GetOrderTypes());
            OrderFrequencies = new ObservableCollection<string>() { Identifiers.ORDER_FREQUENCY_WEEKLY, Identifiers.ORDER_FREQUENCY_ONE_TIME };
            FulfillmentTypes = new ObservableCollection<string>() { Identifiers.FULFILLMENT_PICKUP, Identifiers.FULFILLMENT_DELIVERY };

            LineItems.CollectionChanged += (s, e) => Order.LineItems = LineItems.ToList();
            LineItems.CollectionChanged += (s, e) => UpdateColumnTotals(); //Doesnt work?

            BackCommand = new RelayCommand(o => { MainViewModel.Instance().BackOrderView(); });
            EditCommand = new RelayCommand(o => { ToggleEditing(); });
            AddLineCommand = new RelayCommand(o => { AddLine(); });
            DeleteLineCommand = new RelayCommand(o => { DeleteLine(o); });
            SaveOrderCommand = new RelayCommand(o => { SaveOrder();  });
            DeleteOrderCommand = new RelayCommand(o => { DeleteOrder();  });
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
            Order.OrderDueDate = DateTime.Parse(FulfillmentDate.ToShortDateString() +" " + Time).ToString();

            if(IsValidOrder())
            {
                ObsOrderModelSingleton.Instance.AddOrder(Order);
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

        private void UpdateColumnTotals()
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
            
            if(Recipient == null) { controlbool = false; OrderItemViewEvents.OnRecipientInvalid(); }
            if(FulfillmentType == null) { controlbool = false; OrderItemViewEvents.OnFulfillmentInvalid(); }
            if(OrderType == null) { controlbool = false; OrderItemViewEvents.OnOrderTypeInvalid(); }
            if(OrderFrequency == "") { controlbool = false; OrderItemViewEvents.OnFrequencyInvalid(); }

            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if (!IsValidLineItems(cs)) { controlbool = false; OrderItemViewEvents.OnLineItemsInvalid(); }

            if(OrderType != Identifiers.ORDER_TYPE_WHOLESALE && FulfillmentType == Identifiers.FULFILLMENT_DELIVERY) 
            {
                if(DeliveryAddr == null) { controlbool = false; OrderItemViewEvents.OnDelAddressInvalid(); }
                if(PhoneNumber == null) { controlbool = false; OrderItemViewEvents.OnPhoneInvalid(); }
            }

            if(OrderFrequency == Identifiers.ORDER_FREQUENCY_ONE_TIME && FulfillmentDate == default) { controlbool = false; OrderItemViewEvents.OnDatePickerInvalid(); }
            if(OrderFrequency == Identifiers.ORDER_FREQUENCY_ONE_TIME && FulfillmentDate < DateTime.Today) { controlbool = false; OrderItemViewEvents.OnDatePickerLessThanInvalid(); }

            if(OrderFrequency == Identifiers.ORDER_FREQUENCY_WEEKLY /* && DOTW == null */) { controlbool = false; OrderItemViewEvents.OnDOTWInvalid(); }

            return controlbool;
        }

        private bool IsValidLineItems(CatalogService cs)
        {
            foreach (PetsiOrderLineItem lineItem in LineItems)
            {
                string id = cs.GetCatalogObjectId(lineItem.ItemName);
                if (id == "") { return false; }
                if(lineItem.CatalogObjectId == "") { lineItem.CatalogObjectId = id; }
                if (lineItem.CatalogObjectId != id) { return false; }
                if (lineItem.ItemName == "" || lineItem.ItemName == null) { return false; }
                if (lineItem.AmountRegular == 0
                       && lineItem.Amount3 == 0
                       && lineItem.Amount5 == 0
                       && lineItem.Amount8 == 0
                      && lineItem.Amount10 == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
