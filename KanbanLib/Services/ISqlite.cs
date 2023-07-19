using Microsoft.Data.Sqlite;

namespace KanbanAPI.Services;

public interface ISqlite
{
    SqliteConnection Connection { get; }

    SqliteCommand OpenCommand();
}