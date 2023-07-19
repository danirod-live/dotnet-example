using KanbanAPI.Dto;
using KanbanAPI.Models;

namespace KanbanAPI.Services
{
    public class CardRepositoryService : ICardRepository
    {
        private readonly ICardCrud crud;

        public CardRepositoryService(ICardCrud crud)
        {
            this.crud = crud;
        }

        public Card? Get(int id) => crud.Find(id);

        public Card[] GetAll() => crud.All();

        public Card Insert(CreateCardDto data)
        {
            var card = data.ToCard();
            crud.Insert(card);
            return card;
        }

        public Card? Update(int id, UpdateCardDto data)
        {
            var card = crud.Find(id);
            if (card != null)
            {
                data.UpdateCard(card);
                crud.Update(card);
            }
            return card;
        }

        public void Delete(int id)
        {
            var card = crud.Find(id);
            if (card != null)
            {
                crud.Delete(card);
            }
        }
    }
}
