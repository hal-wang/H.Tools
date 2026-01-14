using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace H.Tools.Data;

public static class DbQueryExtend
{
    public static async Task<int> ExecuteNonQueryAsync(this DbConnection dbConnection, string sql)
    {
        using var cmd = dbConnection.CreateCommand();
        cmd.CommandText = sql;
        return await cmd.ExecuteNonQueryAsync();
    }

    public async static Task<DataTable> ExecuteQueryAsync(this DbConnection dbConnection, string sql)
    {
        using var cmd = dbConnection.CreateCommand();
        cmd.CommandText = sql;

        using var reader = await cmd.ExecuteReaderAsync();
        var dt = new DataTable();
        dt.Load(reader);
        return dt;
    }

    public static async Task<IReadOnlyList<T>> ExecuteQueryAsync<T>(this DbConnection dbConnection, string sql) where T : class, new()
    {
        using var dt = await dbConnection.ExecuteQueryAsync(sql);
        return dt.ToList<T>();
    }

    public static async Task<T?> ExecuteScalarAsync<T>(this DbConnection dbConnection, string sql)
    {
        using var cmd = dbConnection.CreateCommand();
        cmd.CommandText = sql;
        var obj = await cmd.ExecuteScalarAsync();
        if (obj == DBNull.Value) return default;

        try
        {
            return (T)obj!;
        }
        catch
        {
            return (T)Convert.ChangeType(obj, typeof(T))!;
        }
    }
}
