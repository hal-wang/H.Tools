using SQLite;

namespace Hubery.Tools
{
    public class SqliteHelper<T> : SQLiteConnection
    {
        public SqliteHelper(string databasePath, bool storeDateTimeAsTicks = true) :
            base(databasePath, storeDateTimeAsTicks)
        {
            CreateTable<T>();
        }
    }
}
