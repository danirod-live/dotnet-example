using KanbanAPI.Models;

namespace KanbanAPI.Services;

public interface ICardCrud
{
    Card[] All();

    Card? Find(int id);

    void Insert(Card card);

    void Update(Card card);

    void Delete(Card card);
}
