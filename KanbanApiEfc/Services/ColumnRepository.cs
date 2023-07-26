using KanbanApiEfc.Dto;
using KanbanApiEfc.Exceptions;
using KanbanLibEFC;
using KanbanLibEFC.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace KanbanApiEfc.Services
{
    public class ColumnRepository
    {
        private KanbanContext Context;

        public ColumnRepository(KanbanContext context)
        {
            Context = context;
        }

        private static IQueryable<ColumnDto> coerce(IQueryable<Column> query)
        {
            return query.Select(c => new ColumnDto
            {
                Id = c.Id,
                Name = c.Name,
            });
        }

        private void assertBoardExists(int boardId)
        {
            var board = Context.Boards.FirstOrDefault(b => b.Id == boardId);
            if (board == null)
            {
                throw new NotFoundException($"Board {boardId} not found");
            }
        }

        public List<ColumnDto> ListColumnsByBoardId(int boardId)
        {
            assertBoardExists(boardId);
            return coerce(Context.Columns.Where(c => c.BoardId == boardId)).ToList();
        }

        public ColumnDto? GetColumn(int boardId, int id)
        {
            return coerce(Context.Columns.Where(c => c.Id == id && c.BoardId == boardId)).FirstOrDefault();
        }

        public ColumnDto Insert(int boardId, ColumnParams data)
        {
            assertBoardExists(boardId);
            var column = new Column { BoardId = boardId, Name = data.name };
            Context.Columns.Add(column);
            Context.SaveChanges();
            return new ColumnDto
            {
                Id = column.Id,
                Name = column.Name,
            };
        }

        public ColumnDto Update(int boardId, int columnId, ColumnParams data)
        {
            var column = Context.Columns.FirstOrDefault(c => c.Id == columnId && c.BoardId == boardId);
            if (column == null)
            {
                throw new NotFoundException($"Column {columnId} not found in board {boardId}");
            }
            column.Name = data.name;
            Context.SaveChanges();
            return new ColumnDto
            {
                Id = column.Id,
                Name = column.Name,
            };
        }

        public void DeleteColumn(int boardId, int columnId)
        {
            Context.Columns.Where(c => c.Id == columnId && c.BoardId == boardId).ExecuteDelete();
        }
    }
}
