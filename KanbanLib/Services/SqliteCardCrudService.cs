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
        var query = "SELECT guid, title, description, priority FROM cards";
        var command = sqlite.OpenCommand();
        command.CommandText = query;
        using var reader = command.ExecuteReader();

        var cards = new List<Card>();
        while (reader.Read())
        {
            _ = Enum.TryParse(reader.GetString(3), out Priority level);
            var card = new Card
            {
                Id = reader.GetGuid(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Level = level,
            };
            cards.Add(card);
        }
        return cards.ToArray();
    }

    public Card? Find(Guid id)
    {
        var query = "SELECT guid, title, description, priority FROM cards WHERE guid = $id";
        var command = sqlite.OpenCommand();
        command.CommandText = query;
        command.Parameters.AddWithValue("$id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            _ = Enum.TryParse(reader.GetString(3), out Priority level);
            var card = new Card
            {
                Id = reader.GetGuid(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Level = level
            };
            return card;
        }
        return null;
    }

    public void Insert(Card card)
    {
        var stmt = "INSERT INTO cards (guid, title, description, priority) VALUES ($id, $title, $description, $priority)";
        var command = sqlite.OpenCommand();
        command.CommandText = stmt;
        command.Parameters.AddWithValue("$id", card.Id);
        command.Parameters.AddWithValue("$title", card.Title);
        command.Parameters.AddWithValue("$description", card.Description);
        command.Parameters.AddWithValue("$priority", card.Level);
        command.ExecuteNonQuery();
    }

    public void Update(Card card)
    {
        var stmt = "UPDATE cards SET title = $title, description = $desc, priority = $prio WHERE guid = $id";
        var command = sqlite.OpenCommand();
        command.CommandText = stmt;
        command.Parameters.AddWithValue("$id", card.Id);
        command.Parameters.AddWithValue("$title", card.Title);
        command.Parameters.AddWithValue("$desc", card.Description);
        command.Parameters.AddWithValue("$prio", card.Level);
        command.ExecuteNonQuery();
    }

    public void Delete(Card card)
    {
        var query = "DELETE FROM cards WHERE guid = $id";
        var command = sqlite.OpenCommand();
        command.CommandText = query;
        command.Parameters.AddWithValue("$id", card.Id);
        command.ExecuteNonQuery();
    }
}
