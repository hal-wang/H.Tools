using SQLite;

namespace HTools
{
    public class SqliteBase<T> : SQLiteConnection
    {
        public SqliteBase(string databasePath, bool storeDateTimeAsTicks = true) :
            base(databasePath, storeDateTimeAsTicks)
        {
            CreateTable<T>();
        }
    }
}
