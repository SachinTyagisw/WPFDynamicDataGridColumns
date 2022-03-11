using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DynamicDataGridColumns.Converters
{
    /// <summary>
    /// http://stackoverflow.com/questions/13571902/hide-cell-in-new-row-in-wpf-datagrid
    /// </summary>
    public class IsNamedObjectVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().Name == "NamedObject")
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
