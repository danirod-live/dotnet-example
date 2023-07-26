using KanbanAPI.Dto;
using KanbanApiEfc.Dto;
using KanbanLibEFC;
using KanbanLibEFC.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanApiEfc.Services
{
    public class BoardRepository
    {
        private KanbanContext Context;

        public BoardRepository(KanbanContext context)
        {
            Context = context;
        }

        private IQueryable<BoardWithColumnDto> PrepareBoardWithColumn() =>
            Context.Boards.OrderByDescending(b => b.Id)
                .Include(b => b.Columns)
                .Select(b => new BoardWithColumnDto
                {
                    Name = b.Name,
                    Description = b.Description,
                    Id = b.Id,
                    Columns = b.Columns.Select(c => new ColumnDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList(),
                });

        public List<BoardWithColumnDto> List(int page)
        {
            return PrepareBoardWithColumn().ToList();
        }

        public BoardWithColumnDto? Get(int id)
        {
            return PrepareBoardWithColumn().Where(x => x.Id == id).FirstOrDefault();
        }

        public BoardWithColumnDto Insert(BoardParams data)
        {
            var board = new Board
            {
                Name = data.name,
                Description = data.description
            };
            Context.Boards.Add(board);
            Context.SaveChanges();
            return new BoardWithColumnDto
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                Columns = new(),
            };
        }

        public BoardWithColumnDto? Update(int id, BoardParams data)
        {
            var record = Context.Boards.Where(x => x.Id == id).FirstOrDefault();
            if (record != null)
            {
                record.Name = data.name;
                record.Description = data.description;
                Context.SaveChanges();
            }
            return Get(id);
        }

        public void Delete(int id)
        {
            Context.Columns.Where(c => c.BoardId == id).ExecuteDelete();
            Context.Boards.Where(b => b.Id == id).ExecuteDelete();
        }
    }
}
