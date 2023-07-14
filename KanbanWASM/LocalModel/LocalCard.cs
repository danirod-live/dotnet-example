using KanbanAPI.Dto;
using KanbanAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace KanbanWasm.LocalModel
{
    public class LocalCard
    {
        [Required]
        [StringLength(64, ErrorMessage = "No te pases")]
        public string Title { get; set; }

        [StringLength(256, ErrorMessage = "No te pases")]
        public string Description { get; set; }

        [Required]
        public Priority Level { get; set; }

        public LocalCard() : this(new()) { }



        public LocalCard(Card card)
        {
            Title = card.Title ?? "";
            Description = card.Description ?? "";
            Level = card.Level;
        }

        public Card ToCard()
        {
            return new() { Title = Title, Description = Description, Level = Level };
        }

        public CreateCardDto ToCreateCard()
        {
            return new(Title, Description, Level);
        }

        public UpdateCardDto ToUpdateCard()
        {
            return new(Title, Description, Level);
        }

        public override bool Equals(object? obj)
        {
            return obj is LocalCard card &&
                   Title == card.Title &&
                   Description == card.Description &&
                   Level == card.Level;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Description, Level);
        }

        public override string ToString()
        {
            return $"Card {Title}";
        }
    }
}
