using Petsi.Reports;
using Petsi.Services;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;

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
        public string PieTemplateName 
        {
            get { return _pieTemplateName; }
            set
            {
                if( _pieTemplateName != value)
                {
                    _pieTemplateName = value;
                    config.SetValue(Identifiers.SETTING_PIE_TEMPLATE, PieTemplateName);
                    OnPropertyChanged(nameof(PieTemplateName));
                }
            } 
        }

        private string _pastryTemplateName;
        public string PastryTemplateName
        {
            get { return _pastryTemplateName; }
            set
            {
                if (_pastryTemplateName != value)
                {
                    _pastryTemplateName = value;
                    config.SetValue(Identifiers.SETTING_PASTRY_TEMPLATE, PastryTemplateName);
                    OnPropertyChanged(nameof(PastryTemplateName));
                }
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
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
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = (DateTime)value;
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

        private PetsiConfig config = PetsiConfig.GetInstance();

        public ReportViewModel()
        {
            //PetsiConfig config 
            PieTemplateName = config.GetVariable(Identifiers.SETTING_PIE_TEMPLATE);
            PastryTemplateName = config.GetVariable(Identifiers.SETTING_PASTRY_TEMPLATE);

            ReportDirector rd = new ReportDirector();

            PrintFrontList = new RelayCommand(o => { if(IsValidDate()) rd.CreateFrontList(StartDate, RetailFilter, SquareFilter, WholesaleFilter, SpecialFilter, EzCaterFilter); });
            PrintBackList = new RelayCommand(o => { if (IsValidDate()) rd.CreateBackList(StartDate, EndDate, RetailFilter, SquareFilter, WholesaleFilter, SpecialFilter, EzCaterFilter); });
            PrintWsAgg = new RelayCommand(o => { if (IsValidDate()) rd.CreateWsDay(StartDate, RetailFilter, SquareFilter, WholesaleFilter, SpecialFilter, EzCaterFilter); });
            PrintWsBreakDown = new RelayCommand(o => { if (IsValidDate()) rd.CreateWsDayName(StartDate, RetailFilter, SquareFilter, WholesaleFilter, SpecialFilter, EzCaterFilter); });
            SetPieTemplate = new RelayCommand(o => { StPieTempCmd(); });
            SetPastryTemplate = new RelayCommand(o => { StPastTempCmd(); });
            OpenTemplateListView = new RelayCommand(o => { MainViewModel.Instance().OpenTemplateListView(false); });

            RetailFilter = true;
            SquareFilter = true;
            WholesaleFilter = true;
            SpecialFilter = true;
            EzCaterFilter = true;

            StartDate = null;
            EndDate = null;
        }
        private void StPieTempCmd()
        {
            SetSettingsVariableWindow win = new SetSettingsVariableWindow("Select Pie Template", ReportTemplateService.Instance().GetTemplateNames());
            win.ShowDialog();
            if (win.selectionMade)
            {
                PieTemplateName = win.VariableSelection;
            }
        }
        private void StPastTempCmd()
        {
            SetSettingsVariableWindow win = new SetSettingsVariableWindow("Select Pastry Template", ReportTemplateService.Instance().GetTemplateNames());
            win.ShowDialog();
            if (win.selectionMade)
            {
                PastryTemplateName = win.VariableSelection;
            }
        }

        private bool IsValidDate()
        {
            if (StartDate == default) { return false; }
            return true;
        }
    }
}
