using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;
using Petsi.Utils;
using System.Windows.Data;
using System.ComponentModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        public RelayCommand OpenOrderItemView { get; set; }
        public RelayCommand OpenNewOrderItemView { get; set; }
        public RelayCommand FilterNone { get; set; }
        public RelayCommand FilterWholesale { get; set; }
        public RelayCommand FilterSquare { get; set; }
        public RelayCommand FilterRetail { get; set; }
        public RelayCommand FilterSpecial { get; set; }
        public RelayCommand FilterFrozen { get; set; }

        public ObservableCollection<PetsiOrder> _orders { get; set; }
        public CollectionViewSource DashboardOrders { get; } = new CollectionViewSource();
        public ICollectionView DashBoardOrdersView => DashboardOrders.View;
        
        private int _totalOrderCount;
        public int TotalOrderCount
        {
            get { return _totalOrderCount; }
            set
            {
                _totalOrderCount = value;
                OnPropertyChanged(nameof(TotalOrderCount));
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    DashBoardOrdersView.Refresh();
                    TotalOrderCount = DashboardOrders.View.Cast<object>().Count();
                }
            }
        }

        private string activeFilter;
        private FilterEventHandler currentFilter;
        public OrderViewModel() 
        {
            _orders = ObsOrderModelSingleton.Instance.Orders;
         
            DashboardOrders.Source = _orders;
            DashBoardOrdersView.MoveCurrentTo(null);
            currentFilter = NoFilter;
            DashboardOrders.Filter += currentFilter;  

            TotalOrderCount = _orders.Count();

            OpenOrderItemView = new RelayCommand(o => { MainViewModel.Instance().OpenOrderItemView(o); DashBoardOrdersView.MoveCurrentTo(null); });

            OpenNewOrderItemView = new RelayCommand(o => { MainViewModel.Instance().OpenNewOrderItemView(); DashBoardOrdersView.MoveCurrentTo(null); });

            FilterNone = new RelayCommand(o => { ChangeFilter(NoFilter);});

            FilterWholesale = new RelayCommand(o => {ChangeFilter(WsFilter);});

            FilterSquare = new RelayCommand(o => {  ChangeFilter(SqFilter);});

            FilterRetail = new RelayCommand(o => {  ChangeFilter(RtFilter);});

            FilterSpecial = new RelayCommand(o => {  ChangeFilter(SpFilter);});

            FilterFrozen = new RelayCommand(o => { ChangeFilter(FrFilter);});
        }

        private void ChangeFilter(FilterEventHandler newFilter)
        {
            DashboardOrders.Filter -= currentFilter;
            currentFilter = newFilter;
            DashboardOrders.Filter += currentFilter;
            DashBoardOrdersView.MoveCurrentTo(null);
            DashBoardOrdersView.Refresh();
            TotalOrderCount = DashboardOrders.View.Cast<object>().Count();
        }
        private void NoFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null && !order.IsFrozen)
            {
                e.Accepted = OrderContainsSearchQuery(order);
            }
        }

        private void WsFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null && !order.IsFrozen)
            {
                e.Accepted = OrderContainsSearchQuery(order) && order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE;
            }
        }

        private void SqFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null && !order.IsFrozen)
            {
                e.Accepted = OrderContainsSearchQuery(order) && order.OrderType == Identifiers.ORDER_TYPE_SQUARE;
            }
        }

        private void RtFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null && !order.IsFrozen)
            {
                e.Accepted = OrderContainsSearchQuery(order) && order.OrderType == Identifiers.ORDER_TYPE_RETAIL;
            }
        }

        private void SpFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null && !order.IsFrozen)
            {
                e.Accepted = OrderContainsSearchQuery(order) && order.OrderType == Identifiers.ORDER_TYPE_SPECIAL;
            }
        }

        private void FrFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = OrderContainsSearchQuery(order) && order.IsFrozen;
            }
        }

        private bool OrderContainsSearchQuery(PetsiOrder order)
        {
            if(SearchQuery == "" || SearchQuery == null) { return true; }

            if (order != null && !order.IsFrozen)
            {
                if (order.Recipient.ToLower().Contains(SearchQuery.ToLower())) { return true; }
                foreach (PetsiOrderLineItem lineItem in order.LineItems)
                {
                    if (lineItem.ItemName.ToLower().Contains(SearchQuery.ToLower())) { return true; }
                }
            }
            return false;
        }
    }
}
