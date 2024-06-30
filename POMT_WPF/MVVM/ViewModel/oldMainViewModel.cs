using Petsi.Units;
using POMT_WPF.Core;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
		public PetsiOrder SelectedOrder;

        public RelayCommand CatalogViewCommand { get; set; }
        public RelayCommand LabelViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        //public RelayCommand OrderItemViewCommand { get; set; }

        /*
        public CatalogView CatalogView { get; set; }
        public LabelView LabelView { get; set; }
        public OrderView OrderView { get; set; }
        public ReportView ReportView { get; set; }
        public SettingsView SettingsView { get; set; }

		public OrderItemView OrderItemView { get; set; }
        */

        public CatalogViewModel CatalogVM { get; set; }
        public LabelViewModel LabelVM { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public ReportViewModel ReportVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        public OrderItemViewModel OrderItemVM { get; set; }
        public CatalogItemViewModel CatalogItemVM { get; set; }


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

        public void OrderItemViewCommand(object? o)
        {
            if (o is PetsiOrder order) OrderItemVM = new OrderItemViewModel(order);
            else  OrderItemVM = new OrderItemViewModel(null);
            CurrentView = OrderItemVM;
        }

        public void CatalogItemViewCommand(object? o)
        {
            if (o is CatalogItemPetsi order) CatalogItemVM = new CatalogItemViewModel(order);
            else CatalogItemVM = new CatalogItemViewModel(null);
            CurrentView = CatalogItemVM;
        }

        public void BackOrderViewCommand()
        {
            CurrentView = OrderVM;
        }

        public void BackCatalogViewCommand()
        {
            CurrentView = CatalogVM;
        }

        private MainViewModel()
		{
            CatalogVM = new CatalogViewModel();
            LabelVM = new LabelViewModel();
            OrderVM = new OrderViewModel();
            ReportVM = new ReportViewModel();
            SettingsVM = new SettingsViewModel();

            //OrderItemVM = new OrderItemViewModel();

            /*
            CatalogView = new CatalogView();
            LabelView = new LabelView();
            OrderView = new OrderView();
            ReportView = new ReportView();
            SettingsView = new SettingsView();

            OrderItemView = new OrderItemView();
            */

            CurrentView = OrderVM;

			CatalogViewCommand = new RelayCommand(o =>{ CurrentView = CatalogVM; });

			LabelViewCommand = new RelayCommand(o =>{ CurrentView = LabelVM; });

			OrderViewCommand = new RelayCommand(o =>{ CurrentView = OrderVM; });

			ReportViewCommand = new RelayCommand(o =>{ CurrentView = ReportVM; });

			SettingsViewCommand = new RelayCommand(o =>{ CurrentView = SettingsVM; });

            //OrderItemViewCommand = new RelayCommand(o =>{ CurrentView = OrderItemVM; });
        }

    }
}