using POMT_WPF.Core;
using POMT_WPF.MVVM.View;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
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

        public CatalogView CatalogView { get; set; }
        public LabelView LabelView { get; set; }
        public OrderView OrderView { get; set; }
        public ReportView ReportView { get; set; }
        public SettingsView SettingsView { get; set; }


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

		public MainViewModel()
		{
            CatalogVM = new CatalogViewModel();
            LabelVM = new LabelViewModel();
			OrderVM = new OrderViewModel();
			ReportVM = new ReportViewModel();
			SettingsVM = new SettingsViewModel();

			CatalogView = new CatalogView();
			LabelView = new LabelView();
			OrderView = new OrderView();
			ReportView = new ReportView();
			SettingsView = new SettingsView();
			
			CurrentView = OrderVM;

			CatalogViewCommand = new RelayCommand(o =>{ CurrentView = CatalogView; });

			LabelViewCommand = new RelayCommand(o =>{ CurrentView = LabelView; });

			OrderViewCommand = new RelayCommand(o =>{ CurrentView = OrderView; });

			ReportViewCommand = new RelayCommand(o =>{ CurrentView = ReportView; });

			SettingsViewCommand = new RelayCommand(o =>{ CurrentView = SettingsView; });
        }
    }
}