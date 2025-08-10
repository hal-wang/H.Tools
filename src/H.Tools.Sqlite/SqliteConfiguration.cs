using H.Tools.Config;
using SQLite;
using System;
using System.IO;
using System.Linq;

namespace H.Tools.Sqlite;

[Table("Configuration")]
file class ConfigItem
{
    [PrimaryKey]
    public string Key { get; set; }
    public string Value { get; set; }
}

public class SqliteConfiguration : Configuration, IDisposable
{
    private SQLiteConnection Connection => _initCon ?? _cbCon();

    private readonly SQLiteConnection _initCon;
    public SqliteConfiguration(SQLiteConnection con)
    {
        _initCon = con;
    }

    private readonly Func<SQLiteConnection> _cbCon;
    public SqliteConfiguration(Func<SQLiteConnection> con)
    {
        _cbCon = con;
    }

    public SqliteConfiguration(string path = null) : this(() => new SingleSqlite<ConfigItem>(path ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.db")))
    {
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
        base.Remove(key);
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}
