using KanbanAPI.Dto;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KanbanApiEfc.Dto
{
    public class BoardWithColumnDto : BoardDto
    {
        public List<ColumnDto> Columns { get; set; }
    }
}
