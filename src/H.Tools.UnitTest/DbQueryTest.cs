using H.Tools.Data;
using Microsoft.Data.Sqlite;

namespace H.Tools.UnitTest;

[TestClass]
public class DbQueryTest
{
    [TestMethod]
    public async System.Threading.Tasks.Task ExecuteNonQueryAsync()
    {
        using var con = new SqliteConnection($"Data Source=./test.db;Cache=2");
        await con.OpenAsync();
        await con.ExecuteNonQueryAsync("SELECT * FROM sqlite_master");
    }

    [TestMethod]
    public async System.Threading.Tasks.Task ExecuteScalarAsync()
    {
        using var con = new SqliteConnection($"Data Source=./test.db;Cache=2");
        await con.OpenAsync();
        var str = await con.ExecuteScalarAsync<string?>("SELECT * FROM sqlite_master");
        Assert.IsNull(str);
    }
}
