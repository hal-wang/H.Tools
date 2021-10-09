using Newtonsoft.Json;
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
    /// 用于绑定测试
    /// </summary>
    class TestConverter : IValueConverter
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
            Console.WriteLine("TestConverter Convert");
            Console.WriteLine("value: " + JsonConvert.SerializeObject(value));
            Console.WriteLine("targetType: " + targetType);
            Console.WriteLine("parameter: " + parameter);
            Console.WriteLine("language: " + language);

            return GetDefault(targetType);
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
            Console.WriteLine("TestConverter ConvertBack");
            Console.WriteLine("value: " + JsonConvert.SerializeObject(value));
            Console.WriteLine("targetType: " + targetType);
            Console.WriteLine("parameter: " + parameter);
            Console.WriteLine("language: " + language);

            return GetDefault(targetType);
        }

        private object GetDefault(Type targetType)
        {
            if (targetType.IsValueType)
            {
                return Activator.CreateInstance(targetType);
            }
            else
            {
                return null;
            }
        }
    }
}
