using System;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Hubery.Tools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SettingConfigBase
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected T Get<T>(T defaultValue = default, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new Exception();
            }

            object result = _localSettings.Values[propertyName];
            if (result == null)
            {
                return defaultValue;
            }
            else
            {
                return (T)result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            _localSettings.Values[propertyName] = value;
        }
    }
}