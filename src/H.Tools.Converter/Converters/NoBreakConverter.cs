using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

class NoBreakConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        if (string.IsNullOrEmpty(value?.ToString()))
        {
            return string.Empty;
        }

        if (value is not string str)
        {
            str = value.ToString() ?? string.Empty;
        }

        return str.Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ');
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
}
