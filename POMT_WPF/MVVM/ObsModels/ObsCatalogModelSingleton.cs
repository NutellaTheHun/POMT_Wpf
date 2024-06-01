
using Petsi.Models;

namespace POMT_WPF.MVVM.ObsModels
{
    public class ObsCatalogModelSingleton
    {
        private static ObsCatalogModelSingleton _instance;
        private CatalogModelPetsi _cmp;
        public static ObsCatalogModelSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObsCatalogModelSingleton();
                }
                return _instance;
            }
        }
    }
}
