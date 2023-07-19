using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace KanbanAPI.Services;

public class SqliteService : ISqlite, IDisposable
{
    private readonly SqliteConnection connection;

    public IDbConnection Connection => connection;

    public SqliteService()
    {
        connection = new SqliteConnection("Data Source=Database.db");
    }

    public void Dispose()
    {
        connection.Close();
    }

    public SqliteCommand OpenCommand()
    {
        connection.Open();
        return connection.CreateCommand();
    }
}
