using Petsi.Interfaces;
using Petsi.Services;
using POMT_WPF.Core;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateListViewModel : ViewModelBase, ITemplateService
    {
        public ObservableCollection<string> TemplateNames { get; set; }
        private ReportTemplateService _templateService;
        RelayCommand RemoveTemplate { get; set; }
        RelayCommand ViewTemplate { get; set; }
        RelayCommand GoBack { get; set; }

        public TemplateListViewModel()
        {
            _templateService = ReportTemplateService.Instance();
            _templateService.Subscribe(this);
            TemplateNames = new ObservableCollection<string>(_templateService.GetTemplateNames());

            RemoveTemplate = new RelayCommand(o => { });
            ViewTemplate = new RelayCommand(o => { });
            GoBack = new RelayCommand(o => { });
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
            if(o is string)
            {

            }
        }
        private void ViewTemplateCmd(object? o)
        {
            if(o == null)
            {

            }
            else if (o is string)
            {

            }
        }
    }
}
