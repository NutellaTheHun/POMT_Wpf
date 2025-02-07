﻿using Petsi.Reports;
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

        private bool _farmer;
        public bool FarmerFilter
        {
            get { return _farmer; }
            set
            {
                if (_farmer != value)
                {
                    _farmer = value;
                    OnPropertyChanged(nameof(FarmerFilter));
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
                    config.SetVariable(Identifiers.SETTING_PIE_TEMPLATE, PieTemplateName);
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
                    config.SetVariable(Identifiers.SETTING_PASTRY_TEMPLATE, PastryTemplateName);
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
                    if(value != null) 
                    {
                        _endDate = (DateTime)value;
                    }
                    else
                    {
                        _endDate = null;
                    }
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        private bool _isPrint;
        public bool IsPrint
        {
            get { return _isPrint; }
            set
            {
                if (_isPrint != value)
                {
                    _isPrint = value;
                    OnPropertyChanged(nameof(IsPrint));
                }
            }
        }
        private bool _isExport;
        public bool IsExport
        {
            get { return _isExport; }
            set
            {
                if (_isExport != value)
                {
                    _isExport = value;
                    OnPropertyChanged(nameof(IsExport));
                }
            }
        }
        #endregion
        public RelayCommand PrintFrontList {  get; set; }
        //public RelayCommand PrintBackList { get; set; } //not used, evolved to use PrintPieBackList() and PrintPastryBackList()
        public RelayCommand PrintPieBackList { get; set; }
        public RelayCommand PrintPastryBackList { get; set; }
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

            PrintFrontList = new RelayCommand(o => { if (IsValidDate())      rd.RequestFrontList(GetReportConfig()); });
            PrintPieBackList = new RelayCommand(o => { if (IsValidDate())    rd.RequestPieBackList(GetReportConfig()); });
            PrintPastryBackList = new RelayCommand(o => { if (IsValidDate()) rd.RequestPastryBackList(GetReportConfig()); });
            //PrintBackList = new RelayCommand(o => { if (IsValidDate())       rd.CreateBackList(GetReportConfig()); });
            PrintWsAgg = new RelayCommand(o => { if (IsValidDate())          rd.RequestWsDay(GetReportConfig()); });
            PrintWsBreakDown = new RelayCommand(o => { if (IsValidDate())    rd.RequestWsDayName(GetReportConfig()); });
            SetPieTemplate = new RelayCommand(o => { StPieTempCmd(); });
            SetPastryTemplate = new RelayCommand(o => { StPastTempCmd(); });
            OpenTemplateListView = new RelayCommand(o => { MainViewModel.Instance().OpenTemplateListView(false); });

            RetailFilter = true;
            SquareFilter = true;
            WholesaleFilter = true;
            SpecialFilter = true;
            //EzCater ALWAYS FALSE
            EzCaterFilter = false;
            FarmerFilter = true;

            IsPrint = true;
            IsExport = false;

            StartDate = null;
            EndDate = null;
        }

        private ReportConfig GetReportConfig()
        {
            return new ReportConfig(StartDate, EndDate,
                IsPrint, IsExport,
                RetailFilter, SquareFilter,
                WholesaleFilter, SpecialFilter,
                EzCaterFilter, FarmerFilter,
                null,
                null);
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
