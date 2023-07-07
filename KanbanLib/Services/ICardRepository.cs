using KanbanAPI.Dto;
using KanbanAPI.Models;

namespace KanbanAPI.Services;

public interface ICardRepository
{
    Card[] GetAll();

    Card? Get(Guid id);

    Card Insert(CreateCardDto data);

    Card? Update(Guid id, UpdateCardDto data);

    void Delete(Guid id);
}
