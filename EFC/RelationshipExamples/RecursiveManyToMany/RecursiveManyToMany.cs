using Microsoft.EntityFrameworkCore;

namespace RelationshipExamples.RecursiveManyToMany
{
    public class RecursiveManyToMany : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = C:\TRMO\.NET projects\DNPExamples\EFC\RelationshipExamples\RecursiveManyToMany.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserToUser>()
                .HasKey(sc => 
                    new
                    {
                        sc.FromUserId, 
                        sc.ToUserId
                    }
                );

            modelBuilder.Entity<UserToUser>()
                .HasOne(userToUser => userToUser.FromUser)
                .WithMany(user => user.FollowsUsers)
                .HasForeignKey(userToUser => userToUser.FromUserId);
            
            modelBuilder.Entity<UserToUser>()
                .HasOne(userToUser => userToUser.ToUser)
                .WithMany(user => user.FollowsUsers)
                .HasForeignKey(userToUser => userToUser.ToUserId);
        }
    }
}