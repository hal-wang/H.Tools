using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

internal class NumCompareConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string param = (parameter as string)!;
        return ConverterHelper.BoolTo(CompareNumber(param[0], param.Remove(0, 1), value), targetType);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private bool CompareNumber(char ch, string compareValue, object value)
    {
        if (value is short shortNum)
        {
            return ch switch
            {
                '>' => shortNum > short.Parse(compareValue),
                '<' => shortNum < short.Parse(compareValue),
                '=' => shortNum == short.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is ushort ushortNum)
        {
            return ch switch
            {
                '>' => ushortNum > ushort.Parse(compareValue),
                '<' => ushortNum < ushort.Parse(compareValue),
                '=' => ushortNum == ushort.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is int intNum)
        {
            return ch switch
            {
                '>' => intNum > int.Parse(compareValue),
                '<' => intNum < int.Parse(compareValue),
                '=' => intNum == int.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is uint uintNum)
        {
            return ch switch
            {
                '>' => uintNum > uint.Parse(compareValue),
                '<' => uintNum < uint.Parse(compareValue),
                '=' => uintNum == uint.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is long longNum)
        {
            return ch switch
            {
                '>' => longNum > long.Parse(compareValue),
                '<' => longNum < long.Parse(compareValue),
                '=' => longNum == long.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is ulong ulongNum)
        {
            return ch switch
            {
                '>' => ulongNum > ulong.Parse(compareValue),
                '<' => ulongNum < ulong.Parse(compareValue),
                '=' => ulongNum == ulong.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is float floutNum)
        {
            return ch switch
            {
                '>' => floutNum > float.Parse(compareValue),
                '<' => floutNum < float.Parse(compareValue),
                '=' => floutNum == float.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is double doubleNum)
        {
            return ch switch
            {
                '>' => doubleNum > double.Parse(compareValue),
                '<' => doubleNum < double.Parse(compareValue),
                '=' => doubleNum == double.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is decimal decimalNum)
        {
            return ch switch
            {
                '>' => decimalNum > decimal.Parse(compareValue),
                '<' => decimalNum < decimal.Parse(compareValue),
                '=' => decimalNum == decimal.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else if (value is string str)
        {
            return ch switch
            {
                '>' => double.Parse(str) > double.Parse(compareValue),
                '<' => double.Parse(str) < double.Parse(compareValue),
                '=' => double.Parse(str) == double.Parse(compareValue),
                _ => throw new NotSupportedException(),
            };
        }
        else
        {
            throw new NotSupportedException();
        }
    }
}
