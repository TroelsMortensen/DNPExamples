using Microsoft.EntityFrameworkCore;

namespace OneToOne.Models
{
    public class OneToOneContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProfileInfo> ProfileInfos { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = OneToOne.db");
        }
    }
}