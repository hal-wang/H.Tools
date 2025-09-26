using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace H.Tools.Data;

public static class SqlDataTable
{
    public static List<T> ToList<T>(this DataTable table) where T : class, new()
    {
        var result = new List<T>();
        var properties = typeof(T).GetProperties();
        foreach (DataRow dr in table.Rows)
        {
            var item = dr.ToObject<T>(properties);
            result.Add(item);
        }
        return result;
    }

    public static T ToObject<T>(this DataRow row) where T : class, new()
    {
        var properties = typeof(T).GetProperties();
        return row.ToObject<T>(properties);
    }

    private static string? GetName(this DataColumnCollection columns, string name)
    {
        if (columns.Contains(name))
        {
            return name;
        }
        else
        {
            foreach (DataColumn col in columns)
            {
                if (string.Equals(col.ColumnName, name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return col.ColumnName;
                }
            }
        }
        return null;
    }

    private static T ToObject<T>(this DataRow row, PropertyInfo[] properties) where T : class, new()
    {
        var result = new T();
        foreach (var pi in properties)
        {
            var name = row.Table.Columns.GetName(pi.GetCustomAttributes<FieldName>().FirstOrDefault()?.Name ?? pi.Name);
            if (string.IsNullOrEmpty(name)) continue;

            var value = row[name];
            if (value is DBNull || value == null) continue;
            pi.SetValue(result, value);
        }
        return result;
    }

    public static List<dynamic> ToList(this DataTable table)
    {
        var result = new List<dynamic>();
        foreach (DataRow dr in table.Rows)
        {
            dynamic item = dr.ToObject();
            result.Add(item);
        }
        return result;
    }

    public static dynamic ToObject(this DataRow row)
    {
        dynamic result = new ExpandoObject();
        for (var i = 0; i < row.Table.Columns.Count; i++)
        {
            var name = row.Table.Columns.GetName(row.Table.Columns[i].ColumnName)!;
            if (string.IsNullOrEmpty(name)) continue;

            ((IDictionary<string, object?>)result).Add(name, row[i] == DBNull.Value ? null : row[i]);
        }
        return result;
    }

    public static string[] GetColumns(this DataTable dt)
    {
        var result = new string[dt.Columns.Count];
        for (var i = 0; i < dt.Columns.Count; i++)
        {
            result[i] = dt.Columns[i].ColumnName;
        }
        return result;
    }

    #region ConvertValue
    private static object ConvertValueToObject<T>(object value)
    {
        var type = typeof(T);
        if (value == null || value is DBNull || (value is string emptyStr && emptyStr == string.Empty))
        {
            return default!;
        }
        else if (value.GetType() == type)
        {
            return value;
        }
        else if (value is string str && (type == typeof(bool) || type == typeof(bool?)))
        {
            if (string.Equals(str, "y", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(str, "yes", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(str, "true", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(str, "1", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            try
            {
                return (T)value;
            }
            catch
            {
                return Convert.ChangeType(value, type);
            }
        }
    }

    private static T ConvertValue<T>(object value)
    {
        return (T)ConvertValueToObject<T>(value);
    }

    public static T GetValue<T>(this IDataReader dataReader, string fieldName)
    {
        return ConvertValue<T>(dataReader[fieldName]);
    }

    public static T GetValue<T>(this IDataReader dataReader, int index)
    {
        return ConvertValue<T>(dataReader[index]);
    }

    public static T GetValue<T>(this DataRow dataRow, string fieldName)
    {
        return ConvertValue<T>(dataRow[fieldName]);
    }

    public static T GetValue<T>(this DataRow dataRow, int index)
    {
        return ConvertValue<T>(dataRow[index]);
    }

    public static T GetValue<T>(this XAttribute xAttribute)
    {
        return ConvertValue<T>(xAttribute.Value);
    }
    #endregion
}
