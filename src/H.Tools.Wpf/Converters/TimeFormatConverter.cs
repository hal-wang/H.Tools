using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Wpf.Converters;

internal class TimeFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return "";

        var format = parameter as string;
        bool isLocal = false;
        if (!string.IsNullOrEmpty(format) && format[^1] == 'L')
        {
            isLocal = true;
            format = format[..^1];
        }

        if (value is DateTime dateTime)
        {
            if (string.IsNullOrEmpty(format))
            {
                return dateTime.ToString();
            }
            else if (isLocal)
            {
                return dateTime.ToLocalTime().ToString(format);
            }
            else
            {
                return dateTime.ToString(format);
            }
        }
        else if (value is TimeSpan timeSpan)
        {
            if (string.IsNullOrEmpty(format))
            {
                return timeSpan.ToString();
            }
            else
            {
                return timeSpan.ToString(format);
            }
        }
        else if (value is DateTimeOffset dateTimeOffset)
        {
            if (string.IsNullOrEmpty(format))
            {
                return dateTimeOffset.ToString();
            }
            else if (isLocal)
            {
                return dateTimeOffset.ToLocalTime().ToString(format);
            }
            else
            {
                return dateTimeOffset.ToString(format);
            }
        }
        else if (value is long ms)
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(ms).ToString();
            }
            else if (isLocal)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(ms).ToLocalTime().ToString(format);
            }
            else
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(ms).ToString(format);
            }
        }
        else if (value is int seconds)
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTimeOffset.FromUnixTimeSeconds(seconds).ToString();
            }
            else if (isLocal)
            {
                return DateTimeOffset.FromUnixTimeSeconds(seconds).ToLocalTime().ToString(format);
            }
            else
            {
                return DateTimeOffset.FromUnixTimeSeconds(seconds).ToString(format);
            }
        }
        else if (value is string str)
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTime.Parse(str).ToString();
            }
            else if (isLocal)
            {
                return DateTime.Parse(str).ToLocalTime().ToString(format);
            }
            else
            {
                return DateTime.Parse(str).ToString(format);
            }
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (value as string)!;

        var format = parameter as string;
        bool isLocal = false;
        if (!string.IsNullOrEmpty(format) && format[^1] == 'L')
        {
            isLocal = true;
            format = format[..^1];
        }

        if (targetType == typeof(TimeSpan))
        {
            if (string.IsNullOrEmpty(format))
            {
                return TimeSpan.TryParse(val, out var res) ? res : null!;
            }
            else
            {
                return TimeSpan.TryParseExact(val, format, isLocal ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture, out var res) ? res : null!;
            }
        }
        else if (targetType == typeof(DateTime))
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTime.TryParse(val, out var res) ? res : null!;
            }
            else
            {
                return DateTime.TryParseExact(val, format, isLocal ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture, DateTimeStyles.None, out var res) ? res : null!;
            }
        }
        else if (targetType == typeof(DateTimeOffset))
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTimeOffset.TryParse(val, out var res) ? res : null!;
            }
            else
            {
                return DateTimeOffset.TryParseExact(val, format, isLocal ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture, DateTimeStyles.None, out var res) ? res : null!;
            }
        }
        else if (targetType == typeof(long))
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTimeOffset.TryParse(val, out var res) ? res.ToUnixTimeMilliseconds() : null!;
            }
            else
            {
                return DateTimeOffset.TryParseExact(val, format, isLocal ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture, DateTimeStyles.None, out var res) ? res.ToUnixTimeMilliseconds() : null!;
            }
        }
        else if (targetType == typeof(int))
        {
            if (string.IsNullOrEmpty(format))
            {
                return DateTimeOffset.TryParse(val, out var res) ? res.ToUnixTimeSeconds() : null!;
            }
            else
            {
                return DateTimeOffset.TryParseExact(val, format, isLocal ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture, DateTimeStyles.None, out var res) ? res.ToUnixTimeSeconds() : null!;
            }
        }

        throw new NotSupportedException();
    }
}
