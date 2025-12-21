using H.Tools.Config;
using H.Tools.Data;
using Microsoft.Data.Sqlite;
using System;

namespace H.Tools.Sqlite;

public class SqliteConfiguration : Configuration, IDisposable
{
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

    public SqliteConfiguration(string path = "./config.db") : this(() => new SqliteConnection($"Data Source={path};Cache=2"))
    {
    }

    private void Init()
    {
        if (Connection.State != System.Data.ConnectionState.Open)
        {
            Connection.OpenAsync().Wait();
        }
        Connection.ExecuteNonQueryAsync("CREATE TABLE IF NOT EXISTS Configuration (Key TEXT PRIMARY KEY, Value TEXT);").Wait();
    }

    public override bool ContainsKey(string key) => Connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Configuration WHERE Key = '{key.Replace("'", "''")}';").Result > 0;

    protected override string GetValue(string key = null)
    {
        return Connection.ExecuteScalarAsync<string>($"SELECT Value FROM Configuration WHERE Key = '{key.Replace("'", "''")}';").Result ?? "";
    }

    protected override void SetValue(string value, string key)
    {
        Connection.ExecuteNonQueryAsync($"INSERT OR REPLACE INTO Configuration (Key, Value) VALUES ('{key.Replace("'", "''")}', '{value.Replace("'", "''")}');").Wait();
    }

    protected override void RemoveKey(string key)
    {
        Connection.ExecuteNonQueryAsync($"DELETE FROM Configuration WHERE Key = '{key.Replace("'", "''")}';").Wait();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}
