using H.Tools.Config;
using H.Tools.Data;
using Microsoft.Data.Sqlite;
using System;

namespace H.Tools.Sqlite;

public class SqliteConfiguration : Configuration, IDisposable
{
    private readonly bool _ownsConnection = false;
    private SqliteConnection Connection => _initCon ?? (_cbCon ??= _conCb());

    private readonly SqliteConnection _initCon;
    public SqliteConfiguration(SqliteConnection con)
    {
        _initCon = con;
        Init();
    }

    private SqliteConnection _cbCon;
    private readonly Func<SqliteConnection> _conCb;
    public SqliteConfiguration(Func<SqliteConnection> con)
    {
        _conCb = con;
        Init();
    }

    public SqliteConfiguration(string path = "./config.db") : this(() => new SqliteConnection($"Data Source={path};Pooling=true"))
    {
        _ownsConnection = true;
    }

    protected virtual string SectionName { get; } = "Configuration";

    private void Init()
    {
        if (Connection.State != System.Data.ConnectionState.Open)
        {
            Connection.OpenAsync().GetAwaiter().GetResult();
        }
        Connection.ExecuteNonQueryAsync($"CREATE TABLE IF NOT EXISTS \"{SectionName}\" (Key TEXT PRIMARY KEY, Value TEXT);").GetAwaiter().GetResult();
    }

    public override bool ContainsKey(string key) => Connection.ExecuteScalarAsync<int>(
        $"SELECT COUNT(*) FROM \"{SectionName}\" WHERE Key = @Key;",
        new
        {
            Key = key,
        }).GetAwaiter().GetResult() > 0;

    protected override string GetValue(string key)
    {
        return Connection.ExecuteScalarAsync<string>(
            $"SELECT Value FROM \"{SectionName}\" WHERE Key = @Key;",
            new
            {
                Key = key,
            }).GetAwaiter().GetResult() ?? "";
    }

    protected override void SetValue(string value, string key)
    {
        Connection.ExecuteNonQueryAsync(
            $"INSERT OR REPLACE INTO \"{SectionName}\" (Key, Value) VALUES (@Key, @Value);",
            new
            {
                Key = key,
                Value = value,
            }).GetAwaiter().GetResult();
    }

    protected override void RemoveKey(string key)
    {
        Connection.ExecuteNonQueryAsync(
            $"DELETE FROM \"{SectionName}\" WHERE Key = @Key;",
            new
            {
                Key = key,
            }).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        if (_ownsConnection)
        {
            Connection.Dispose();
        }
    }
}
