﻿using System.Configuration;
using System.Linq;

namespace H.Tools.Config;

public abstract class SettingConfigBase : ConfigBase
{
    public override bool ContainsKey(string key) => ConfigurationManager.AppSettings.AllKeys.Contains(key);

    protected override string GetValue(string key) => ConfigurationManager.AppSettings[key];

    protected override void SetValue(string value, string key)
    {
        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        cfa.AppSettings.Settings.Remove(key);
        cfa.AppSettings.Settings.Add(key, value);
        cfa.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }

    public override void Remove(string key)
    {
        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        cfa.AppSettings.Settings.Remove(key);
        cfa.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}
