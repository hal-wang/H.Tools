using OneOf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Xml.Linq;

namespace H.Tools.Data;

public static class SqlDataTable
{
    public static List<T> ToList<T>(this DataTable table, Func<string, OneOf<string, string[]>> nameReplace = null) where T : class, new()
    {
        var result = new List<T>();
        var properties = typeof(T).GetProperties();
        foreach (DataRow dr in table.Rows)
        {
            var item = dr.ToObject<T>(properties, nameReplace);
            result.Add(item);
        }
        return result;
    }

    public static T ToObject<T>(this DataRow row, Func<string, OneOf<string, string[]>> nameReplace = null) where T : class, new()
    {
        var properties = typeof(T).GetProperties();
        return row.ToObject<T>(properties, nameReplace);
    }

    private static string GetName(this DataColumnCollection columns, string name)
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

    private static string GetName(this DataColumnCollection columns, string name, Func<string, OneOf<string, string[]>> nameReplace)
    {
        var replaceNames = nameReplace(name);
        if (replaceNames.IsT0)
        {
            var result = columns.GetName(replaceNames.AsT0);
            if (!string.IsNullOrEmpty(name))
            {
                return result;
            }
        }
        else
        {
            foreach (var replaceName in replaceNames.AsT1)
            {
                var result = columns.GetName(replaceName);
                if (!string.IsNullOrEmpty(name))
                {
                    return result;
                }
            }
        }
        return columns.GetName(name);
    }

    private static T ToObject<T>(this DataRow row, PropertyInfo[] properties, Func<string, OneOf<string, string[]>> nameReplace = null) where T : class, new()
    {
        var result = new T();
        foreach (var pi in properties)
        {
            var name = row.Table.Columns.GetName(pi.Name, nameReplace);
            if (string.IsNullOrEmpty(name)) continue;

            var value = row[name];
            if (value is DBNull || value == null) continue;
            pi.SetValue(result, value);
        }
        return result;
    }

    public static List<dynamic> ToList(this DataTable table, Func<string, OneOf<string, string[]>> nameReplace = null)
    {
        var result = new List<dynamic>();
        foreach (DataRow dr in table.Rows)
        {
            dynamic item = dr.ToObject(nameReplace);
            result.Add(item);
        }
        return result;
    }

    public static dynamic ToObject(this DataRow row, Func<string, OneOf<string, string[]>> nameReplace = null)
    {
        dynamic result = new ExpandoObject();
        for (var i = 0; i < row.Table.Columns.Count; i++)
        {
            var name = row.Table.Columns.GetName(row.Table.Columns[i].ColumnName, nameReplace);
            if (string.IsNullOrEmpty(name)) continue;

            ((IDictionary<string, object>)result).Add(name, row[i] == DBNull.Value ? null : row[i]);
        }
        return result;
    }

    #region ConvertValue
    private static object ConvertValue(object value, Type type)
    {
        if (value == null || value is DBNull || (value is string emptyStr && emptyStr == string.Empty))
        {
            return default!;
        }
        else if (value.GetType() == type)
        {
            return value;
        }
        else if (value is string str && type == typeof(bool))
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
            return Convert.ChangeType(value, type);
        }
    }

    private static T ConvertValue<T>(object value)
    {
        return (T)ConvertValue(value, typeof(T));
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
