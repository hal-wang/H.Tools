using SQLite;
using System;
using System.IO;
using System.Linq;

namespace HTools.Config
{
    public abstract class SqliteConfigBase : ConfigBase<string>, IDisposable
    {
        private SQLiteConnection _con;

        public SqliteConfigBase() { }

        public SqliteConfigBase(SQLiteConnection con)
        {
            _con = con;
        }

        public SqliteConfigBase(string path)
        {
            Connect(path);
        }

        public void Connect(string path)
        {
            _con?.Dispose();
            _con = new SqliteBase<ConfigItem>(path ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config.db"));
        }

        public override bool ContainsKey(string key) => _con.Table<ConfigItem>().Any(item => item.Key == key);

        protected override string GetValue(string key = null)
        {
            return _con.Find<ConfigItem>(key).Value;
        }

        protected override void SetValue(string value, string key)
        {
            _con.InsertOrReplace(new ConfigItem()
            {
                Key = key,
                Value = value
            });
        }

        public override void Remove(string key)
        {
            _con.Table<ConfigItem>().Where(item => item.Key == key).Delete();
        }

        public void Dispose()
        {
            _con.Dispose();
        }
    }
}
