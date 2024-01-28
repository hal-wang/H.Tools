using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class IsNullConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolTo(value == null, targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
