using HTools.Config;
using Windows.Storage;

namespace HTools.Uwp.Helpers {
    public abstract class SettingConfigBase : ConfigBase<object> {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public override bool ContainsKey(string key) => _localSettings.Values.ContainsKey(key);

        protected override object GetValue(string key) => _localSettings.Values[key];

        protected override void SetValue(object value, string key) => _localSettings.Values[key] = value;

        public override void Remove(string key) => _localSettings.Values.Remove(key);
    }
}