using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogItemViewModel : ViewModelBase
    {
        public CatalogItemPetsi cItem;

        #region props

        public CatalogItemPetsi? VeganMapping
        {
            get { return cItem.VeganPieAssociation; }
            set
            {
                if (cItem.VeganPieAssociation != value)
                {
                    cItem.VeganPieAssociation = value;
                    OnPropertyChanged(nameof(VeganMapping));              
                }
            }
        }

        private string _veganMappedItemName;
        public string VeganMappedItemName 
        { 
            get 
             {  
                return _veganMappedItemName;
            }      
            set 
            {
                if (_veganMappedItemName != value)
                {
                    _veganMappedItemName = value;
                    OnPropertyChanged(nameof(VeganMappedItemName));
                }
            }
        }

        public string StandardLabelFilePath { 
            get { return cItem.StandardLabelFilePath; }
            set
            { 
                if(cItem.StandardLabelFilePath != value)
                {
                    cItem.StandardLabelFilePath = value;
                    OnPropertyChanged(nameof(StandardLabelFilePath));
                }
            }
        }

        public string CutieLabelFilePath
        {
            get { return cItem.CutieLabelFilePath; }
            set
            {
                if (cItem.CutieLabelFilePath != value)
                {
                    cItem.CutieLabelFilePath = value;
                    OnPropertyChanged(nameof(CutieLabelFilePath));
                }
            }
        }
        public string CategoryId 
        { 
            get { return cItem.CategoryId; }
            set
            {
                if(cItem.CategoryId != value)
                {
                    cItem.CategoryId = value;
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

        public string ItemName
        {
            get { return cItem.ItemName; }
            set
            {
                if (cItem.ItemName != value)
                {
                    cItem.ItemName = value;
                    OnPropertyChanged(nameof(ItemName));
                }
            }
        }

        public ObservableCollection<string> NaturalNames { get; set; }

        public ObservableCollection<string> CategoryNames { get; set; }

        private bool _canDelete;
        public bool CanDelete
        {
            get { return _canDelete; }
            set
            {
                if (_canDelete != value)
                {
                    _canDelete = value;
                    OnPropertyChanged(nameof(CanDelete));
                }
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                if (_isNew != value)
                {
                    _isNew = value;
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                if (_isEdit != value)
                {
                    _isEdit = value;
                    OnPropertyChanged(nameof(IsEdit));
                }
            }
        }

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

        public bool IsPOTM
        {
            get { return cItem.IsPOTM; }
            set
            {
                if (cItem.IsPOTM != value)
                {
                    cItem.IsPOTM = value;
                    OnPropertyChanged(nameof(IsPOTM));
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand AddAltName { get; set; }
        public RelayCommand RemoveAltName { get; set; }
        public RelayCommand SetStandardLblFile { get; set; }
        public RelayCommand SetCutieLblFile { get; set; }
        public RelayCommand SetVeganPie { get; set; }
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand SaveItem { get; set; }
        public RelayCommand BackCatalogItem { get; set; }

        #endregion

        public CatalogItemViewModel(CatalogItemPetsi? inputItem)
        {
            cItem = new CatalogItemPetsi(inputItem);

            if (inputItem == null)
            {
                IsNew = true;
                CanDelete = false;
                IsEdit = true;

                NaturalNames = new ObservableCollection<string>();
                NaturalNames.CollectionChanged += (s, e) => cItem.NaturalNames = NaturalNames.ToList();

                CategoryService cs = GetCategoryService();
                CategoryNames = new ObservableCollection<string>(cs.GetCategoryNames());

                CatalogService catalogService = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
                cItem.CatalogObjectId = catalogService.GenerateCatalogId();
            }
            else
            {
                CanDelete = true;
                IsNew = false;
                IsEdit = false;

                //Variations
                foreach ((string key, string value) in cItem.VariationList)
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
                NaturalNames.CollectionChanged += (s, e) => cItem.NaturalNames = NaturalNames.ToList();

                CategoryId = inputItem.CategoryId;

                CategoryService cs = GetCategoryService();
                CategoryNames = new ObservableCollection<string>(cs.GetCategoryNames());
                CategoryName = cs.GetCategoryName(CategoryId);

                if (inputItem.VeganPieAssociation != null)
                {
                    VeganMappedItemName = inputItem.VeganPieAssociation.ItemName;
                }
            }

            AddAltName = new RelayCommand(o => { AddAltNameCmd(); });
            RemoveAltName = new RelayCommand(o => { RemAltNameCmd(o); });
            SetStandardLblFile = new RelayCommand(o => { SetStdLblFileCmd(); });
            SetCutieLblFile = new RelayCommand(o => { SetCutieLblFileCmd(); });
            SetVeganPie = new RelayCommand(o => { SetVeganPieCmd(); });
            DeleteItem = new RelayCommand(o => { DeleteItemCmd(); });
            SaveItem = new RelayCommand(o => { SaveItemCmd(); });
            BackCatalogItem = new RelayCommand(o => { BackCmd(); });

        }

        private void AddAltNameCmd()
        {
            AddNaturalNameWindow view = new AddNaturalNameWindow();
            view.ShowDialog();
            if (view.ControlBool)
            {
                NaturalNames.Add(view.AlternativeName);
            }
        }
        private void RemAltNameCmd(object o)
        {
            if (o is string)
            {
                ConfirmationWindow confirmationWindow = new ConfirmationWindow();
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    NaturalNames.Remove((string)o);
                }
            }
        }
        private void SetStdLblFileCmd()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Pie";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    StandardLabelFilePath = fileDialog.FileName;
                }
            }
        }
        private void SetCutieLblFileCmd()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Cuties";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    CutieLabelFilePath = fileDialog.FileName;
                }
            }
        }
        private void SetVeganPieCmd()
        {
            VeganMapWindow window = new VeganMapWindow();
            window.ShowDialog();
            if (window.ControlBool)
            {
                VeganMapping = window.Selection;
                VeganMappedItemName = VeganMapping.ItemName;
            }
        }
        private void DeleteItemCmd()
        {
            ConfirmationWindow confirmationWindow = new ConfirmationWindow();
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                ObsCatalogModelSingleton.Instance.RemoveItem(cItem);
                BackCmd();
            }
        }

        private void SaveItemCmd()
        {
            ObsCatalogModelSingleton.Instance.AddItem(cItem);
            BackCmd();
        }
        private void BackCmd()
        {
            MainViewModel.Instance().BackCatalogView();
        }

        public void AddNaturalName(string naturalName)
        {
            NaturalNames.Add(naturalName);
        }

        public void RemoveNaturalName(string selectedItem)
        {
            NaturalNames.Remove(selectedItem);
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
                }
            }
        }
        private void UpdateCatalogModel()
        {
            /*
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            if (IsNew)
            {
                ObsCatalogModelSingleton.Instance.AddItem(cItem);
            }
            else
            {
                //cmp.UpdateModel();
                ObsCatalogModelSingleton.ModifyItem(cItem);
            }
            */
            ObsCatalogModelSingleton.Instance.AddItem(cItem);
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

        //NEED FIX will return false if pre-existing name is changed and returned to original
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
            cItem.ItemName = ItemName;
            cItem.CategoryId = CategoryId;
            cItem.IsPOTM = IsPOTM;
            cItem.VeganPieAssociation = VeganMapping;
            cItem.UpdateSizeVariation(Identifiers.SIZE_SMALL, IsSmall);
            cItem.UpdateSizeVariation(Identifiers.SIZE_MEDIUM, IsMedium);
            cItem.UpdateSizeVariation(Identifiers.SIZE_LARGE, IsLarge);
            cItem.UpdateSizeVariation(Identifiers.SIZE_REGULAR, IsRegular);
            cItem.UpdateSizeVariation(Identifiers.SIZE_CUTIE, IsCutie);
            cItem.StandardLabelFilePath = StandardLabelFilePath;
            cItem.CutieLabelFilePath = CutieLabelFilePath;
            cItem.NaturalNames = NaturalNames.ToList();

            UpdateCatalogModel();
        }
    }
}
