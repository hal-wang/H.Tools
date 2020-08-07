using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace Hubery.Tools
{
    public static class DataExtend
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
    }

}
