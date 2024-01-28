using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class IsTypeEqualConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolTo(value?.GetType() == parameter as Type, targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
