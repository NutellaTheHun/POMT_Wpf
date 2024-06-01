using POMT_WPF.Core;
using System;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LabelViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
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

        }
    }
}