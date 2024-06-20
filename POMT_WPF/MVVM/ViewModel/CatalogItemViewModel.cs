using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogItemViewModel : ViewModelBase
    { 
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
        }

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
        }

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
        }

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
        }

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
        }
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
        }

        public ObservableCollection<string> NaturalNames;

        public ObservableCollection<string> CategoryNames;
        
        private CatalogItemPetsi? _item;
        public CatalogItemPetsi? Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
                }
            }
        }
        
        
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
        }

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
        }

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
        }

        public CatalogItemViewModel(CatalogItemPetsi? item)
        {
            Item = item;
            if(Item == null)
            {
                Item = new CatalogItemPetsi();
                NaturalNames = new ObservableCollection<string>();
            }
            else
            {
                //Variations
                foreach ((string key, string value) in Item.VariationList)
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
                //Natural Names
                NaturalNames = new ObservableCollection<string>(item.NaturalNames);
                //StandardLabelFp

                StandardLabelFilePath = item.StandardLabelFilePath;
                //CutieLabelFp
                CutieLabelFilePath = item.CutieLabelFilePath;
                //Category
                CategoryService cs = GetCategoryService();
                //CategoryNames = 
                CategoryNames = new ObservableCollection<string>(cs.GetCategoryNames());

                CategoryId = item.CategoryId;
                CategoryName = cs.GetCategoryName(CategoryId);
                IsPOTM = item.IsPOTM;
            }
        }

        public void AddCatalogItem()
        {
           
        }

        public void AddNaturalName(string naturalName)
        {
            NaturalNames.Add(naturalName);
            Item.AddNaturalName(naturalName);
            UpdateCatalogModel();
        }

        public void RemoveNaturalName(string selectedItem)
        {
            NaturalNames.Remove(selectedItem);
            Item.RemoveNaturalName(selectedItem);
            UpdateCatalogModel();
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
                    Item.CutieLabelFilePath = fileDialog.FileName;
                    UpdateCatalogModel();
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
                    Item.StandardLabelFilePath = fileDialog.FileName;
                    UpdateCatalogModel();
                }
            }
        }
        private void UpdateCatalogModel()
        {
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            cmp.UpdateModel();
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

        public void SetCategory(string? v)
        {
            if(v == null) { return; }

            CategoryService cs = GetCategoryService();
            string result = "";
            result = cs.GetCategoryIdByCategoryName(v);
            if (result != "")
            {
                Item.CategoryId = result;
                UpdateCatalogModel();
            }
            else
            {
                SystemLogger.Log("CatalogItemViewModel for item: " + Item.ItemName + " set category with input: \"" + v + "\" returned empty");
            }
              
        }
        public void SetIsPOTM(bool isPOTM)
        {
            Item.IsPOTM = isPOTM;
            UpdateCatalogModel();
        }
        private CategoryService GetCategoryService() { return (CategoryService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATEGORY); }

        public void UpdateSizeSetting(string sizeVariation,bool isChecked)
        {
            Item.UpdateSizeVariation(sizeVariation, isChecked);
            UpdateCatalogModel();
        }
    }
}
