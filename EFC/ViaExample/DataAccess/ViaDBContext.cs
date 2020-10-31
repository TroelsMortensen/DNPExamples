using Microsoft.EntityFrameworkCore;
using ViaExample.Models;

namespace ViaExample.DataAccess
{
    public class ViaDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = C:\TRMO\.NET projects\DNPExamples\EFC\ViaExample\VIA.db");
        }
    }
}