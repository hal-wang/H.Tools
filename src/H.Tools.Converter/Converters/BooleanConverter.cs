using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class BooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolTo(value != null && (bool)value, targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
