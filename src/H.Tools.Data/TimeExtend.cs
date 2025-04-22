using System;

namespace H.Tools.Data;

public static class TimeExtend
{
    public static DateTime TrimSeconds(this DateTime time)
    {
        return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Kind);
    }
}
