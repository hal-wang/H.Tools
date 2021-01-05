using System;

#if NET452
using System.Windows.Data;
using System.Globalization;
#endif

#if UAP10_0_18362
using Windows.UI.Xaml.Data;
#endif

namespace HTools.Converters
{
    internal class IsDarkThemeToValueConverter : IValueConverter
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
#if NET452
            return ConverterHelper.BoolToValue(value, false, targetType, parameter as string);
#endif
#if UAP10_0_18362
            return ConverterHelper.BoolToValue(value, Windows.UI.Xaml.Application.Current.RequestedTheme == Windows.UI.Xaml.ApplicationTheme.Dark, targetType, parameter as string);
#endif
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
