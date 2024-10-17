using Petsi.Events.ItemEvents;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    class LabelItemViewModel : ViewModelBase
    {
        private LabelItemWindow _view;

        #region Props

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    _itemName = value;
                    OnPropertyChanged(nameof(ItemName)); 
                } 
            }
        }

        private string _pieFile;
        public string PieFile
        {
            get { return _pieFile; }
            set
            {
                if (_pieFile != value)
                {
                    _pieFile = value;
                    OnPropertyChanged(nameof(PieFile));
                }
            }
        }

        private string _cutieFile;
        public string CutieFile
        {
            get { return _cutieFile; }
            set
            {
                if (_cutieFile != value)
                {
                    _cutieFile = value;
                    OnPropertyChanged(nameof(CutieFile));
                }
            }
        }

        #endregion

        public RelayCommand SetPieLabel {  get; set; }
        public RelayCommand SetCutieLabel {  get; set; }
        public RelayCommand ClearPieLabel {  get; set; }
        public RelayCommand ClearCutieLabel {  get; set; }
        public RelayCommand Done {  get; set; }
        public RelayCommand Cancel {  get; set; }

        private List<CatalogItemPetsi> ExistingItems;
        private bool _isNew;

        public LabelItemViewModel(CatalogItemPetsi? item, LabelItemWindow view, List<CatalogItemPetsi> existingItems) 
        {
            _view = view;
            ExistingItems = existingItems;

            if (item != null)
            {
                ItemName = item.ItemName;
                CutieFile = item.CutieLabelFilePath;
                PieFile = item.StandardLabelFilePath;
            }
            else
            {
                _isNew = false;
            }

            SetPieLabel = new RelayCommand(o => { SetPieCommand(); } );
            SetCutieLabel = new RelayCommand(o => { SetCutieCommand(); } );
            ClearPieLabel = new RelayCommand(o => { ClearPieCommand(); } );
            ClearCutieLabel = new RelayCommand(o => { ClearCutieCommand(); } );
            Done = new RelayCommand(o => { DoneCommand(); } );
            Cancel = new RelayCommand(o => { _view.Close(); } );
        }

        private void SetPieCommand()
        {
            //string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_PIE_LBL_PATH);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                //fileDialog.InitialDirectory = labelsFilepath + "\\Pie";
                fileDialog.InitialDirectory = labelsFilepath;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    PieFile = System.IO.Path.GetFileName(fileDialog.FileName);
                }
            }
        }
        private void SetCutieCommand()
        {
            //string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_CUTIE_LBL_PATH);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Cuties";
                fileDialog.InitialDirectory = labelsFilepath;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    CutieFile = System.IO.Path.GetFileName(fileDialog.FileName);
                }
            }
        }

        private void ClearPieCommand(){ PieFile = null; }

        private void ClearCutieCommand(){CutieFile = null; }

        private void DoneCommand() 
        {
            CatalogItemPetsi item = new CatalogItemPetsi();
            if(ValidateItem(out item))
            {
                ObsCatalogModelSingleton.Instance.AddItem(item);
                LabelItemViewEvents.RaiseSaveSuccessEvents();
            } 
        }

        /// <summary>
        /// Validates that the ItemName exists in the catalog and prepares an object to be sent to the model as a modification.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ValidateItem(out CatalogItemPetsi item)
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            
            item = new CatalogItemPetsi(cs.GetCatalogItem(ItemName));
            if (_isNew)
            {
                foreach (CatalogItemPetsi existingItem in ExistingItems)
                {
                    if (existingItem.ItemName == ItemName)
                    {
                        return false;
                    }
                }
            }
            if (item == null) 
            {
                SystemLogger.LogError($"label item validation argument not found:{ItemName}", "LabelItemViewModel ValidateItem()");
                return false;
            }

            item.StandardLabelFilePath = PieFile;
            item.CutieLabelFilePath = CutieFile;

            return true;
        }
    }
}
