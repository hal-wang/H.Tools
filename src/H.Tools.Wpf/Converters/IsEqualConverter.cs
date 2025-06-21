using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

internal class IsEqualConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolTo(value == parameter, targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class IsToStringEqualConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConverterHelper.BoolTo(value?.ToString() == parameter?.ToString(), targetType, parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class IsMultiEqualConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var isEqual = true;
        for (var i = 0; i < values.Length; i++)
        {
            if (values[i] != parameter)
            {
                isEqual = false;
            }
        }
        return ConverterHelper.BoolTo(isEqual, targetType, parameter as string);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class IsMultiEqualWithoutParamConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var isEqual = true;
        for (var i = 1; i < values.Length; i++)
        {
            if (values[i] != values[0])
            {
                isEqual = false;
            }
        }
        return ConverterHelper.BoolTo(isEqual, targetType, parameter as string);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class IsMultiToStringEqualConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var isEqual = true;
        for (var i = 0; i < values.Length; i++)
        {
            if (values[i]?.ToString() != parameter?.ToString())
            {
                isEqual = false;
            }
        }
        return ConverterHelper.BoolTo(isEqual, targetType, parameter as string);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


internal class IsMultiToStringEqualWithoutParamConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var isEqual = true;
        for (var i = 1; i < values.Length; i++)
        {
            if (values[i]?.ToString() != values[0]?.ToString())
            {
                isEqual = false;
            }
        }
        return ConverterHelper.BoolTo(isEqual, targetType, parameter as string);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}