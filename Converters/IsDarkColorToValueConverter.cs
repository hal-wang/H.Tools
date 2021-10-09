using System;

#if NET462
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
#endif

#if UAP10_0_18362
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#endif

namespace HTools.Converters
{
    internal class IsDarkColorToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
#if NET462
            CultureInfo language
#endif
#if UAP10_0_18362
            string language
#endif
            )
        {
            Color color;
            if (value is Color)
            {
                color = (Color)value;
            }
            else if (value is SolidColorBrush scb)
            {
                var c = scb.Color;
                c.A = (byte)(c.A * scb.Opacity);
                color = c;
            }
            else if (value is string str)
            {
                color = ColorHelper.GetColor(str);
            }
            else
            {
                throw new NotSupportedException();
            }

            return ConverterHelper.BoolToValue(value, ColorHelper.IsDarkColor(color), targetType, parameter as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
#if NET462
            CultureInfo language
#endif
#if UAP10_0_18362
            string language
#endif
            )
        {
            throw new NotImplementedException();
        }
    }
}
