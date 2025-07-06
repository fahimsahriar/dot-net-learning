using DMP.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DMP.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category { Category21Id =1, Name = "Seattle", DisplayOrder = 1 },
            new Category { Category21Id = 2, Name = "Vancouver", DisplayOrder = 2 },
            new Category { Category21Id = 3, Name = "Mexico City", DisplayOrder = 3 },
            new Category { Category21Id = 4, Name = "Puebla", DisplayOrder = 3 });
        }
    }
}
