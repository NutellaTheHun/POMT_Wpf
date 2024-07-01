using Petsi.Reports;
using POMT_WPF.Core;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        #region Props
        private bool _retail;
        public bool RetailFilter
        {
            get { return _retail; }
            set 
            {
                if (_retail != value)
                {
                    _retail = value;
                    OnPropertyChanged(nameof(RetailFilter));
                }
            }
        }

        private bool _square;
        public bool SquareFilter
        {
            get { return _square; }
            set 
            {
                if (_square != value)
                {
                    _square = value;
                    OnPropertyChanged(nameof(SquareFilter));
                }
            }
        }

        private bool _wholesale;
        public bool WholesaleFilter
        {
            get { return _wholesale; }
            set 
            { 
                if (_wholesale != value)
                {
                    _wholesale = value;
                    OnPropertyChanged(nameof(WholesaleFilter));
                }
                
            }
        }

        private bool _special;
        public bool SpecialFilter
        {
            get { return _special; }
            set 
            {
                if (_special != value)
                {
                    _special = value;
                    OnPropertyChanged(nameof(SpecialFilter));
                }
            }
        }

        private bool _ezCater;
        public bool EzCaterFilter
        {
            get { return _ezCater; }
            set 
            { 
                if (_ezCater != value)
                {
                    _ezCater = value;
                    OnPropertyChanged(nameof(EzCaterFilter));
                }             
            }
        }

        private string _pieTemplateName;
        private string PieTemplateName 
        {
            get { return _pieTemplateName; }
            set
            {
                if( _pieTemplateName != value)
                {
                    _pieTemplateName = value;
                    OnPropertyChanged(nameof(PieTemplateName));
                }
            } 
        }

        private string _pastryTemplateName;
        private string PastryTemplateName
        {
            get { return _pastryTemplateName; }
            set
            {
                if (_pastryTemplateName != value)
                {
                    _pastryTemplateName = value;
                    OnPropertyChanged(nameof(PastryTemplateName));
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        #endregion

        public RelayCommand PrintFrontList {  get; set; }
        public RelayCommand PrintBackList { get; set; }
        public RelayCommand PrintWsAgg { get; set; }
        public RelayCommand PrintWsBreakDown { get; set; }
        public RelayCommand SetPieTemplate { get; set; }
        public RelayCommand SetPastryTemplate { get; set; }
        public RelayCommand OpenTemplateListView { get; set; }

        public ReportViewModel()
        {
            ReportDirector rd = new ReportDirector();

            PrintFrontList = new RelayCommand(o => { if(IsValidDate()) rd.CreateFrontList(StartDate); });
            PrintBackList = new RelayCommand(o => { if (IsValidDate()) rd.CreateBackList(StartDate, EndDate); });
            PrintWsAgg = new RelayCommand(o => { if (IsValidDate()) rd.CreateWsDay(StartDate); });
            PrintWsBreakDown = new RelayCommand(o => { if (IsValidDate()) rd.CreateWsDayName(StartDate); });
            SetPieTemplate = new RelayCommand(o => { StPieTempCmd(); });
            SetPastryTemplate = new RelayCommand(o => { StPastTempCmd(); });
            OpenTemplateListView = new RelayCommand(o => { TmpltCmd(); });

            RetailFilter = true;
            SquareFilter = true;
            WholesaleFilter = true;
            SquareFilter = true;
            EzCaterFilter = true;
        }


        private void StPieTempCmd()
        {

        }
        private void StPastTempCmd()
        {

        }
        private void TmpltCmd()
        {

        }
        private bool IsValidDate()
        {
            if (StartDate == default) { return false; }
            return true;
        }
    }
}
