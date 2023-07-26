using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanLibEFC.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CardId { get; set; }
        public int Priority { get; set; }
        
        public int ColumnId {  get; set; }

        public Column Column { get; set; }
    }
}
