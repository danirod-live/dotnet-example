using KanbanAPI.Dto;
using KanbanAPI.Models;

namespace KanbanAPI.Services;

public interface ICardRepository
{
    Card[] GetAll();

    Card? Get(int id);

    Card Insert(CreateCardDto data);

    Card? Update(int id, UpdateCardDto data);

    void Delete(int id);
}
