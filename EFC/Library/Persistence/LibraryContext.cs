using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = C:\TRMO\.NET projects\DNPExamples\EFC\Library\Library.db");
        }
    }
}