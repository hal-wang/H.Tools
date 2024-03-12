using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace H.Tools.Wpf.Converters;

internal class IsDarkColorToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        Color color;
        if (value is Color mc)
        {
            color = mc;
        }
        else if (value is SolidColorBrush scb)
        {
            var c = scb.Color;
            c.A = (byte)(c.A * scb.Opacity);
            color = c;
        }
        else if (value is string str)
        {
            color = GetColor(str);
        }
        else
        {
            throw new NotSupportedException();
        }

        return ConverterHelper.BoolToValue(value, IsDarkColor(color), targetType, (parameter as string)!);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }

    private static Color GetColor(string str)
    {
        if (str[0] == '#')
        {
            return str.Length switch
            {
                4 => Color.FromArgb(0xff, System.Convert.ToByte("0x" + str[1] + str[1], 16), System.Convert.ToByte("0x" + str[2] + str[2], 16), System.Convert.ToByte("0x" + str[3].ToString() + str[3], 16)),
                5 => Color.FromArgb(System.Convert.ToByte("0x" + str[1] + str[1], 16), System.Convert.ToByte("0x" + str[2] + str[2], 16), System.Convert.ToByte("0x" + str[3] + str[3], 16), System.Convert.ToByte("0x" + str[4] + str[4], 16)),
                7 => Color.FromArgb(0xff, System.Convert.ToByte("0x" + str[1] + str[2], 16), System.Convert.ToByte("0x" + str[3] + str[4], 16), System.Convert.ToByte("0x" + str[5] + str[6], 16)),
                9 => Color.FromArgb(System.Convert.ToByte("0x" + str[1] + str[2], 16), System.Convert.ToByte("0x" + str[3] + str[4], 16), System.Convert.ToByte("0x" + str[5] + str[6], 16), System.Convert.ToByte("0x" + str[7] + str[8], 16)),
                _ => throw new FormatException(),
            };
        }
        else
        {
            var resource = Application.Current.Resources[str];
            if (resource is Color color)
            {
                return color;
            }
            else if (resource is SolidColorBrush brush)
            {
                return brush.Color;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }

    private static bool IsDarkColor(Color color, int sensory = 192, Color? backColor = null)
    {
        if (color.A < 255)
        {
            color = MergeAlpha(color, backColor);
        }

        if (color.R * 0.299 + color.G * 0.578 + color.B * 0.114 >= sensory)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static Color MergeAlpha(Color alphaColor, Color? backColor = null)
    {
        double percent = (double)alphaColor.A / 0xff;
        backColor ??= Colors.White;

        alphaColor.R = (byte)(alphaColor.R * percent + backColor.Value.R * (1 - percent));
        alphaColor.G = (byte)(alphaColor.G * percent + backColor.Value.G * (1 - percent));
        alphaColor.B = (byte)(alphaColor.B * percent + backColor.Value.B * (1 - percent));
        alphaColor.A = 0xff;

        return alphaColor;
    }
}
