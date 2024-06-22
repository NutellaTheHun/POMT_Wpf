using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogItemViewModel : ViewModelBase
    {

        #region props

        private bool _isSmall;
        public bool IsSmall
        {
            get { return _isSmall; }
            set
            {
                if (_isSmall != value)
                {
                    _isSmall = value;
                    OnPropertyChanged(nameof(IsSmall));
                }
            }
        }//******

        private bool _isMedium;
        public bool IsMedium
        {
            get { return _isMedium; }
            set
            {
                if (_isMedium != value)
                {
                    _isMedium = value;
                    OnPropertyChanged(nameof(IsMedium));
                }
            }
        }//******

        private bool _isLarge;
        public bool IsLarge
        {
            get { return _isLarge; }
            set
            {
                if (_isLarge != value)
                {
                    _isLarge = value;
                    OnPropertyChanged(nameof(IsLarge));
                }
            }
        }//******

        private bool _isRegular;
        public bool IsRegular
        {
            get { return _isRegular; }
            set
            {
                if (_isRegular != value)
                {
                    _isRegular = value;
                    OnPropertyChanged(nameof(IsRegular));
                }
            }
        }//******

        private bool _isCutie;
        public bool IsCutie
        {
            get { return _isCutie; }
            set
            {
                if (_isCutie != value)
                {
                    _isCutie = value;
                    OnPropertyChanged(nameof(IsCutie));
                }
            }
        }//******

        private bool _isPOTM;
        public bool IsPOTM
        {
            get { return _isPOTM; }
            set
            {
                if (_isPOTM != value)
                {
                    _isPOTM = value;
                    OnPropertyChanged(nameof(IsPOTM));
                }
            }
        }//******

        public ObservableCollection<string> NaturalNames;//******

        public ObservableCollection<string> CategoryNames;
        
        private CatalogItemPetsi? _catalogItem;
        public CatalogItemPetsi? CatalogItem
        {
            get { return _catalogItem; }
            set
            {
                if (_catalogItem != value)
                {
                    _catalogItem = value;
                    OnPropertyChanged(nameof(CatalogItem));
                }
            }
        }

        private CatalogItemPetsi? _veganMapping;
        public CatalogItemPetsi? VeganMapping
        {
            get { return _veganMapping; }
            set
            {
                if (_veganMapping != value)
                {
                    _veganMapping = value;
                    OnPropertyChanged(nameof(VeganMapping));              
                }
            }
        }
        private string _veganMappedItemName;
        public string VeganMappedItemName 
        { 
            get 
            { if(_veganMappedItemName != null) {  return _veganMappedItemName; }
                return "";
            }         
            set 
            {
                if (_veganMappedItemName != value)
                {
                    _veganMappedItemName = value;
                    OnPropertyChanged(nameof(VeganMappedItemName));
                }
            }
        }//******

        public string _standardLabelFilepath;
        public string StandardLabelFilePath { 
            get { return _standardLabelFilepath; }
            set
            { 
                if(_standardLabelFilepath != value)
                {
                    _standardLabelFilepath = value;
                    OnPropertyChanged(nameof(StandardLabelFilePath));
                }
            }
        }//******

        public string _cutieLabelFilepath;
        public string CutieLabelFilePath
        {
            get { return _cutieLabelFilepath; }
            set
            {
                if (_cutieLabelFilepath != value)
                {
                    _cutieLabelFilepath = value;
                    OnPropertyChanged(nameof(CutieLabelFilePath));
                }
            }
        }//******

        public string _categoryId;
        public string CategoryId 
        { 
            get { return _categoryId; }
            set
            {
                if(_categoryId != value)
                {
                    _categoryId = value;
                    OnPropertyChanged(nameof(CategoryId));
                }
            }
        }

        public string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                if (_categoryName != value)
                {
                    _categoryName = value;
                    OnPropertyChanged(nameof(CategoryName));
                }
            }
        }//******

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
        }//******

        #endregion
        private bool isNew;
        //public bool IsReadOnly { get; set; }
        public CatalogItemViewModel(CatalogItemPetsi? inputItem)
        {
            CatalogItem = inputItem;
            if(CatalogItem == null)
            {
                isNew = true;
                CatalogItem = new CatalogItemPetsi();
                NaturalNames = new ObservableCollection<string>();
                CategoryService cs = GetCategoryService();
                CategoryNames = new ObservableCollection<string>(cs.GetCategoryNames());
                CatalogService catalogService = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
                CatalogItem.CatalogObjectId = catalogService.GenerateCatalogId();
            }
            else
            {
                //Variations
                foreach ((string key, string value) in CatalogItem.VariationList)
                {
                    if (value.Contains(Identifiers.SIZE_CUTIE))
                    {
                        IsCutie = true;
                    }
                    if (value.Contains(Identifiers.SIZE_SMALL))
                    {
                        IsSmall = true;
                    }
                    if (value.Contains(Identifiers.SIZE_MEDIUM))
                    {
                        IsMedium = true;
                    }
                    if (value.Contains(Identifiers.SIZE_LARGE))
                    {
                        IsLarge = true;
                    }
                    if (value.Contains(Identifiers.SIZE_REGULAR))
                    {
                        IsRegular = true;
                    }
                }

                NaturalNames = new ObservableCollection<string>(inputItem.NaturalNames);
                StandardLabelFilePath = inputItem.StandardLabelFilePath;
                CutieLabelFilePath = inputItem.CutieLabelFilePath;
                CategoryService cs = GetCategoryService(); 
                CategoryNames = new ObservableCollection<string>(cs.GetCategoryNames());
                CategoryId = inputItem.CategoryId;
                CategoryName = cs.GetCategoryName(CategoryId);
                IsPOTM = inputItem.IsPOTM;
                ItemName = inputItem.ItemName;
                if (inputItem.VeganPieAssociation != null)
                {
                    _veganMapping = inputItem.VeganPieAssociation;
                    VeganMappedItemName = inputItem.VeganPieAssociation.ItemName;
                }
            }

        }

        public void AddNaturalName(string naturalName)
        {
            NaturalNames.Add(naturalName);
            //Item.AddNaturalName(naturalName);
            //UpdateCatalogModel();
        }

        public void RemoveNaturalName(string selectedItem)
        {
            NaturalNames.Remove(selectedItem);
            //Item.RemoveNaturalName(selectedItem);
            //UpdateCatalogModel();
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
                    CutieLabelFilePath = fileDialog.FileName;
                    //Item.CutieLabelFilePath = fileDialog.FileName;
                    //UpdateCatalogModel();
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
                    StandardLabelFilePath = fileDialog.FileName;
                    //Item.StandardLabelFilePath = fileDialog.FileName;
                    //UpdateCatalogModel();
                }
            }
        }
        private void UpdateCatalogModel()
        {
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            if (isNew)
            {
                ObsCatalogModelSingleton.AddItem(CatalogItem);
            }
            else
            {
                //cmp.UpdateModel();
                ObsCatalogModelSingleton.ModifyItem(CatalogItem);
            }
            
        }

        public object SetSelectedItem()
        {
            if(CategoryId == "") { return null; }
            foreach(string category in CategoryNames)
            {
                if(CategoryName == category)
                {
                    return category;
                }
            }
            return null;
        }

        public void SetCategory(string? categoryName)
        {
            if(categoryName == null) { return; }

            CategoryService cs = GetCategoryService();
            string result = "";
            result = cs.GetCategoryIdByCategoryName(categoryName);
            if (result != "")
            {
                //Item.CategoryId = result;
                CategoryId = result;
                CategoryName = categoryName;
                //UpdateCatalogModel();
            }
            else
            {
                SystemLogger.Log("CatalogItemViewModel for item: " + ItemName + " set category with input: \"" + categoryName + "\" returned empty");
            }
              
        }
        public void SetIsPOTM(bool isPOTM)
        {
            //Item.IsPOTM = isPOTM;
            //UpdateCatalogModel();
        }
        private CategoryService GetCategoryService() { return (CategoryService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATEGORY); }
        
        /*
        public void UpdateSizeSetting(string sizeVariation,bool isChecked)
        {
            Item.UpdateSizeVariation(sizeVariation, isChecked);
            //UpdateCatalogModel();
        }
        */

        public void SetVeganPieAssociation(CatalogItemPetsi selection)
        {
            //Item.VeganPieAssociation = selection;
            VeganMappedItemName = selection.ItemName;
            //UpdateCatalogModel();
        }

        public bool ValidateCatalogItem()
        {
            if (ItemName == "" || ItemName == null) { return false; }
            if(CategoryId == "" || CategoryId == null) { return false; }
            //if (!IsValidSizes(CategoryId)) { return false; }
            return true;
        }

        private bool IsValidSizes(string categoryId)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCatalogName(string textBoxItemName)
        {
            CatalogService service = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            return service.GetCatalogItem(textBoxItemName) == null;
        }

        public void SetItemName(string text)
        {
            //Item.ItemName = text;
            //UpdateCatalogModel();
        }

        public void UpdateItem()
        {
            CatalogItem.ItemName = ItemName;
            CatalogItem.CategoryId = CategoryId;
            CatalogItem.IsPOTM = IsPOTM;
            CatalogItem.VeganPieAssociation = VeganMapping;
            CatalogItem.UpdateSizeVariation(Identifiers.SIZE_SMALL, IsSmall);
            CatalogItem.UpdateSizeVariation(Identifiers.SIZE_MEDIUM, IsMedium);
            CatalogItem.UpdateSizeVariation(Identifiers.SIZE_LARGE, IsLarge);
            CatalogItem.UpdateSizeVariation(Identifiers.SIZE_REGULAR, IsRegular);
            CatalogItem.UpdateSizeVariation(Identifiers.SIZE_CUTIE, IsCutie);
            CatalogItem.StandardLabelFilePath = StandardLabelFilePath;
            CatalogItem.CutieLabelFilePath = CutieLabelFilePath;
            CatalogItem.NaturalNames = NaturalNames.ToList();

            UpdateCatalogModel();
        }
    }
}
