using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace POMT_WPF.Converters
{
    public class BoolToNavigationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isReadOnly)
            {
                return isReadOnly ? KeyboardNavigationMode.None : KeyboardNavigationMode.Cycle;
            }
            return KeyboardNavigationMode.Cycle; // Default value
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
