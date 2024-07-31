using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace POMT_WPF.Converters
{
    //For Keyboard Navigation
    public class InvertBoolToNavigationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isReadOnly)
            {
                return isReadOnly ? KeyboardNavigationMode.Cycle : KeyboardNavigationMode.None;
            }
            return KeyboardNavigationMode.Cycle; // Default value
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
