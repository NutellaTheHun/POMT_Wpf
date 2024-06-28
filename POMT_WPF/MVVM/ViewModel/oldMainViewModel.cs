using POMT_WPF.Core;

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
			
			CurrentView = OrderVM;

			CatalogViewCommand = new RelayCommand(o =>
			{
				CurrentView = CatalogVM;
			});

			LabelViewCommand = new RelayCommand(o =>
			{
				CurrentView = LabelVM;
			});

			OrderViewCommand = new RelayCommand(o =>
			{
				CurrentView = OrderVM;
			});

			ReportViewCommand = new RelayCommand(o =>
			{
				CurrentView = ReportVM;
			});

			SettingsViewCommand = new RelayCommand(o =>
			{
				CurrentView = SettingsVM;
			});
        }
    }
}