using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using System.Collections.ObjectModel;

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
                    OnPropertyChanged(nameof(OrderFrequency));
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
        public bool IsFrozen
        {
            get { return Order.IsFrozen; }
            set 
            {
                if(value != Order.IsFrozen)
                {
                    Order.IsFrozen = value;
                    OnPropertyChanged(nameof(IsFrozen));
                }
            }
        }
        private bool _isEdit; //--
        public bool IsEdit
        {
            get { return _isEdit; }
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
        public RelayCommand FreezeCommand {  get; set; }
        public RelayCommand EditCommand {  get; set; }
        public RelayCommand AddLineCommand {  get; set; }
        public RelayCommand DeleteLineCommand {  get; set; }
        public RelayCommand SaveLineCommand {  get; set; }
        public RelayCommand DeleteOrderCommand {  get; set; }

        #endregion

        public OrderItemViewModel(PetsiOrder? orderContext)
        {
            Order = new PetsiOrder(orderContext);

            if(orderContext == null)
            {
                IsEdit = true;

                LineItems = new ObservableCollection<PetsiOrderLineItem>();
            }
            else
            {
                Time = DateTime.Parse(orderContext.OrderDueDate).ToShortTimeString();
                FulfillmentDate = DateTime.Parse(orderContext.OrderDueDate);

                if (orderContext.IsPeriodic) { OrderFrequency = "Weekly"; }
                else if (orderContext.IsOneShot) { OrderFrequency = "One Time"; }

                IsEdit = false;

                LineItems = new ObservableCollection<PetsiOrderLineItem>(orderContext.LineItems);
            }

            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderTypes = new ObservableCollection<string>(omp.GetOrderTypes());
            OrderFrequencies = new ObservableCollection<string>() { "Weekly", "One Time" };
            FulfillmentTypes = new ObservableCollection<string>() { "PICKUP", "DELIVERY" };

            LineItems.CollectionChanged += (s, e) => Order.LineItems = LineItems.ToList();
            LineItems.CollectionChanged += (s, e) => UpdateColumnTotals();

            BackCommand = new RelayCommand(o => { MainViewModel.Instance().BackOrderViewCommand(); });
            FreezeCommand = new RelayCommand(o => {  });
            EditCommand = new RelayCommand(o => {  });
            AddLineCommand = new RelayCommand(o => {  });
            DeleteLineCommand = new RelayCommand(o => {  });
            SaveLineCommand = new RelayCommand(o => {  });
            DeleteOrderCommand = new RelayCommand(o => {  });
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
    }
}
