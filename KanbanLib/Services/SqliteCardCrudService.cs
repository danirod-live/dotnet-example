using Dapper;
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
            CREATE TABLE IF NOT EXISTS cards(
                guid VARCHAR(64) NOT NULL,
                title VARCHAR(256) NOT NULL,
                description TEXT,
                priority VARCHAR(64) NOT NULL
            );
            CREATE UNIQUE INDEX IF NOT EXISTS cards_guid ON cards(guid);
            CREATE INDEX IF NOT EXISTS cards_priority ON cards(priority);
            ";
        var command = sqlite.OpenCommand();
        command.CommandText = schema;
        command.ExecuteNonQuery();
    }

    public Card[] All()
    {
        var query = "SELECT guid AS id, title, description, priority FROM cards";
        return sqlite.Connection.Query<Card>(query).ToArray();
    }

    public Card? Find(Guid id)
    {
        var query = "SELECT guid AS id, title, description, priority FROM cards WHERE guid = @Id";
        return sqlite.Connection.QueryFirstOrDefault<Card>(query, new
        {
            Id = id,
        });
    }

    public void Insert(Card card)
    {
        var stmt = "INSERT INTO cards (guid, title, description, priority) VALUES (@Id, @Title, @Description, @Level)";
        sqlite.Connection.Execute(stmt, card);
    }

    public void Update(Card card)
    {
        var stmt = "UPDATE cards SET title = @Title, description = @Description, priority = @Level WHERE guid = @Id";
        sqlite.Connection.Execute(stmt, card);
    }

    public void Delete(Card card)
    {
        var query = "DELETE FROM cards WHERE guid = @Id";
        sqlite.Connection.Execute(query, card);
    }
}
