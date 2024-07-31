using Petsi.Interfaces;
using Petsi.Services;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateListViewModel : ViewModelBase, ITemplateService
    {
        public ObservableCollection<string> TemplateNames { get; set; }

        private ReportTemplateService _templateService;

        public RelayCommand RemoveTemplate { get; set; }
        public RelayCommand ViewTemplate { get; set; }
        public RelayCommand CreateTemplate { get; set; }
        public RelayCommand GoBack { get; set; }


        public bool IsFromSettingsVM;

        public TemplateListViewModel(/*bool isFromSettingsVM*/)
        {
            //_isFromSettingsVM = isFromSettingsVM;
            _templateService = ReportTemplateService.Instance();
            _templateService.Subscribe(this);
            TemplateNames = new ObservableCollection<string>(_templateService.GetTemplateNames());

            RemoveTemplate = new RelayCommand(o => { RemoveTemplateCmd(o); });
            ViewTemplate = new RelayCommand(o => { ViewTemplateCmd(o); });
            CreateTemplate = new RelayCommand(o => {CreateTemplateCmd();});
            GoBack = new RelayCommand(o => { MainViewModel.Instance().BackTmpltLstView(IsFromSettingsVM); });
        }
        public void Update()
        {
            var newTemplateNames = _templateService.GetTemplateNames();
            TemplateNames.Clear();
            foreach (var name in newTemplateNames)
            {
                TemplateNames.Add(name);
            }
        }
        private void RemoveTemplateCmd(object o)
        {
            ConfirmationWindow confirmationWindow = new ConfirmationWindow(null);
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                ReportTemplateService rts = ReportTemplateService.Instance();
                rts.RemoveTemplate((string)o);
            }
        }
        private void ViewTemplateCmd(object? o)
        {
            TemplateItemViewWindow view;
            if(o is string)
            {
                List<BackListItem> items = _templateService.GetTemplate((string)o);
                view = new TemplateItemViewWindow(items, (string)o);
                view.Show();
            }
        }
        private void CreateTemplateCmd()
        {
            TemplateItemViewWindow view;
                view = new TemplateItemViewWindow(null, null);
                view.Show();
        }
    }
}
