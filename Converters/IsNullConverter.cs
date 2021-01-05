using System;

#if NET452
using System.Windows.Data;
using System.Globalization;
#endif

#if UAP10_0_18362
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#endif

namespace HTools.Converters
{
    internal class IsNullConverter : IValueConverter
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
            return ConverterHelper.BoolTo(value == null, targetType, parameter as string);
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