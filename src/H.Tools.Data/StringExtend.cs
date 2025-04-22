using System;

namespace H.Tools.Data;

public static class StringExtend
{
    public static string Hide(this string input, int start, int length, char ch = '*')
    {
        length = Math.Max(0, length);
        start = Math.Max(0, start);

        var chars = "".PadLeft(length, ch);
        if (string.IsNullOrEmpty(input) || input.Length <= length)
        {
            return chars;
        }

        start = Math.Min(start, input.Length);
        length = Math.Min(length, input.Length - start);
        return string.Concat(input.Substring(0, start), chars, input.Substring(start + length, input.Length - start - length));
    }
}
