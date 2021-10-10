using System;
using System.IO;
using System.Linq;

namespace HTools.Config {
    public abstract class SqliteConfigBase : ConfigBase<string>, IDisposable {
        private readonly SqliteBase<ConfigItem> _sqliteConnection;

        public SqliteConfigBase(string path = null) {
            _sqliteConnection = new SqliteBase<ConfigItem>(path ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config.db"));
        }

        public override bool ContainsKey(string key) => _sqliteConnection.Table<ConfigItem>().Any(item => item.Key == key);

        protected override string GetValue(string key = null) {
            return _sqliteConnection.Find<ConfigItem>(key).Value;
        }

        protected override void SetValue(string value, string key) {
            _sqliteConnection.InsertOrReplace(new ConfigItem() {
                Key = key,
                Value = value
            });
        }

        public override void Remove(string key) {
            _sqliteConnection.Table<ConfigItem>().Where(item => item.Key == key).Delete();
        }

        public void Dispose() {
            _sqliteConnection.Dispose();
        }
    }
}
