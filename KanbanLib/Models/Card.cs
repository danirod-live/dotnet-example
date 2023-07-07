namespace KanbanAPI.Models;

public class Card
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Priority Level { get; set; }

    public Card() { }

    public Card(Guid id, string title, string? description, Priority level)
    {
        Id = id;
        Title = title;
        Description = description;
        Level = level;
    }
}
