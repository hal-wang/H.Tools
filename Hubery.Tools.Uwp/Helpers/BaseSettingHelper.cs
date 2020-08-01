using System;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Hubery.Common.Uwp.Helpers
{
    public abstract class BaseSettingHelper
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

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

        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            _localSettings.Values[propertyName] = value;
        }
    }
}