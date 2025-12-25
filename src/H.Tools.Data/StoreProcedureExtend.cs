using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace H.Tools.Data;

public static class StoreProcedureExtend
{
    public static List<Tuple<SqlDbType, Type>> SqlDbTypeMapping =>
    [
        new ( SqlDbType.NVarChar, typeof(string) ),
        new (SqlDbType.Char, typeof(string)),
        new (SqlDbType.VarChar, typeof(string)),
        new (SqlDbType.NChar, typeof(string)),
        new (SqlDbType.Xml, typeof(string)),
        new (SqlDbType.Binary, typeof(byte[])),
        new (SqlDbType.Bit, typeof(bool)),
        new (SqlDbType.Decimal, typeof(decimal)),
        new (SqlDbType.Money, typeof(decimal)),
        new (SqlDbType.SmallMoney, typeof(decimal)),
        new (SqlDbType.DateTime, typeof(DateTime)),
        new (SqlDbType.Time, typeof(DateTime)),
        new (SqlDbType.Date, typeof(DateTime)),
        new (SqlDbType.DateTime2, typeof(DateTime)),
        new (SqlDbType.DateTimeOffset, typeof(DateTimeOffset)),
        new (SqlDbType.Float, typeof(double)),
        new (SqlDbType.Float, typeof(float)),
        new (SqlDbType.UniqueIdentifier, typeof(Guid)),
        new (SqlDbType.SmallInt, typeof(short)),
        new (SqlDbType.Int, typeof(int)),
        new (SqlDbType.Int, typeof(uint)),
        new (SqlDbType.BigInt, typeof(long)),
        new (SqlDbType.BigInt, typeof(ulong)),
        new (SqlDbType.TinyInt, typeof(byte)),
        new (SqlDbType.TinyInt, typeof(sbyte)),
        new (SqlDbType.Image, typeof(byte[])),
    ];

    private static async Task<DbCommand> CreateCommand(this DbConnection dbConnection, string name, object? args)
    {
        if (dbConnection.State != ConnectionState.Open)
        {
            await dbConnection.OpenAsync();
        }
        var cmd = dbConnection.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = name;

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

    private static List<DbParameter> InitOutput(this DbCommand cmd, ExpandoObject? output)
    {
        var resultParameters = new List<DbParameter>();
        if (output != null)
        {
            KeyValuePair<string, object>[] kvps = [.. output];
            foreach (var kvp in output.ToArray())
            {
                var param = cmd.CreateParameter();
                param.Direction = ParameterDirection.Output;
                param.Size = int.MaxValue;
                param.ParameterName = kvp.Key;
                param.Value = SqlDbTypeMapping.First(item => item.Item2 == kvp.Value?.GetType()).Item1;

                cmd.Parameters.Add(param);
                resultParameters.Add(param);
            }
        }
        return resultParameters;
    }

    private static void SetOutput(List<DbParameter> resultParameters, ExpandoObject? output)
    {
        if (output == null) return;

        IDictionary<string, object> op = output;
        resultParameters.ForEach(param =>
        {
            op[param.ParameterName] = param.Value;
        });
    }

    public async static Task ExecuteNonQueryProcAsync(this DbConnection dbConnection, string name, object? args = null, ExpandoObject? output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        await cmd.ExecuteNonQueryAsync();
        SetOutput(resultParameters, output);
    }

    public async static Task<DataTable> ExecuteProcAsync(this DbConnection dbConnection, string name, object? args = null, ExpandoObject? output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        using var reader = await cmd.ExecuteReaderAsync();

        var dt = new DataTable();
        dt.Load(reader);
        SetOutput(resultParameters, output);
        return dt;
    }

    public async static Task<List<T>> ExecuteProcAsync<T>(this DbConnection dbConnection, string name, object? args = null, ExpandoObject? output = null) where T : class, new()
    {
        using var dt = await dbConnection.ExecuteProcAsync(name, args, output);
        return dt.ToList<T>();
    }

    public async static Task<T> ExecuteScalarProcAsync<T>(this DbConnection dbConnection, string name, object? args = null, ExpandoObject? output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        var obj = (T)await cmd.ExecuteScalarAsync();
        SetOutput(resultParameters, output);
        return obj;
    }

    public async static Task<object> ExecuteScalarProcAsync(this DbConnection dbConnection, string name, object? args = null, ExpandoObject? output = null)
    {
        return await dbConnection.ExecuteScalarProcAsync<object>(name, args, output);
    }
}
