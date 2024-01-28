using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class IsEqualConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolTo(value == parameter, targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
