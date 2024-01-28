using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class IsNullToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        return ConverterHelper.BoolToValue(value, value == null, targetType, (parameter as string)!);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
}
