﻿using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.Interfaces;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;
using Petsi.Utils;
using System.Windows.Data;
using System.ComponentModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class OrderViewModel : ViewModelBase, IObsOrderModelSubscriber
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
        }
        private void NoFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = !order.IsFrozen;
            }
        }

        private void WsFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = !order.IsFrozen && order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE;
            }
        }

        private void SqFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = !order.IsFrozen && order.OrderType == Identifiers.ORDER_TYPE_SQUARE;
            }
        }

        private void RtFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = !order.IsFrozen && order.OrderType == Identifiers.ORDER_TYPE_RETAIL;
            }
        }

        private void SpFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = !order.IsFrozen && order.OrderType == Identifiers.ORDER_TYPE_SPECIAL;
            }
        }

        private void FrFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = order.IsFrozen;
            }
        }
        private void SearchBarFilter(object sender, FilterEventArgs e)
        {
            PetsiOrder order = e.Item as PetsiOrder;
            if (order != null)
            {
                e.Accepted = order.IsFrozen;
            }
        }

        /// <summary>
        /// filters:
        /// Square -> InputOriginType: SQUARE_ORDER_INPUT
        ///  Wholesale -> orderType: wholesale, IsPeriodic? (isUserEntered)
        /// SpecialOrders(Other) -> IsUserEntered, IsOneShot?      (isUserEntered)
        /// OrderTypes:
        ///     Square
        ///     Wholesale
        ///     Ez-Cater
        ///     SpecialOrder
        /// InputOriginType:
        ///     Square
        ///     UserEntered
        ///     Ez-Cater
        /// </summary>
        /// <param name="filter"></param>
        public void FilterOrderType(string? orderTypefilter)
        {
            activeFilter = orderTypefilter;

            if (orderTypefilter == null)
            {
                _orders = ObsOrderModelSingleton.Instance.Orders;
                TotalOrderCount = _orders.Count;
            }
            else
            {
                _orders = new ObservableCollection<PetsiOrder>(ObsOrderModelSingleton.Instance.Orders.Where(x => x.OrderType == orderTypefilter));
                TotalOrderCount = _orders.Count;
            }
        }

        public void FilterSearchBar(string text)
        {
            ObservableCollection<PetsiOrder> modelOrders = ObsOrderModelSingleton.Instance.Orders;
            ObservableCollection<PetsiOrder> results = new ObservableCollection<PetsiOrder>();
            foreach (PetsiOrder order in modelOrders)
            {
                if (order.Recipient.ToLower().Contains(text.ToLower()))
                {
                    results.Add(order);
                    continue;
                }
                foreach (PetsiOrderLineItem lineItem in order.LineItems)
                {
                    if (lineItem.ItemName.ToLower().Contains(text.ToLower()))
                    {
                        results.Add(order);
                        continue;
                    }
                }
            }
            _orders = results;
            TotalOrderCount = _orders.Count;
        }

        public void Update()
        {
           // UpdateOrderList();
            //UpdateFrozenOrderList();
            //view.UpdateDataGrid();
        }
    }
}
