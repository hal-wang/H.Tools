using System.Windows;
using System.Windows.Media;

namespace H.Tools.Converter;

public static class ConverterHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isTrue"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static object BoolToValue(object value, bool isTrue, Type targetType, string parameter)
    {
        string[] strs = parameter.Split(':');
        string value1 = strs[0];
        string value2 = strs[1];
        if (
                (isTrue && string.Equals(value1, "@VALUE", StringComparison.InvariantCultureIgnoreCase)) ||
                (!isTrue && string.Equals(value2, "@VALUE", StringComparison.InvariantCultureIgnoreCase))
            )
        {
            if (value.GetType() == targetType)
            {
                return value;
            }

            if (value is not string str)
            {
                return ConvertValue<string>(value)!;
            }

            if (isTrue)
            {
                value1 = str;
            }
            else
            {
                value2 = str;
            }
        }

        if (targetType == typeof(int))
        {
            return isTrue ? int.Parse(value1) : int.Parse(value2);
        }
        else if (targetType == typeof(double))
        {
            return isTrue ? double.Parse(value1) : double.Parse(value2);
        }
        else if (targetType == typeof(Color) || targetType == typeof(Brush) || targetType == typeof(SolidColorBrush))
        {
            if (targetType == typeof(Color))
            {
                return isTrue
                    ? (value1[0] == '#' ? (Color)ColorConverter.ConvertFromString(value1) : (Color)Application.Current.Resources[value1])
                    : (value2[0] == '#' ? (Color)ColorConverter.ConvertFromString(value2) : (Color)Application.Current.Resources[value2]);
            }
            else
            {
                var val = isTrue
                    ? (value1[0] == '#' ? (Color)ColorConverter.ConvertFromString(value1) : Application.Current.Resources[value1])
                    : (value2[0] == '#' ? (Color)ColorConverter.ConvertFromString(value2) : Application.Current.Resources[value2]);
                if (val is Color color)
                {
                    return new SolidColorBrush(color);
                }
                else
                {
                    return val;
                }
            }
        }
        else if (targetType == typeof(Style))
        {
            return isTrue ? Application.Current.Resources[value1] : Application.Current.Resources[value2];
        }
        else if (targetType.IsEnum)
        {
            return isTrue ? Enum.Parse(targetType, value1) : Enum.Parse(targetType, value2);
        }
        else
        {
            return isTrue ? value1 : value2;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isTrue"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static object BoolTo(bool isTrue, Type targetType, string? parameter = null)
    {
        if (string.Equals(parameter, "true", StringComparison.InvariantCultureIgnoreCase) || string.Equals(parameter, "T", StringComparison.InvariantCultureIgnoreCase))
        {
            isTrue = !isTrue;
        }

        if (targetType == typeof(bool) || targetType == typeof(bool?))
        {
            return isTrue;
        }
        else if (targetType == typeof(Visibility))
        {
            return isTrue ? Visibility.Visible : Visibility.Collapsed;
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public static object? ConvertValue<T>(object value)
    {
        return ConvertValue(value, typeof(T));
    }

    public static object? ConvertValue(object value, Type type)
    {
        if (value == null || value is DBNull || (value is string emptyStr && emptyStr == string.Empty))
        {
            return default;
        }
        else if (value.GetType() == type)
        {
            return value;
        }
        else if (value is string str && type == typeof(bool))
        {
            if (string.Equals(str, "y", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(str, "yes", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(str, "true", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(str, "1", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Convert.ChangeType(value, type);
        }
    }
}
