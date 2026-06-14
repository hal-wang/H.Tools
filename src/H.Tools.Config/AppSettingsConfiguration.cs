using System.Configuration;
using System.Linq;

namespace H.Tools.Config;

public class AppSettingsConfiguration : Configuration
{
    protected virtual bool UseLock => false;
    private static readonly object _configLock = new();

    #region ContainsKey
    public override bool ContainsKey(string key)
    {
        if (UseLock)
        {
            lock (_configLock)
            {
                return ContainsKeyInternal(key);
            }
        }
        else
        {
            return ContainsKeyInternal(key);
        }
    }

    public bool ContainsKeyInternal(string key) => GetSection(GetConfiguration()).Settings.AllKeys.Contains(key);
    #endregion

    #region GetValue
    protected override string GetValue(string key)
    {
        if (UseLock)
        {
            lock (_configLock)
            {
                return GetValueInternal(key);
            }
        }
        else
        {
            return GetValueInternal(key);
        }
    }

    private string GetValueInternal(string key) => GetSection(GetConfiguration()).Settings[key]?.Value ?? string.Empty;
    #endregion

    #region SetValue
    protected override void SetValue(string value, string key)
    {
        if (UseLock)
        {
            lock (_configLock)
            {
                SetValueInternal(value, key);
            }
        }
        else
        {
            SetValueInternal(value, key);
        }
    }

    private void SetValueInternal(string value, string key)
    {
        var config = GetConfiguration();
        var section = GetSection(config);
        section.Settings.Remove(key);
        section.Settings.Add(key, value);
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(SectionName);
    }
    #endregion

    #region RemoveKey
    protected override void RemoveKey(string key)
    {
        if (UseLock)
        {
            lock (_configLock)
            {
                RmoveInternal(key);
            }
        }
        else
        {
            RmoveInternal(key);
        }
    }

    private void RmoveInternal(string key)
    {
        var config = GetConfiguration();
        var section = GetSection(config);
        section.Settings.Remove(key);
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(SectionName);
    }
    #endregion

    private AppSettingsSection GetSection(System.Configuration.Configuration config)
    {
        var section = (AppSettingsSection)config.GetSection(SectionName);
        if (section == null)
        {
            section = new AppSettingsSection();
            config.Sections.Add(SectionName, section);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(SectionName);
        }
        return section;
    }

    protected virtual string SectionName { get; } = "AppSettings";

    protected virtual System.Configuration.Configuration GetConfiguration()
    {
        return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    }
}
