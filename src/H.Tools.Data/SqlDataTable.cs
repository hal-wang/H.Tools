using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Xml.Linq;

namespace H.Tools.Data;

public static class SqlDataTable
{
    public static List<T> ToList<T>(this DataTable table) where T : class, new()
    {
        var result = new List<T>();
        var propertys = typeof(T).GetProperties();
        foreach (DataRow dr in table.Rows)
        {
            var item = new T();
            result.Add(item);
            foreach (var pi in propertys)
            {
                if (!table.Columns.Contains(pi.Name)) continue;
                var value = dr[pi.Name];
                if (value is DBNull || value == null) continue;
                pi.SetValue(item, value);
            }
        }
        return result;
    }

    public static List<dynamic> ToList(this DataTable table)
    {
        var result = new List<dynamic>();
        foreach (DataRow dr in table.Rows)
        {
            dynamic item = new ExpandoObject();
            for (var i = 0; i < table.Columns.Count; i++)
            {
                ((IDictionary<string, object>)item).Add(table.Columns[i].ColumnName, dr[i] == DBNull.Value ? null : dr[i]);
            }
            result.Add(item);
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
