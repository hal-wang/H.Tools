using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace H.Tools.Wpf.Converters;

internal class BackgroundToForegroundConverter : IsDarkColorConverterBase, IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isDarkColor;
        try
        {
            isDarkColor = IsDarkColor(value);
        }
        catch
        {
            return null;
        }

        if (targetType == typeof(Color))
        {
            return isDarkColor ? Colors.White : Colors.Black;
        }
        else if (targetType == typeof(SolidColorBrush) || targetType == typeof(Brush))
        {
            return isDarkColor ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
        }
        else
        {
            return isDarkColor;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
