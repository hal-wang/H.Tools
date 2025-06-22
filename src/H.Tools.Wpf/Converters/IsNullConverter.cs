using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

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
