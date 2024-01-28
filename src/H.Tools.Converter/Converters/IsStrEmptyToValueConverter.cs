using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class IsStrEmptyToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        return ConverterHelper.BoolToValue(value, string.IsNullOrEmpty(value as string), targetType, (string)parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
}
