using Petsi.Utils;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Properties
        private string _labelPrinter; //config, getPrinters()
        public string LabelPrinter
        {
            get { return _labelPrinter; }
            set
            {
                if (_labelPrinter != value)
                {
                    _labelPrinter = value;
                    config.SetValue(Identifiers.SETTING_LABEL_PRINTER, LabelPrinter);
                    OnPropertyChanged(nameof(LabelPrinter));
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
                    config.SetValue(Identifiers.SETTING_STD_PRINTER, StandardPrinter);                 
                    OnPropertyChanged(nameof(StandardPrinter));
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
                    config.SetValue(Identifiers.SETTING_LABEL_FP, LabelsFilepath);
                    OnPropertyChanged(nameof(LabelsFilepath));
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
                    config.SetValue(Identifiers.SETTING_PIE_TEMPLATE, PieTemplate);
                    OnPropertyChanged(nameof(PieTemplate));
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
                    _pastryTemplate = value;
                    config.SetValue(Identifiers.SETTING_PASTRY_TEMPLATE, PastryTemplate);
                    OnPropertyChanged(nameof(PastryTemplate));
                }
            }
        }
        #endregion

        PetsiConfig config;
        public SettingsViewModel()
        {
            config = PetsiConfig.GetInstance();
            LabelPrinter = config.GetVariable(Identifiers.SETTING_LABEL_PRINTER);
            StandardPrinter = config.GetVariable(Identifiers.SETTING_STD_PRINTER);
            LabelsFilepath = config.GetVariable(Identifiers.SETTING_LABEL_FP);
            NumberOfDays = config.GetVariable(Identifiers.SETTING_DAYNUM);
            PieTemplate = config.GetVariable(Identifiers.SETTING_PIE_TEMPLATE);
            PastryTemplate = config.GetVariable(Identifiers.SETTING_PASTRY_TEMPLATE);
        }

        public void SetLabelsFilePath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select folder containing pie and cutie label folders";
            string sSelectedPath = "";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                sSelectedPath = fbd.SelectedPath;
            }
            if(sSelectedPath != "")
            {
                LabelsFilepath = sSelectedPath;
            }
        }

        public void SetPieTemplate(string templateName)
        {
            PieTemplate = templateName;
        }

        public void SetPastryTemplate(string templateName)
        {
            PastryTemplate = templateName;
        }

        public void SetStandardPrinter(string printerName)
        {
            StandardPrinter = printerName;
        }

        public void SetLabelPrinter(string printerName)
        {
            LabelPrinter = printerName;
        }
    }
}
