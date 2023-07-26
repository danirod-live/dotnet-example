using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanLibEFC.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; }

        public List<Card> Cards { get; } = new();

    }
}
