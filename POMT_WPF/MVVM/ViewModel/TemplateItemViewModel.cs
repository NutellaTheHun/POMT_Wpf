
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateItemViewModel : ViewModelBase
    {
        private ReportTemplateService _templateService;
        private CatalogService _catalogService;
        private TemplateItemViewWindow _view;

        public ObservableCollection<BackListItem> TemplateItems { get; set; }

        private string _templateName;
        public string TemplateName
        {
            get { return _templateName; }
            set
            {
                if (_templateName != value)
                {
                    _templateName = value;
                    OnPropertyChanged(nameof(TemplateName));
                }
            }
        }
        public RelayCommand Close {  get; set; }
        public RelayCommand Done {  get; set; }
        public RelayCommand AddItem {  get; set; }
        public RelayCommand RemoveItem {  get; set; }
        public RelayCommand MoveItemUp {  get; set; }
        public RelayCommand MoveItemDown {  get; set; }

        public TemplateItemViewModel(string? templateName, TemplateItemViewWindow view)
        {
            _view = view;
            _templateService = ReportTemplateService.Instance();
            _catalogService = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if (templateName != null)
            {
                List<BackListItem> templateItems = _templateService.GetTemplate(templateName);
                if (templateItems != null)
                {
                    TemplateName = templateName;
                    TemplateItems = new ObservableCollection<BackListItem>(templateItems);
                }
            }

            Close = new RelayCommand(o => { });
            Done = new RelayCommand(o => { });
            AddItem = new RelayCommand(o => { });
            RemoveItem = new RelayCommand(o => { });
            MoveItemUp = new RelayCommand(o => { });
            MoveItemDown = new RelayCommand(o => { });
        }

    }
}
