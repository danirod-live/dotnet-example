using Microsoft.Data.Sqlite;
using System.Data;

namespace KanbanAPI.Services;

public interface ISqlite
{
    IDbConnection Connection { get; }

    SqliteCommand OpenCommand();
}