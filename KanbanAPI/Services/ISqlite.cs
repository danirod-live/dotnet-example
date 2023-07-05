using Microsoft.Data.Sqlite;

namespace KanbanAPI.Services;

public interface ISqlite
{
    SqliteCommand OpenCommand();
}