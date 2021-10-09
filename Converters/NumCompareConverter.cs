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
    internal class NumCompareConverter : IValueConverter
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
            string param = parameter as string;
            var isTrue = (param[0]) switch
            {
                '>' => (int)value > int.Parse(param[1].ToString()),
                '<' => (int)value < int.Parse(param[1].ToString()),
                '=' => (int)value == int.Parse(param[1].ToString()),
                _ => throw new Exception(),
            };

            return ConverterHelper.BoolTo(isTrue, targetType);
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