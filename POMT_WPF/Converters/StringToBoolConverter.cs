using System.Globalization;
using System.Windows.Data;

namespace POMT_WPF.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue && parameter is string strParameter)
            {
                return strValue == strParameter;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue && parameter is string strParameter)
            {
                return strParameter;
            }
            return Binding.DoNothing;
        }
    }
}
