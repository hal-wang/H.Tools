using System;

#if NET452
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
#endif

#if UAP10_0_18362
using HTools.Uwp.Helpers;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#endif

namespace HTools.Converters
{
    internal class BackColorToForeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
#if NET452
            CultureInfo language
#endif
#if UAP10_0_18362
            string language
#endif
            )
        {

            Color? backColor = null;
            if (value == null)
            {
                backColor = null;
            }
            else if (value.GetType() == typeof(Color?))
            {
                backColor = (Color?)value;
            }
            else if (value.GetType() == typeof(Color))
            {
                backColor = (Color)value;
            }
            else if (value.GetType() == typeof(SolidColorBrush))
            {
                if (value is SolidColorBrush scb)
                {
                    var color = scb.Color;
                    color.A = (byte)(color.A * scb.Opacity);
                    backColor = color;
                }
            }
            else
            {
                var color = value.ToString();
                if (color != null && color.Length > 0 && color[0] == '#')
                {
                    backColor = ColorHelper.GetColor(color);
                }
                else
                {
                    backColor = ResourcesHelper.GetResource<Color>(color);
                }
            }

            bool isBackDark;
            if (backColor == null)
            {
#if UAP10_0_18362
                isBackDark = ThemeHelper.ElementTheme == Windows.UI.Xaml.ElementTheme.Dark;
#endif
#if NET452
                isBackDark = false;
#endif
            }
            else
            {

                Color defaultBackColor;
#if UAP10_0_18362
                defaultBackColor = ThemeHelper.ElementTheme == Windows.UI.Xaml.ElementTheme.Dark ? Colors.Black : Colors.White;
#endif
#if NET452
                defaultBackColor = Colors.White;
#endif

                isBackDark = ColorHelper.IsDarkColor(backColor.Value, backColor: defaultBackColor);
            }

            if (targetType == typeof(Color))
            {
                return isBackDark ? Colors.White : Colors.Black;
            }
            else if (targetType == typeof(SolidColorBrush) || targetType == typeof(Brush))
            {
                return isBackDark ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
#if NET452
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
