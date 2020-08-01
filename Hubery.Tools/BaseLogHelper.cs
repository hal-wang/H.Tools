using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hubery.Tools
{
    public abstract class BaseLogHelper
    {
        private readonly string _path;

        public BaseLogHelper(string path)
        {
            _path = path;
        }

        public bool Log(Log log)
        {
            try
            {
                using (SqliteHelper<Log> sqliteHelper = new SqliteHelper<Log>(_path, storeDateTimeAsTicks: false))
                {
                    sqliteHelper.Insert(log);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
         
        public List<Log> GetLastLogs(int num)
        {
            using (SqliteHelper<Log> sqliteHelper = new SqliteHelper<Log>(_path, storeDateTimeAsTicks: false))
            {
                return sqliteHelper.Table<Log>().OrderByDescending((item) => item.ID).Take(num).ToList();
            }
        }
    }
}