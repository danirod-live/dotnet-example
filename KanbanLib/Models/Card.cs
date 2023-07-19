using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanAPI.Models;

[Table("Card")]
public class Card
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Priority Level { get; set; }

    public Card() { }

    public Card(int id, string title, string? description, Priority level)
    {
        Id = id;
        Title = title;
        Description = description;
        Level = level;
    }
}
