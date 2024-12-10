using Humanizer;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Item>Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Select Category" },
                new Category { Id = 2, Name = "Computer" },
                new Category { Id = 3, Name = "Mobiles" },
                new Category { Id = 4, Name = "Electroic Device" }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
