using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class FormatStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        return string.Format((parameter as string)!, value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
}
