using System;
using Windows.UI.Xaml.Data;

namespace HTools.Uwp.Converters
{
    internal class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConverterHelper.BoolTo((bool)value, targetType, parameter as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return false;
            else
                return true;
        }
    }
}