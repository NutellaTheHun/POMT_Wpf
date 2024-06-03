
using Petsi.Units;
using Petsi.Utils;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogItemViewModel : ViewModelBase
    { 
        private CatalogItemPetsi? _item;

        private bool _isSmall;

        public bool IsSmall
        {
            get { return _isSmall; }
            set
            {
                if (_isSmall != value)
                {
                    _isSmall = value;
                    OnPropertyChanged(nameof(_isSmall));
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
                    OnPropertyChanged(nameof(_isMedium));
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
                    OnPropertyChanged(nameof(_isLarge));
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
                    OnPropertyChanged(nameof(_isRegular));
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
                    OnPropertyChanged(nameof(_isCutie));
                }
            }
        }
        public CatalogItemPetsi? Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    OnPropertyChanged(nameof(_item));
                }
            }
        }
        public CatalogItemViewModel(CatalogItemPetsi? item)
        {
            Item = item;
            if(Item == null){ return; }
            foreach((string key, string value) in Item.VariationList)
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
            //if (Item.Variations.Contains(Identifiers.SIZE_CUTIE))
            //{
            //    IsCutie = true;
            //}
            //if (Item.Variations.Contains(Identifiers.SIZE_SMALL))
            //{
            //    IsSmall = true;
            //}
            //if (Item.Variations.Contains(Identifiers.SIZE_MEDIUM))
            //{
            //    IsMedium = true;
            //}
            //if (Item.Variations.Contains(Identifiers.SIZE_LARGE))
            //{
            //    IsLarge = true;
            //}
            //if (Item.Variations.Contains(Identifiers.SIZE_REGULAR))
            //{
            //    IsRegular = true;
            //}
        }

        public void AddCatalogItem()
        {
           Item = new CatalogItemPetsi();
        }
    }
}
