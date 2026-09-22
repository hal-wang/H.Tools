using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace H.Tools.Data;

public static class DbQueryExtend
{
    public static async Task<int> ExecuteNonQueryAsync(this DbConnection dbConnection, string sql, object? args = null)
    {
        using var cmd = await dbConnection.CreateCommand(sql, args);
        return await cmd.ExecuteNonQueryAsync();
    }

    public static async Task<DataTable> ExecuteQueryAsync(this DbConnection dbConnection, string sql, object? args = null)
    {
        using var cmd = await dbConnection.CreateCommand(sql, args);
        using var reader = await cmd.ExecuteReaderAsync();
        var dt = new DataTable();
        await Task.Run(() => dt.Load(reader));
        return dt;
    }

    public static async Task<IReadOnlyList<T>> ExecuteQueryAsync<T>(this DbConnection dbConnection, string sql, object? args = null) where T : class, new()
    {
        using var dt = await dbConnection.ExecuteQueryAsync(sql, args);
        return dt.ToList<T>();
    }

    public static async Task<T?> ExecuteScalarAsync<T>(this DbConnection dbConnection, string sql, object? args = null)
    {
        using var cmd = await dbConnection.CreateCommand(sql, args);
        var obj = await cmd.ExecuteScalarAsync();

        if (obj == DBNull.Value || obj == null)
            return default;

        if (obj is T t)
            return t;

        return (T)Convert.ChangeType(obj, typeof(T));
    }

    private static async Task<DbCommand> CreateCommand(this DbConnection dbConnection, string sql, object? args)
    {
        if (dbConnection.State != ConnectionState.Open)
        {
            await dbConnection.OpenAsync();
        }
        var cmd = dbConnection.CreateCommand();
        cmd.CommandText = sql;

        if (args != null && args is IDictionary<string, string> strDictArgs)
        {
            foreach (var kvp in strDictArgs)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = kvp.Key;
                param.Value = kvp.Value == null ? DBNull.Value : kvp.Value;
                cmd.Parameters.Add(param);
            }
        }
        else if (args != null && args is IDictionary<string, object> objDictArgs)
        {
            foreach (var kvp in objDictArgs)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = kvp.Key;
                param.Value = kvp.Value ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }
        }
        else if (args != null)
        {
            var properties = args.GetType().GetProperties();
            foreach (var property in properties)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = property.Name;
                param.Value = property.GetValue(args) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }
        }
        return cmd;
    }
}
