using System.Windows;

namespace POMT_WPF.MVVM.Other
{
    public class ColumnHeaderHelper : DependencyObject
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached("Header", typeof(string), typeof(ColumnHeaderHelper), new PropertyMetadata(null));

        public static string GetHeader(DependencyObject obj)
        {
            return (string)obj.GetValue(HeaderProperty);
        }

        public static void SetHeader(DependencyObject obj, string value)
        {
            obj.SetValue(HeaderProperty, value);
        }
    }
}
