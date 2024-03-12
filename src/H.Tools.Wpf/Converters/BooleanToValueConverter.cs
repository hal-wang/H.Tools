using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

internal class BooleanToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolToValue(value, value != null && (bool)value, targetType, (string)parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
