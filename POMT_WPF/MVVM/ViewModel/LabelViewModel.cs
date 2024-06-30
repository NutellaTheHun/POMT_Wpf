using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using POMT_WPF.Core;

namespace POMT_WPF.MVVM.ViewModel
{
    public class LabelViewModel : ViewModelBase
    {
        private LabelService ls;

        private DateTime _selectedDate;
        public DateTime SelectedDate 
        { 
            get 
            {
                return _selectedDate;
            } 
            set 
            { 
                if (_selectedDate != value) 
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                } 
            } 
        }

        public RelayCommand PrintStandard {  get; set; }
        public RelayCommand PrintSmall {  get; set; }
        public RelayCommand PrintRound {  get; set; }
        public RelayCommand ConfigureLabels {  get; set; }

        public LabelViewModel()
        {
            ls = (LabelService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_LABEL);
            ls.ValidateFilePaths();

            SelectedDate = DateTime.Today;

            PrintStandard = new RelayCommand(o => { ls.Print_4x2(SelectedDate); });
            PrintSmall = new RelayCommand(o => { ls.Print_2x1(SelectedDate); });
            PrintRound = new RelayCommand(o => { ls.Print_4x2(SelectedDate); });
            ConfigureLabels = new RelayCommand(o => { ls.Print_4x2(SelectedDate); });
        }
    }
}
