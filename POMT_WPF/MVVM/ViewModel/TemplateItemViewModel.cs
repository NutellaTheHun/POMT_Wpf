﻿using Petsi.Events.ItemEvents;
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
        public RelayCommand Save {  get; set; }
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
            else
            {
                TemplateItems = new ObservableCollection<BackListItem>();
            }

            Close = new RelayCommand(o => { _view.Close(); });
            Save = new RelayCommand(o => { SaveCmd(); });
            AddItem = new RelayCommand(o => { if(TemplateItems != null) TemplateItems.Add(new BackListItem()); });
            RemoveItem = new RelayCommand(o => { if (TemplateItems != null) TemplateItems.Remove((BackListItem)o); });
            MoveItemUp = new RelayCommand(o => { SwapItemUpCmd(o); });
            MoveItemDown = new RelayCommand(o => { SwapItemDownCmd(o); });
        }

        private void SaveCmd()
        {
            if (IsValid())
            {
                SaveTemplate();
                TemplateItemViewEvents.OnSaveSuccessful(); 
            }         
        }

        /// <summary>
        /// Validates the template name and each back list item, also assigns a catalog Id based on the item name.
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        private bool IsValid()
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);

            if (TemplateName == "" || TemplateName == null) { return false; }
            if (TemplateItems.Count == 0) { return false; };
            foreach (BackListItem item in TemplateItems)
            {
                string id = cs.GetCatalogObjectId(item.ItemName);
                if (id == "")
                {
                    GeneralErrorWindow errWin = new GeneralErrorWindow("item: " + item.ItemName + " cound not be validated, template was not saved.");
                    errWin.Owner = System.Windows.Application.Current.MainWindow;
                    errWin.Show();
                    return false;
                }
                item.CatalogObjId = id;

                if (item.ItemName == "" || item.ItemName == null) { return false; }
                //if (item.CatalogObjId == "" || item.CatalogObjId == null) { return false; }
                if (item.PageDisplayName == "" || item.PageDisplayName == null) { return false; }
            }
            return true;
        }

        private void SaveTemplate() //IMPLEMENT
        {
            ReportTemplateService rts = ReportTemplateService.Instance();
            List<BackListItem> backListItems = TemplateItems.ToList();
            rts.AddTemplate((TemplateName, backListItems));
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
        private void SwapItemDownCmd(object targetItem)
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
