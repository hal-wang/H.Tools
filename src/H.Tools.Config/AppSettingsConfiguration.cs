using System.Configuration;
using System.Linq;

namespace H.Tools.Config;

public class AppSettingsConfiguration : Configuration
{
    public override bool ContainsKey(string key) => GetSection(GetConfiguration()).Settings.AllKeys.Contains(key);

    protected virtual bool UseLock => false;
    private static readonly object _lock = new();

    #region GetValue
    protected override string GetValue(string key)
    {
        if (UseLock)
        {
            lock (_lock)
            {
                return GetValueInternal(key);
            }
        }
        else
        {
            return GetValueInternal(key);
        }
    }

    private string GetValueInternal(string key) => GetSection(GetConfiguration()).Settings[key]?.Value;
    #endregion

    #region SetValue
    protected override void SetValue(string value, string key)
    {
        if (UseLock)
        {
            lock (_lock)
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

    #region Remove
    public override void Remove(string key)
    {
        if (UseLock)
        {
            lock (_lock)
            {
                RmoveInternal(key);
                base.Remove(key);
            }
        }
        else
        {
            RmoveInternal(key);
            base.Remove(key);
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
