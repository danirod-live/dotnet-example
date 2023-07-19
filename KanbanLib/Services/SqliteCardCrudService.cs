using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using KanbanAPI.Models;

namespace KanbanAPI.Services;

public class SqliteCardCrudService : ICardCrud
{
    private readonly ISqlite sqlite;

    public SqliteCardCrudService(ISqlite sqlite)
    {
        this.sqlite = sqlite;
        InitSchema();
    }

    private void InitSchema()
    {
        var schema = @"
        CREATE TABLE IF NOT EXISTS Card(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title VARCHAR(256) NOT NULL,
            Description TEXT,
            Level VARCHAR(64) NOT NULL
        );
        CREATE UNIQUE INDEX IF NOT EXISTS CardId ON Card(Id);
        CREATE INDEX IF NOT EXISTS CardLevel ON Card(Level);
        ";
        var command = sqlite.OpenCommand();
        command.CommandText = schema;
        command.ExecuteNonQuery();
    }

    public Card[] All()
    {
        return sqlite.Connection.GetAll<Card>().ToArray();
    }

    public Card? Find(int id)
    {
        return sqlite.Connection.Get<Card>(id);
    }

    public void Insert(Card card)
    {
        sqlite.Connection.Insert<Card>(card);
    }

    public void Update(Card card)
    {
        sqlite.Connection.Update<Card>(card);
    }

    public void Delete(Card card)
    {
        sqlite.Connection.Delete<Card>(card);
    }
}