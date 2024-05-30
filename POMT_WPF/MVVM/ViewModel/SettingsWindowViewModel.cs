using Petsi.Utils;


namespace POMT_WPF.MVVM.ViewModel
{
    class SettingsWindowViewModel : ViewModelBase
    {
        private string _rolloPrinter; //config, getPrinters()
        public string RolloPrinter
        {
            get { return _rolloPrinter; }
            set
            {
                if (_rolloPrinter != value)
                {
                    _rolloPrinter = value;
                    OnPropertyChanged(nameof(_rolloPrinter));
                }
            }
        }

        private string _standardPrinter;  //config, getPrinters()
        public string StandardPrinter
        {
            get { return _standardPrinter; }
            set
            {
                if(_standardPrinter != value)
                {
                    _standardPrinter = value;
                    OnPropertyChanged(nameof(_standardPrinter));
                }
            }
        }

        private string _labelsFilepath;  //config
        public string LabelsFilepath
        {
            get { return _labelsFilepath; }
            set
            {
                if (_labelsFilepath != value)
                {
                    _labelsFilepath = value;
                    OnPropertyChanged(nameof(_labelsFilepath));
                }
            }
        }

        private string _numberOfDays;  //config
        public string NumberOfDays
        {
            get { return _numberOfDays; }
            set
            {
                if (_numberOfDays != value)
                {
                    _numberOfDays = value;
                    OnPropertyChanged(nameof(_numberOfDays));
                }
            }
        }

        private string _pieTemplate;  //config
        public string PieTemplate
        {
            get { return _pieTemplate; }
            set
            {
                if (_pieTemplate != value)
                {
                    _pieTemplate = value;
                    OnPropertyChanged(nameof(_pieTemplate));
                }
            }
        }

        private string _pastryTemplate;  //config
        public string PastryTemplate
        {
            get { return _pastryTemplate; }
            set
            {
                if (_pastryTemplate != value)
                {
                    _standardPrinter = value;
                    OnPropertyChanged(nameof(_pastryTemplate));
                }
            }
        }
        PetsiConfig config;

        public SettingsWindowViewModel()
        {
            config = PetsiConfig.GetInstance();
            _rolloPrinter = config.GetVariable(Identifiers.SETTING_ROLLO_PRINTER);
            StandardPrinter = config.GetVariable(Identifiers.SETTING_STD_PRINTER);
            _labelsFilepath = config.GetFilepath(Identifiers.SETTING_LABEL_FP);
            _numberOfDays = config.GetVariable(Identifiers.SETTING_DAYNUM);
            _pieTemplate = config.GetVariable(Identifiers.SETTING_PIE_TEMPLATE);
            _pastryTemplate = config.GetVariable(Identifiers.SETTING_PASTRY_TEMPLATE);
        }
    }
}
