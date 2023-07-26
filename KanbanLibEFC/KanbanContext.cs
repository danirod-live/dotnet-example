using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanLibEFC
{
    public class KanbanContext : DbContext
    {
        public DbSet<Models.Card> Cards {  get; set; }

        public DbSet<Models.Column> Columns { get; set; }

        public DbSet<Models.Board> Boards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(@"Host=localhost;Username=kanban;Password=kanban;Database=kanban");
    }
}
