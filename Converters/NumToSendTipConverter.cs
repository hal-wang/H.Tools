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
    internal class NumToSendTipConverter : IValueConverter
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
            if ((int)value > 0)
            {
                return value;
            }
            else
            {
                return "发送";
            }
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