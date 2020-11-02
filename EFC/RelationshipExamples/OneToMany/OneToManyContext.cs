using Microsoft.EntityFrameworkCore;

namespace RelationshipExamples.OneToMany
{
    public class OneToManyContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = OneToMany.db");
        }
    }
}