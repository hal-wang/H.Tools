using SQLite;

namespace H.Tools.Sqlite;

public class SqliteBase<T> : SQLiteConnection
{
    public SqliteBase(string databasePath, bool storeDateTimeAsTicks = true) :
        base(databasePath, storeDateTimeAsTicks)
    {
        CreateTable<T>();
    }
}
