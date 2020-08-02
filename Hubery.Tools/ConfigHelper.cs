using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Hubery.Tools
{
    public class ConfigHelper
    {
        public T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("Key不能为空");

            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                Set(defaultValue, key);
                return defaultValue;
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        public void Set(object value, [CallerMemberName] string key = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("Key不能为空");

            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings.Remove(key);
            cfa.AppSettings.Settings.Add(key, value?.ToString());
            cfa.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
