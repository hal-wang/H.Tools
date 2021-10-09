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
    /// <summary>
    /// 
    /// </summary>
    public class TimestampConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter,
#if NET462
            CultureInfo language
#endif
#if UAP10_0_18362
            string language
#endif
            )
        {

            var time = new DateTime(1970, 1, 1, 0, 0, 0) + TimeSpan.FromSeconds((long)value);
            return ConverterHelper.TimeToStr(time.ToLocalTime(), parameter as string);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
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
