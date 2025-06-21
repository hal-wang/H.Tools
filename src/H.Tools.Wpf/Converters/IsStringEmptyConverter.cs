using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

internal class IsStringEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        return ConverterHelper.BoolTo(string.IsNullOrEmpty(value as string), targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
}
