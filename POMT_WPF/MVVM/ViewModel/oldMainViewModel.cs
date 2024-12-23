﻿using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public CatalogItemPetsi viewedCatalogItem;
        public PetsiOrder viewedOrderItem;
        public string orderViewFilter;

		public PetsiOrder SelectedOrder;

        public RelayCommand CloseApp { get; set; }
        public RelayCommand CatalogViewCommand { get; set; }
        public RelayCommand LabelViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public CatalogViewModel CatalogVM { get; set; }
        public LabelViewModel LabelVM { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public ReportViewModel ReportVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        public OrderItemViewModel OrderItemVM { get; set; }
        public CatalogItemViewModel CatalogItemVM { get; set; }
        public ConfigureLabelsViewModel ConfigureLabelsVM { get; set; }
        public TemplateListViewModel TemplateListVM { get; set; }

        private object _currentView;
		public object CurrentView
		{
			get { return _currentView; }
			set
			{ 
				_currentView = value;
				OnPropertyChanged();
			}
		}

		private static MainViewModel _instance;
		public static MainViewModel Instance()
		{
			if( _instance == null) _instance = new MainViewModel();
			return _instance;
		}

        public void OpenOrderItemView(object? o)
        {
            if (o is PetsiOrder order)
            {
                OrderItemVM = new OrderItemViewModel(order);
                viewedOrderItem = order;
                CurrentView = OrderItemVM;
            }
        }

        public void OpenNewOrderItemView()
        {
            OrderItemVM = new OrderItemViewModel(null);
            CurrentView = OrderItemVM;
        }

        public void OpenCatalogItemView(object? o)
        {
            if (o is CatalogItemPetsi item)
            {
                CatalogItemVM = new CatalogItemViewModel(item);
                viewedCatalogItem = item;
            }
            else CatalogItemVM = new CatalogItemViewModel(null);
            CurrentView = CatalogItemVM;
        }

        public void OpenConfigureLabelView(bool IsFromSettingsVM)
        {
            if(ConfigureLabelsVM == null) { ConfigureLabelsVM = new ConfigureLabelsViewModel(/*IsFromSettingsVM*/); }
            ConfigureLabelsVM.IsFromSettingsVM = IsFromSettingsVM;
            CurrentView = ConfigureLabelsVM;
        }

        public void OpenTemplateListView(bool IsFromSettingsVM)
        {
            if(TemplateListVM == null) { TemplateListVM = new TemplateListViewModel(/*IsFromSettingsVM*/); }
            TemplateListVM.IsFromSettingsVM = IsFromSettingsVM;
            CurrentView = TemplateListVM;
        }

        public void OpenCatalogListView()
        {
            CurrentView = CatalogVM;
        }

        public void BackOrderView()
        {
            CurrentView = OrderVM;
        }

        public void BackCatalogView()
        {
            CurrentView = CatalogVM;
        }

        public void BackSettingsView()
        {
            CurrentView = SettingsVM;
        }

        public void BackLabelView(bool isFromSettingsVM)
        {
            if(isFromSettingsVM)
            {
                CurrentView = SettingsVM;
            }
            else
            {
                CurrentView = LabelVM;
            }
        }

        public void BackTmpltLstView(bool isFromSettingsVM)
        {
            if(isFromSettingsVM)
            {
                CurrentView = SettingsVM;
            }
            else
            {
                CurrentView = ReportVM;
            }
        }

        private MainViewModel()
		{
            
            CatalogVM = new CatalogViewModel();
            LabelVM = new LabelViewModel();
            OrderVM = new OrderViewModel();
            ReportVM = new ReportViewModel();
            SettingsVM = new SettingsViewModel();
            
            CurrentView = OrderVM;

            CloseApp = new RelayCommand(o =>{ System.Windows.Application.Current.Shutdown(); });

			CatalogViewCommand = new RelayCommand(o =>{ CurrentView = CatalogVM; });

			LabelViewCommand = new RelayCommand(o =>{ CurrentView = LabelVM; });

			OrderViewCommand = new RelayCommand(o =>{ CurrentView = OrderVM; });

			ReportViewCommand = new RelayCommand(o =>{ CurrentView = ReportVM; });

			SettingsViewCommand = new RelayCommand(o =>{ CurrentView = SettingsVM; });

            orderViewFilter = "All_rb";
        } 
    }
}