using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using System.Globalization;
using System.Windows.Controls;

namespace POMT_WPF.Validation
{
    /// <summary>
    /// Handles ItemName validation of CatalogItemPetsi when user is typing an item name into a text field, 
    /// when false a control template highlights the field red to notify the user.
    /// </summary>
    public class ItemNameValidation : ValidationRule
    {
        CatalogService cs;
        public ItemNameValidation()
        {
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name;

            if(((string)value).Length > 0)
            {
                name = (string)value;
                if (!cs.ValidateItemName(name))
                {
                    return new ValidationResult(false, null);
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
