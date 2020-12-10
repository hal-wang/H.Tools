using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HTools.HLog
{
    public abstract class HLogBase
    {
        public HLogBase(string path, string version, string operatingSystemVersion, string deviceModel, string appName, string platform)
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

        public void Log(HLogItem log)
        {
            using SqliteBase<HLogItem> sqliteHelper = new SqliteBase<HLogItem>(Path, storeDateTimeAsTicks: false);
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
            Log(new HLogItem()
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

        public List<HLogItem> GetLastLogs(int num)
        {
            using SqliteBase<HLogItem> sqliteHelper = new SqliteBase<HLogItem>(Path, storeDateTimeAsTicks: false);
            return sqliteHelper.Table<HLogItem>().OrderByDescending((item) => item.ID).Take(num).ToList();
        }
    }
}
