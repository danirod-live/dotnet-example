using KanbanAPI.Models;

namespace KanbanAPI.Dto;

public record CreateCardDto(string Title, string Description = "", Priority Level = Priority.Medium)
{
    public Card ToCard()
    {
        return new Card(0, Title, Description, Level);
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