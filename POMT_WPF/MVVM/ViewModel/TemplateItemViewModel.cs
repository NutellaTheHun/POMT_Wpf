using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateItemViewModel : ViewModelBase
    {
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

        public TemplateItemViewModel(List<BackListItem>? inputList, string? templateName, TemplateItemViewWindow view)
        {
            _view = view;

            if (inputList != null)
            {
                TemplateItems = new ObservableCollection<BackListItem>(inputList);
                TemplateName = templateName;
            }

            Close = new RelayCommand(o => { _view.CloseWin(); });
            Done = new RelayCommand(o => { DoneCmd(); });
            AddItem = new RelayCommand(o => { if(TemplateItems != null) TemplateItems.Add(new BackListItem()); });
            RemoveItem = new RelayCommand(o => { if (TemplateItems != null) TemplateItems.Remove((BackListItem)o); });
            MoveItemUp = new RelayCommand(o => { SwapItemUpCmd(o); });
            MoveItemDown = new RelayCommand(o => { SawpItemDownCmd(o); });
        }

        private void DoneCmd()
        {
            //validate
            //save
            _view.CloseWin();
        }

        private void SwapItemUpCmd(object targetItem)
        {
            foreach (BackListItem item in TemplateItems)
            {
                if (item == (BackListItem)targetItem)
                {
                    int index = TemplateItems.IndexOf(item);
                    if (index != 0)
                    {
                        List<BackListItem> tempList = TemplateItems.ToList();
                        BackListItem temp = tempList[index - 1];
                        tempList[index - 1] = tempList[index];
                        tempList[index] = temp;

                        TemplateItems.Clear();
                        foreach (BackListItem name in tempList)
                        {
                            TemplateItems.Add(name);
                        }
                        return;
                    }
                }
            }
        }
        private void SawpItemDownCmd(object targetItem)
        {
            foreach (BackListItem item in TemplateItems)
            {
                if (item == (BackListItem)targetItem)
                {
                    int index = TemplateItems.IndexOf(item);
                    if (index != TemplateItems.Count - 1)
                    {
                        List<BackListItem> tempList = TemplateItems.ToList();
                        BackListItem temp = tempList[index + 1];
                        tempList[index + 1] = TemplateItems[index];
                        tempList[index] = temp;

                        TemplateItems.Clear();
                        foreach (BackListItem name in tempList)
                        {
                            TemplateItems.Add(name);
                        }
                        return;
                    }
                }
            }
        }
    }
}
