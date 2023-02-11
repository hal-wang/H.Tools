using System;

#if NET462
using System.Windows.Data;
using System.Globalization;
#endif

#if UAP10_0_18362
using Windows.UI.Xaml.Data;
#endif

namespace HTools.Converters
{
    internal class FormatStringConverter : IValueConverter
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
            return string.Format(parameter as string, value);
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
