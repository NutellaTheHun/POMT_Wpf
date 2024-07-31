using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Globalization;
using System.Windows.Controls;

namespace POMT_WPF.Validation
{
    class CatalogItemValidation : ValidationRule
    {
        public CatalogItemValidation()
        {
            
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            CatalogService cs;
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if (value is string itemName)
            {
                CatalogItemPetsi item = cs.GetCatalogItem(itemName);
                if (item != null)
                {
                    if(item.CategoryId == null || item.CategoryId == "") { return new ValidationResult(false, null); }
                    if(!item.VariationExists(Identifiers.SIZE_REGULAR) 
                       && !item.VariationExists(Identifiers.SIZE_CUTIE) 
                       && !item.VariationExists(Identifiers.SIZE_SMALL) 
                       && !item.VariationExists(Identifiers.SIZE_MEDIUM)
                       && !item.VariationExists(Identifiers.SIZE_LARGE)) {  return new ValidationResult(false, null); }
                }
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, null);
        }
    }
}
