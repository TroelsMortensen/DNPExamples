using AdvancedTodoWebAPIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTodoWebAPIDB.Persistence
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite("Data Source = Todos.db");
        }
    }
}