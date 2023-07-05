using KanbanAPI.Models;

namespace KanbanAPI.Services;

public interface ICardCrud
{
    Card[] All();

    Card? Find(Guid id);

    void Insert(Card card);

    void Update(Card card);

    void Delete(Card card);
}
