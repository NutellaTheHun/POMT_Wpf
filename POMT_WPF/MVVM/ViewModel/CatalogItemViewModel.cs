using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

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
        public string StandardLabelFilepath { 
            get { return _standardLabelFilepath; }
            set
            { 
                if(_standardLabelFilepath != value)
                {
                    _standardLabelFilepath = value;
                    OnPropertyChanged(nameof(StandardLabelFilepath));
                }
            }
        }

        public string _cutieLabelFilepath;
        public string CutieLabelFilepath
        {
            get { return _cutieLabelFilepath; }
            set
            {
                if (_cutieLabelFilepath != value)
                {
                    _cutieLabelFilepath = value;
                    OnPropertyChanged(nameof(CutieLabelFilepath));
                }
            }
        }

        ObservableCollection<string> NaturalNames;

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
                StandardLabelFilepath = item.StandardLabelFilePath;
                //CutieLabelFp
                CutieLabelFilepath = item.CutieLabelFilePath;
                //Category

            }
        }

        public void AddCatalogItem()
        {
           
        }
    }
}
