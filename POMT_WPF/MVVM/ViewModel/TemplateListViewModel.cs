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
        public RelayCommand GoBack { get; set; }


        private bool _isFromSettingsVM;

        public TemplateListViewModel(bool isFromSettingsVM)
        {
            _isFromSettingsVM = isFromSettingsVM;
            _templateService = ReportTemplateService.Instance();
            _templateService.Subscribe(this);
            TemplateNames = new ObservableCollection<string>(_templateService.GetTemplateNames());

            RemoveTemplate = new RelayCommand(o => { RemoveTemplateCmd(o); });
            ViewTemplate = new RelayCommand(o => { ViewTemplateCmd(o); });
            GoBack = new RelayCommand(o => { MainViewModel.Instance().BackTmpltLstView(_isFromSettingsVM); });
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
            /*
            ConfirmationWindow confirmationWindow = new ConfirmationWindow();
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                _templateService.RemoveTemplate((string)o);
            }
            */
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
            else if (o == null)
            {
                view = new TemplateItemViewWindow(null, null);
                view.Show();
            }
        }
    }
}
