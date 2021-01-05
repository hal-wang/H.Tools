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
    class NoBreakConverter : IValueConverter
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
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return string.Empty;
            }

            if (!(value is string str))
            {
                str = value.ToString();
            }

            return str.Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ');
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
