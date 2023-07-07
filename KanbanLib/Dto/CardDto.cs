using KanbanAPI.Models;
using System.Reflection;

namespace KanbanAPI.Dto;

public record CreateCardDto(string Title, string Description = "", Priority Level = Priority.Medium)
{
    public Card ToCard()
    {
        var guid = Guid.NewGuid();
        return new Card(guid, Title, Description, Level);
    }
}

public record UpdateCardDto(string? Title, string? Description, Priority? Level)
{
    public void UpdateCard(Card card)
    {
        if (Title != null) card.Title = Title;
        if (Description != null) card.Description = Description;
        if (Level != null) card.Level = (Priority)Level;
    }
}