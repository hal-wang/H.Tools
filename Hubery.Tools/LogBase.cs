using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hubery.Tools
{
    public abstract class LogBase
    {
        public LogBase(string path, string version, string operatingSystemVersion, string deviceModel, string appName, string platform)
        {
            Path = path;
            Version = version;
            OperatingSystemVersion = operatingSystemVersion;
            DeviceModel = deviceModel;
            AppName = appName;
            Platform = platform;
        }

        public string Path { get; set; }
        public string Version { get; set; }
        public string OperatingSystemVersion { get; set; }
        public string DeviceModel { get; set; }
        public string AppName { get; set; }
        public string Platform { get; set; }

        public void Log(Log log)
        {
            using SqliteBase<Log> sqliteHelper = new SqliteBase<Log>(Path, storeDateTimeAsTicks: false);
            sqliteHelper.Insert(log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="source"></param>
        public void Log(Exception ex, [CallerMemberName] string source = null)
        {
            Log(ex?.ToString(), source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="source"></param>
        public void Log(string content, [CallerMemberName] string source = null)
        {
            Log(new Log()
            {
                Source = source,
                Content = content,
                Time = DateTime.Now,
                Version = Version,
                OperatingSystemVersion = OperatingSystemVersion,
                DeviceModel = DeviceModel,
                AppName = AppName,
                Platform = Platform,
            });
        }

        public List<Log> GetLastLogs(int num)
        {
            using SqliteBase<Log> sqliteHelper = new SqliteBase<Log>(Path, storeDateTimeAsTicks: false);
            return sqliteHelper.Table<Log>().OrderByDescending((item) => item.ID).Take(num).ToList();
        }
    }
}
