using H.Tools.Config;
using OneOf;
using SQLite;
using System;
using System.IO;
using System.Linq;

namespace H.Tools.Sqlite;

[Table("Config")]
file class ConfigItem
{
    [PrimaryKey]
    public string Key { get; set; }
    public string Value { get; set; }
}

public abstract class SqliteConfigBase : ConfigBase, IDisposable
{
    private OneOf<SQLiteConnection, Func<SQLiteConnection>> _con;
    private SQLiteConnection Connection => _con.IsT0 ? _con.AsT0 : _con.AsT1();

    public SqliteConfigBase() { }

    public SqliteConfigBase(OneOf<SQLiteConnection, Func<SQLiteConnection>> con)
    {
        _con = con;
    }

    public SqliteConfigBase(string path)
    {
        Connect(path);
    }

    public void Connect(string path)
    {
        Connection?.Dispose();
        _con = new SqliteBase<ConfigItem>(path ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config.db"));
    }

    public override bool ContainsKey(string key) => Connection.Table<ConfigItem>().Any(item => item.Key == key);

    protected override string GetValue(string key = null)
    {
        return Connection.Find<ConfigItem>(key).Value;
    }

    protected override void SetValue(string value, string key)
    {
        Connection.InsertOrReplace(new ConfigItem()
        {
            Key = key,
            Value = value
        });
    }

    public override void Remove(string key)
    {
        Connection.Table<ConfigItem>().Where(item => item.Key == key).Delete();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}
