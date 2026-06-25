using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

public class IsEqualConverter : IValueConverter
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

public class IsToStringEqualConverter : IValueConverter
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

public class IsMultiEqualConverter : IMultiValueConverter
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

public class IsMultiEqualWithoutParamConverter : IMultiValueConverter
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

public class IsMultiToStringEqualConverter : IMultiValueConverter
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


public class IsMultiToStringEqualWithoutParamConverter : IMultiValueConverter
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