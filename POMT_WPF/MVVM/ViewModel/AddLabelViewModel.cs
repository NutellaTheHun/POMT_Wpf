using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    public class AddLabelViewModel : ViewModelBase
    {
        CatalogService cs;
        CatalogItemPetsi item;
        public string _itemName;
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
        string _standardFilepath;
        public string StandardFilePath
        {
            get { return _standardFilepath; }
            set
            {
                if (_standardFilepath != value)
                {
                    _standardFilepath = value;
                    OnPropertyChanged(nameof(StandardFilePath));
                }
            }
        }
        string _cutieFilepath;
        public string CutieFilePath
        {
            get { return _cutieFilepath; }
            set
            {
                if (_cutieFilepath != value)
                {
                    _cutieFilepath = value;
                    OnPropertyChanged(nameof(CutieFilePath));
                }
            }
        }

        public AddLabelViewModel(CatalogItemPetsi? item)
        {
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if(item != null)
            {
                ItemName = item.ItemName;
                StandardFilePath = item.StandardLabelFilePath;
                CutieFilePath = item.CutieLabelFilePath;
            }
        }

        public void SetCutieFile()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Cuties";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    CutieFilePath = fileDialog.FileName;
                }
            }
        }

        public void SetStandardLabelFile()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Pie";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    StandardFilePath = fileDialog.FileName;
                }
            }
        }

        public bool ValidateItem(string text)
        {
            if(text == null) { return false; }  

            if (cs.NameExists(text))
            {
                ItemName = text;
                return true;
            }
            return false;
        }

        public List<CatalogItemPetsi> GetItemMatchResults(string itemName)
        {
            return cs.GetItemNameValidationResults(itemName);
        }

        public bool ValidateFields()
        {
            if(!ValidateItem(ItemName)){ return false; }
            item = cs.GetCatalogItem(ItemName);
            if ( (StandardFilePath == "" || StandardFilePath == null)  && (CutieFilePath == "" || CutieFilePath == null)) { return false; }
            return true;
        }

        public void UpdateCatalogItem()
        {
            item.StandardLabelFilePath = StandardFilePath;
            item.CutieLabelFilePath = CutieFilePath;
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            cmp.UpdateModel();
        }

        /// <summary>
        /// Checks if the target item already has assigned files. If there's already associated labels with the item, then it already exists in
        /// the lists of labels and is therfor a duplicate.
        /// </summary>
        /// <returns></returns>
        public bool HasDuplicate()
        {
            if (item != null)
            {
                if (item.StandardLabelFilePath != null || item.CutieLabelFilePath != null) { return true; }
            }
            return false;
        }

        public void ClearStandardLabel()
        {
            StandardFilePath = null;
        }

        public void ClearCutieLabel()
        {
            CutieFilePath = null;
        }
    }
}
