using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace H.Tools.Data;

public static class StoreProcedureExtend
{
    public static List<Tuple<SqlDbType, Type>> SqlDbTypeMapping => new()
    {
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
    };

    #region Microsoft.Data.SqlClient
    private static async Task<Microsoft.Data.SqlClient.SqlCommand> CreateCommand(this Microsoft.Data.SqlClient.SqlConnection dbConnection, string name, object args)
    {
        if (dbConnection.State != ConnectionState.Open)
        {
            await dbConnection.OpenAsync();
        }
        var cmd = new Microsoft.Data.SqlClient.SqlCommand(name, dbConnection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (args != null)
        {
            var properties = args.GetType().GetProperties();
            foreach (var property in properties)
            {
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(property.Name, property.GetValue(args)));
            }
        }
        return cmd;
    }

    private static List<Microsoft.Data.SqlClient.SqlParameter> InitOutput(this Microsoft.Data.SqlClient.SqlCommand cmd, ExpandoObject output)
    {
        var resultParameters = new List<Microsoft.Data.SqlClient.SqlParameter>();
        if (output != null)
        {
            KeyValuePair<string, object>[] kvps = [.. output];
            foreach (var kvp in output.ToArray())
            {
                var param = new Microsoft.Data.SqlClient.SqlParameter(kvp.Key, SqlDbTypeMapping.First(item => item.Item2 == kvp.Value?.GetType()).Item1)
                {
                    Direction = ParameterDirection.Output,
                    Size = int.MaxValue,
                };
                cmd.Parameters.Add(param);
                resultParameters.Add(param);
            }
        }
        return resultParameters;
    }

    private static void SetOutput(List<Microsoft.Data.SqlClient.SqlParameter> resultParameters, ExpandoObject output)
    {
        if (output == null) return;

        IDictionary<string, object> op = output;
        resultParameters.ForEach(param =>
        {
            op[param.ParameterName] = param.Value;
        });
    }

    public async static Task ExecuteNonQueryProcAsync(this Microsoft.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        await cmd.ExecuteNonQueryAsync();
        SetOutput(resultParameters, output);
    }

    public async static Task<DataTable> ExecuteProcAsync(this Microsoft.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        using var reader = await cmd.ExecuteReaderAsync();

        var dt = new DataTable();
        dt.Load(reader);
        SetOutput(resultParameters, output);
        return dt;
    }

    public async static Task<List<T>> ExecuteProcAsync<T>(this Microsoft.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null) where T : class, new()
    {
        using var dt = await dbConnection.ExecuteProcAsync(name, args, output);
        return dt.ToList<T>();
    }

    public async static Task<T> ExecuteScalarProcAsync<T>(this Microsoft.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        var obj = (T)await cmd.ExecuteScalarAsync();
        SetOutput(resultParameters, output);
        return obj;
    }

    public async static Task<object> ExecuteScalarProcAsync(this Microsoft.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        return await dbConnection.ExecuteScalarProcAsync<object>(name, args, output);
    }
    #endregion

    #region System.Data.SqlClient
    private static async Task<System.Data.SqlClient.SqlCommand> CreateCommand(this System.Data.SqlClient.SqlConnection dbConnection, string name, object args)
    {
        if (dbConnection.State != ConnectionState.Open)
        {
            await dbConnection.OpenAsync();
        }
        var cmd = new System.Data.SqlClient.SqlCommand(name, dbConnection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (args != null)
        {
            var properties = args.GetType().GetProperties();
            foreach (var property in properties)
            {
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter(property.Name, property.GetValue(args)));
            }
        }
        return cmd;
    }

    private static List<System.Data.SqlClient.SqlParameter> InitOutput(this System.Data.SqlClient.SqlCommand cmd, ExpandoObject output)
    {
        var resultParameters = new List<System.Data.SqlClient.SqlParameter>();
        if (output != null)
        {
            KeyValuePair<string, object>[] kvps = [.. output];
            foreach (var kvp in output.ToArray())
            {
                var param = new System.Data.SqlClient.SqlParameter(kvp.Key, SqlDbTypeMapping.First(item => item.Item2 == kvp.Value?.GetType()).Item1)
                {
                    Direction = ParameterDirection.Output,
                    Size = int.MaxValue,
                };
                cmd.Parameters.Add(param);
                resultParameters.Add(param);
            }
        }
        return resultParameters;
    }

    private static void SetOutput(List<System.Data.SqlClient.SqlParameter> resultParameters, ExpandoObject output)
    {
        if (output == null) return;

        IDictionary<string, object> op = output;
        resultParameters.ForEach(param =>
        {
            op[param.ParameterName] = param.Value;
        });
    }

    public async static Task ExecuteNonQueryProcAsync(this System.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        await cmd.ExecuteNonQueryAsync();
        SetOutput(resultParameters, output);
    }

    public async static Task<DataTable> ExecuteProcAsync(this System.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        using var reader = await cmd.ExecuteReaderAsync();

        var dt = new DataTable();
        dt.Load(reader);
        SetOutput(resultParameters, output);
        return dt;
    }

    public async static Task<List<T>> ExecuteProcAsync<T>(this System.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null) where T : class, new()
    {
        using var dt = await dbConnection.ExecuteProcAsync(name, args, output);
        return dt.ToList<T>();
    }

    public async static Task<T> ExecuteScalarProcAsync<T>(this System.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        using var cmd = await dbConnection.CreateCommand(name, args);
        var resultParameters = cmd.InitOutput(output);
        var obj = (T)await cmd.ExecuteScalarAsync();
        SetOutput(resultParameters, output);
        return obj;
    }

    public async static Task<object> ExecuteScalarProcAsync(this System.Data.SqlClient.SqlConnection dbConnection, string name, object args = null, ExpandoObject output = null)
    {
        return await dbConnection.ExecuteScalarProcAsync<object>(name, args, output);
    }
    #endregion
}
