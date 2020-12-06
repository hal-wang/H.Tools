using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Hubery.Tools
{
    public static class DataExtend
    {
        #region DataTableToList
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
        #endregion


        #region ConvertValue
        private static object ConvertValue(object value, Type type)
        {
            if (value == null || value is DBNull || (value is string emptyStr && emptyStr == string.Empty))
            {
                return default;
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

        public static T GetValue<T>(this object obj)
        {
            return ConvertValue<T>(obj);
        }
        public static object GetValue(this object obj, Type type)
        {
            return ConvertValue(obj, type);
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


        public static string ToX2(this byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static byte[] Sha256(byte[] data)
        {
            return SHA256.Create().ComputeHash(data);
        }

        public static string Sha256(string str)
        {
            return Sha256(Encoding.UTF8.GetBytes(str)).ToX2();
        }
    }
}
