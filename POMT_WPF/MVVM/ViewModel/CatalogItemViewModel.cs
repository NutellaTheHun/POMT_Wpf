using Petsi.Events.ItemEvents;
using Petsi.Managers;
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
        private CategoryService _categoryService;

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

        public CatalogItemPetsi? TakeBakeMapping
        {
            get { return cItem.TakeNBakePieAssociation; }
            set
            {
                if (cItem.TakeNBakePieAssociation != value)
                {
                    cItem.TakeNBakePieAssociation = value;
                    OnPropertyChanged(nameof(TakeBakeMapping));
                }
            }
        }

        private string _takeBakeMappedItemName;
        public string TakeBakeMappedItemName
        {
            get
            {
                return _takeBakeMappedItemName;
            }
            set
            {
                if (_takeBakeMappedItemName != value)
                {
                    _takeBakeMappedItemName = value;
                    OnPropertyChanged(nameof(TakeBakeMappedItemName));
                }
            }
        }

        public CatalogItemPetsi? VeganTakeBakeMapping
        {
            get { return cItem.VeganTakeNBakePieAssociation; }
            set
            {
                if (cItem.VeganTakeNBakePieAssociation != value)
                {
                    cItem.VeganTakeNBakePieAssociation = value;
                    OnPropertyChanged(nameof(VeganTakeBakeMapping));
                }
            }
        }

        private string _veganTakeBakeMappedItemName;
        public string VeganTakeBakeMappedItemName
        {
            get
            {
                return _veganTakeBakeMappedItemName;
            }
            set
            {
                if (_veganTakeBakeMappedItemName != value)
                {
                    _veganTakeBakeMappedItemName = value;
                    OnPropertyChanged(nameof(VeganTakeBakeMappedItemName));
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
                    cItem.CategoryId = _categoryService.GetCategoryIdByCategoryName(CategoryName);
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

        public bool IsParbake
        {
            get { return cItem.IsParbake; }
            set
            {
                if (cItem.IsParbake != value)
                {
                    cItem.IsParbake = value;
                    OnPropertyChanged(nameof(IsParbake));
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
        public RelayCommand ClearVeganPie { get; set; }
        public RelayCommand SetTakeBakePie { get; set; }
        public RelayCommand ClearTakeBakePie { get; set; }
        public RelayCommand SetVeganTakeBakePie { get; set; }
        public RelayCommand ClearVeganTakeBakePie { get; set; }
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand SaveItem { get; set; }
        public RelayCommand BackCatalogItem { get; set; }

        #endregion



        public CatalogItemViewModel(CatalogItemPetsi? inputItem)
        {
            cItem = new CatalogItemPetsi(inputItem);
            _categoryService = GetCategoryService();
   
            if (inputItem == null)
            {
                IsNew = true;
                CanDelete = false;
                IsEdit = true;

                NaturalNames = new ObservableCollection<string>();
                NaturalNames.CollectionChanged += (s, e) => cItem.NaturalNames = NaturalNames.ToList();

                CategoryNames = new ObservableCollection<string>(GetCategoryService().GetCategoryNames());

                cItem.CatalogObjectId = CatalogItemPetsi.GenerateCatalogId();
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
                if (inputItem.TakeNBakePieAssociation != null)
                {
                    TakeBakeMappedItemName = inputItem.TakeNBakePieAssociation.ItemName;
                }
                if (inputItem.VeganTakeNBakePieAssociation != null)
                {
                    VeganTakeBakeMappedItemName = inputItem.VeganTakeNBakePieAssociation.ItemName;
                }
            }

            AddAltName = new RelayCommand(o => { AddAltNameCmd(); });
            RemoveAltName = new RelayCommand(o => { RemAltNameCmd(o); });
            SetStandardLblFile = new RelayCommand(o => { SetStdLblFileCmd(); });
            SetCutieLblFile = new RelayCommand(o => { SetCutieLblFileCmd(); });
            SetVeganPie = new RelayCommand(o => { SetVeganPieCmd(); });
            ClearVeganPie = new RelayCommand(o => { ClearVeganPieCmd(); });
            SetTakeBakePie = new RelayCommand(o => { SetTakeBakePieCmd(); });
            ClearTakeBakePie = new RelayCommand(o => { ClearTakeBakePieCmd(); });
            SetVeganTakeBakePie = new RelayCommand(o => { SetVeganTakeBakePieCmd(); });
            ClearVeganTakeBakePie = new RelayCommand(o => { ClearVeganTakeBakePieCmd(); });
            DeleteItem = new RelayCommand(o => { DeleteItemCmd(); });
            SaveItem = new RelayCommand(o => { SaveItemCmd(); });
            BackCatalogItem = new RelayCommand(o => { MainViewModel.Instance().BackCatalogView(); });

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
                ConfirmationWindow confirmationWindow = new ConfirmationWindow(null);
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    NaturalNames.Remove((string)o);
                }
            }
        }
        private void SetStdLblFileCmd()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_PIE_LBL_PATH);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                //fileDialog.InitialDirectory = labelsFilepath + "\\Pie";
                fileDialog.InitialDirectory = labelsFilepath;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    StandardLabelFilePath = System.IO.Path.GetFileName(fileDialog.FileName);
                }
            }
        }
        private void SetCutieLblFileCmd()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_CUTIE_LBL_PATH);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                //fileDialog.InitialDirectory = labelsFilepath + "\\Cuties";
                fileDialog.InitialDirectory = labelsFilepath;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    CutieLabelFilePath = System.IO.Path.GetFileName(fileDialog.FileName);
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
        private void ClearVeganPieCmd()
        {
            VeganMapping = null;
            VeganMappedItemName = null;
        }

        private void SetTakeBakePieCmd()
        {
            TakeBakeMapWindow window = new TakeBakeMapWindow();
            window.ShowDialog();
            if (window.ControlBool)
            {
                TakeBakeMapping = window.Selection;
                TakeBakeMappedItemName = TakeBakeMapping.ItemName;
            }
        }
        private void ClearTakeBakePieCmd()
        {
            TakeBakeMapping = null;
            TakeBakeMappedItemName = null;
        }

        private void SetVeganTakeBakePieCmd()
        {
            TakeBakeMapWindow window = new TakeBakeMapWindow();
            window.ShowDialog();
            if (window.ControlBool)
            {
                VeganTakeBakeMapping = window.Selection;
                VeganTakeBakeMappedItemName = VeganTakeBakeMapping.ItemName;
            }
        }
        private void ClearVeganTakeBakePieCmd()
        {
            VeganTakeBakeMapping = null;
            VeganTakeBakeMappedItemName = null;
        }
        private void DeleteItemCmd()
        {
            string warningMsg = null;
            if (ObsOrderModelSingleton.Instance.ContainsItem(cItem))
            {
                warningMsg = "WARNING: This item currently exists in active orders,\n are you sure you want to delete?";
            }

            ConfirmationWindow confirmationWindow = new ConfirmationWindow(warningMsg);
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                ObsCatalogModelSingleton.Instance.RemoveItem(cItem);
                MainViewModel.Instance().BackCatalogView();
            }
        }

        private void SaveItemCmd()
        {
           if(IsValidItem())
           {
                UpdateSizes();
                ObsCatalogModelSingleton.Instance.AddItem(cItem);
                CatalogItemViewEvents.OnSaveSuccessful();
           }
           //MainViewModel.Instance().BackCatalogView();
           //OR notify SAVED
        }

        private void UpdateSizes()
        {
            cItem.UpdateSizeVariation(Identifiers.SIZE_SMALL, IsSmall);
            cItem.UpdateSizeVariation(Identifiers.SIZE_MEDIUM, IsMedium);
            cItem.UpdateSizeVariation(Identifiers.SIZE_LARGE, IsLarge);
            cItem.UpdateSizeVariation(Identifiers.SIZE_REGULAR, IsRegular);
            cItem.UpdateSizeVariation(Identifiers.SIZE_CUTIE, IsCutie);
        }

        private CategoryService GetCategoryService() { return (CategoryService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATEGORY); }
        private bool IsValidItem()
        {
            bool controlBool = true;
            if (ItemName == "" || ItemName == null) { CatalogItemViewEvents.OnItemNameInvalid(); controlBool = false; }
            if (CategoryName == "" || CategoryName == null) { CatalogItemViewEvents.OnCategoryNameInvalid(); controlBool = false; }
            if (!IsValidSizes()) { CatalogItemViewEvents.OnCategorySizesInvalid(); controlBool = false; }

            //Validate that the item name doesn't exist except for itself.
            //A new item cannot be name already existing in the catalog
            //Viewing a existing item in the catalog can be itself or a new name in the catalog
            CatalogService service = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi result = service.GetCatalogItemById(cItem.CatalogObjectId);
            if (result == null) 
            {
                //if new -> new ID, new Name
                //if name doesnt exist, TRUE
                if (service.NameExists(cItem.ItemName)) { CatalogItemViewEvents.OnItemNameInvalid(); controlBool = false;  }
            }
            else 
            {
                //if exists -> existing ID, existing Name
                //if name matches ID or name doesn't exist return TRUE
                if(result.ItemName != cItem.ItemName && service.NameExists(cItem.ItemName)) { CatalogItemViewEvents.OnItemNameInvalid(); controlBool = false; }
            }
            return controlBool;
        }

        private bool IsValidSizes()
        {
            if(IsSmall) return true;
            if(IsMedium) return true;
            if(IsLarge) return true;
            if(IsRegular) return true;
            if(IsCutie) return true;
            return false;
        }
    }
}
