using System.Text.RegularExpressions;

namespace Hubery.Tools
{
    public static class RegexHelper
    {
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+", RegexOptions.IgnoreCase);
        }

        public static bool IsPhone(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^(12|13|14|15|16|17|18|19)\d{9}$", RegexOptions.IgnoreCase);
        }

        public static bool IsCaptcha(string captcha, int length = 6)
        {
            return Regex.IsMatch(captcha, $"^\\d{{{length}}}$", RegexOptions.IgnoreCase);
        }
    }
}
