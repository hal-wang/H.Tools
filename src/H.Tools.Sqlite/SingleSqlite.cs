using SQLite;

namespace H.Tools.Sqlite;

public class SingleSqlite<T> : SQLiteConnection
{
    public SingleSqlite(string databasePath, bool storeDateTimeAsTicks = true) :
        base(databasePath, storeDateTimeAsTicks)
    {
        CreateTable<T>();
    }
}
