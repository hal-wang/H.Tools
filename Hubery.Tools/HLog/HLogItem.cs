using SQLite;
using System;
using System.Text;

namespace Hubery.Tools.HLog
{
    public class HLogItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Version { get; set; }

        public DateTime Time { get; set; }

        public string OperatingSystemVersion { get; set; }

        public string DeviceModel { get; set; }

        public string AppName { get; set; }

        public string Platform { get; set; }


        public string Source { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Time：");
            sb.Append(Time.ToString());
            sb.AppendLine();

            sb.Append("Version：");
            sb.Append(Version);
            sb.AppendLine();

            sb.Append("OperatingSystemVersion：");
            sb.Append(OperatingSystemVersion);
            sb.AppendLine();

            sb.Append("DeviceModel：");
            sb.Append(DeviceModel);
            sb.AppendLine();

            sb.Append("AppName：");
            sb.Append(AppName);
            sb.AppendLine();

            sb.Append("Platform：");
            sb.Append(Platform);
            sb.AppendLine();

            sb.Append("Source：");
            sb.Append(Source);
            sb.AppendLine();

            sb.Append("Content：");
            sb.Append(Content);

            return sb.ToString();
        }
    }
}