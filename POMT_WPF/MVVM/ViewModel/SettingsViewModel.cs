using Petsi.Services;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Properties
        private string _labelPrinter;
        public string LabelPrinter
        {
            get { return _labelPrinter; }
            set
            {
                if (_labelPrinter != value)
                {
                    _labelPrinter = value;
                    config.SetVariable(Identifiers.SETTING_LABEL_PRINTER, LabelPrinter);
                    OnPropertyChanged(nameof(LabelPrinter));
                }
            }
        }

        private string _standardPrinter;
        public string StandardPrinter
        {
            get { return _standardPrinter; }
            set
            {
                if(_standardPrinter != value)
                {
                    _standardPrinter = value;
                    config.SetVariable(Identifiers.SETTING_STD_PRINTER, StandardPrinter);                 
                    OnPropertyChanged(nameof(StandardPrinter));
                }
            }
        }

        private string _pieLabelsFilepath;
        public string PieLabelsFilepath
        {
            get { return _pieLabelsFilepath; }
            set
            {
                if (_pieLabelsFilepath != value)
                {
                    _pieLabelsFilepath = value;
                    config.SetVariable(Identifiers.SETTING_PIE_LBL_PATH, PieLabelsFilepath);
                    OnPropertyChanged(nameof(PieLabelsFilepath));
                }
            }
        }

        private string _environementFilepath;
        public string EnvironmentFilepath
        {
            get { return _environementFilepath; }
            set
            {
                if (_environementFilepath != value)
                {
                    _environementFilepath = value;
                    config.SetVariable(Identifiers.SETTING_ENVIRON_PATH, EnvironmentFilepath);
                    OnPropertyChanged(nameof(EnvironmentFilepath));
                }
            }
        }

        private string _reportExportFilepath;
        public string ReportExportFilepath
        {
            get { return _reportExportFilepath; }
            set
            {
                if (_reportExportFilepath != value)
                {
                    _reportExportFilepath = value;
                    config.SetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH, ReportExportFilepath);
                    OnPropertyChanged(nameof(ReportExportFilepath));
                }
            }
        }

        private string _cutieLabelsFilepath;
        public string CutieLabelsFilepath
        {
            get { return _cutieLabelsFilepath; }
            set
            {
                if (_cutieLabelsFilepath != value)
                {
                    _cutieLabelsFilepath = value;
                    config.SetVariable(Identifiers.SETTING_CUTIE_LBL_PATH, CutieLabelsFilepath);
                    OnPropertyChanged(nameof(CutieLabelsFilepath));
                }
            }
        }

        private string _numberOfDays;
        public string NumberOfDays
        {
            get { return _numberOfDays; }
            set
            {
                if (_numberOfDays != value)
                {
                    _numberOfDays = value;
                    config.SetVariable(Identifiers.SETTING_DAYNUM, NumberOfDays);
                    OnPropertyChanged(nameof(_numberOfDays));
                }
            }
        }

        private string _pieTemplate;
        public string PieTemplate
        {
            get { return _pieTemplate; }
            set
            {
                if (_pieTemplate != value)
                {
                    _pieTemplate = value;
                    config.SetVariable(Identifiers.SETTING_PIE_TEMPLATE, PieTemplate);
                    OnPropertyChanged(nameof(PieTemplate));
                }
            }
        }

        private string _pastryTemplate;
        public string PastryTemplate
        {
            get { return _pastryTemplate; }
            set
            {
                if (_pastryTemplate != value)
                {
                    _pastryTemplate = value;
                    config.SetVariable(Identifiers.SETTING_PASTRY_TEMPLATE, PastryTemplate);
                    OnPropertyChanged(nameof(PastryTemplate));
                }
            }
        }
        #endregion

        public RelayCommand SetLabelPrinterCommand { get; set; }
        public RelayCommand SetStandardPrinterCommand { get; set; }
        public RelayCommand SetPieLabelFilePathCommand { get; set; }
        public RelayCommand SetCutieLabelFilePathCommand { get; set; }
        public RelayCommand SetEnvironmentFilePathCommand { get; set; }
        public RelayCommand SetReportExportFilePathCommand { get; set; }
        public RelayCommand SetPieTemplateCommand { get; set; }
        public RelayCommand SetPastryTemplateCommand { get; set; }
        public RelayCommand ConfigureLabelsCommand { get; set; }
        public RelayCommand ConfigureTemplatesCommand { get; set; }

        PetsiConfig config;
        public SettingsViewModel()
        {
            config = PetsiConfig.GetInstance();
            LabelPrinter = config.GetVariable(Identifiers.SETTING_LABEL_PRINTER);
            StandardPrinter = config.GetVariable(Identifiers.SETTING_STD_PRINTER);
            PieLabelsFilepath = config.GetVariable(Identifiers.SETTING_PIE_LBL_PATH);
            CutieLabelsFilepath = config.GetVariable(Identifiers.SETTING_CUTIE_LBL_PATH);
            NumberOfDays = config.GetVariable(Identifiers.SETTING_DAYNUM);
            PieTemplate = config.GetVariable(Identifiers.SETTING_PIE_TEMPLATE);
            PastryTemplate = config.GetVariable(Identifiers.SETTING_PASTRY_TEMPLATE);

            SetLabelPrinterCommand = new RelayCommand(o => { SetLabelPrinter(); });
            SetStandardPrinterCommand = new RelayCommand(o => { SetStandardPrinter(); });
            SetPieLabelFilePathCommand = new RelayCommand(o => { SetFilePath(Identifiers.SETTING_PIE_LBL_PATH); });
            SetCutieLabelFilePathCommand = new RelayCommand(o => { SetFilePath(Identifiers.SETTING_CUTIE_LBL_PATH); });
            SetEnvironmentFilePathCommand = new RelayCommand(o => { SetFilePath(Identifiers.SETTING_ENVIRON_PATH);  });
            SetReportExportFilePathCommand = new RelayCommand(o => { SetFilePath(Identifiers.SETTING_REPORT_EXPORT_PATH);  });
            SetPieTemplateCommand = new RelayCommand(o => { SetPieTemplate(); });
            SetPastryTemplateCommand = new RelayCommand(o => { SetPastryTemplate(); });
            ConfigureLabelsCommand = new RelayCommand(o => { MainViewModel.Instance().OpenConfigureLabelView(true); });
            ConfigureTemplatesCommand = new RelayCommand(o => { MainViewModel.Instance().OpenTemplateListView(true); });
        }

        public void SetFilePath(string pieFp)
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
                if(pieFp == Identifiers.SETTING_PIE_LBL_PATH)
                {
                    PieLabelsFilepath = sSelectedPath;
                }
                else if(pieFp == Identifiers.SETTING_CUTIE_LBL_PATH)
                {
                    CutieLabelsFilepath = sSelectedPath;
                }
                else if (pieFp == Identifiers.SETTING_ENVIRON_PATH)
                {
                    EnvironmentFilepath = sSelectedPath;
                }
                else if (pieFp == Identifiers.SETTING_REPORT_EXPORT_PATH)
                {
                    ReportExportFilepath = sSelectedPath;
                }
            }
        }
        private void SetStandardPrinter()
        {
            SetSettingsVariableWindow win = new SetSettingsVariableWindow("Select Standard Printer" ,GetPrinterNames());
            win.ShowDialog();
            if (win.selectionMade)
            {
                StandardPrinter = win.VariableSelection;
            }
        }
        private void SetLabelPrinter()
        {
            SetSettingsVariableWindow win = new SetSettingsVariableWindow("Select Label Printer", GetPrinterNames());
            win.ShowDialog();
            if (win.selectionMade)
            {
                LabelPrinter = win.VariableSelection;
            }
        }
        private void SetPieTemplate()
        {
            SetSettingsVariableWindow win = new SetSettingsVariableWindow("Select Pie Template", ReportTemplateService.Instance().GetTemplateNames());
            win.ShowDialog();
            if (win.selectionMade)
            {
                PieTemplate = win.VariableSelection;
            }
        }
        private void SetPastryTemplate()
        {
            SetSettingsVariableWindow win = new SetSettingsVariableWindow("Select Pastry Template", ReportTemplateService.Instance().GetTemplateNames());
            win.ShowDialog();
            if (win.selectionMade)
            {
                PastryTemplate = win.VariableSelection;
            }
        }
        List<string> GetPrinterNames()
        {
            List<string> result = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                result.Add(printer);
            }
            return result;
        }
    }
}
