using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using System.Globalization;
using System.Windows.Controls;

namespace POMT_WPF.Validation
{
    class CatalogItemNameValidation : ValidationRule
    {
        CatalogService cs;
        public CatalogItemNameValidation()
        {
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name;

            if (((string)value).Length > 0)
            {
                name = (string)value;
                if (!cs.NameExists(name))
                {
                    return new ValidationResult(false, null);
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
