using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class BlogContext : IdentityDbContext<User>, IBlogContext
    {

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=blogDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.Name).HasMaxLength(30);
            modelBuilder.Entity<User>().HasAlternateKey(p => p.Name);
            modelBuilder.Entity<User>().Property(p => p.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasAlternateKey(p => p.UserId);


            modelBuilder.Entity<Comment>().Property(p => p.Text).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Text).HasMaxLength(2000).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Title).HasMaxLength(30).IsRequired();

            modelBuilder.Entity<Post>().HasAlternateKey(p => new { p.UserId, p.Text });
            modelBuilder.Entity<Comment>().HasAlternateKey(p => new { p.UserId, p.Text });


        }
    }
}
