using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Hubery.Tools.Config
{
    public class SqliteConfigBase
    {
        public abstract class SettingHelper : IDisposable
        {
            private readonly SqliteBase<ConfigItem> _sqliteConnection;

            public SettingHelper(string path = null)
            {
                _sqliteConnection = new SqliteBase<ConfigItem>(path ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config.db"));
            }

            public string this[string key]
            {
                get
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        throw new NoNullAllowedException();
                    }

                    var result = _sqliteConnection.Find<ConfigItem>(key);
                    if (result == null)
                        return "";
                    else
                        return result.Value;
                }
                set
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        throw new NoNullAllowedException();
                    }

                    if (value == null)
                        value = "";

                    _sqliteConnection.InsertOrReplace(new ConfigItem()
                    {
                        Key = key,
                        Value = value
                    });
                }
            }


            public T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
            {
                if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

                var value = _sqliteConnection.Find<ConfigItem>(key);
                if (value == null)
                {
                    Set(defaultValue, key);
                    return defaultValue;
                }
                else
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }

            public void Set(object value, [CallerMemberName] string key = null)
            {
                if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

                _sqliteConnection.InsertOrReplace(new ConfigItem()
                {
                    Key = key,
                    Value = value.ToString()
                });
            }

            public void Dispose()
            {
                _sqliteConnection.Dispose();
            }
        }
    }
}
