using System.Configuration;
using System.Linq;

namespace H.Tools.Config;

public class AppSettingsConfiguration : Configuration
{
    public override bool ContainsKey(string key) => GetSection(GetConfiguration()).Settings.AllKeys.Contains(key);

    protected override string GetValue(string key) => GetSection(GetConfiguration()).Settings[key]?.Value;

    protected override void SetValue(string value, string key)
    {
        var config = GetConfiguration();
        var section = GetSection(config);
        section.Settings.Remove(key);
        section.Settings.Add(key, value);
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(SectionName);
    }

    public override void Remove(string key)
    {
        var config = GetConfiguration();
        var section = GetSection(config);
        section.Settings.Remove(key);
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(SectionName);
    }

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
