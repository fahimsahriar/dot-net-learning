using DotNetMasteryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetMasteryProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

<<<<<<< HEAD
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { Category21Id = 1, Name = "Action", DisplayOrder = 1 },
        //        new Category { Category21Id = 2, Name = "Gadget", DisplayOrder = 2 },
        //        new Category { Category21Id = 3, Name = "Clothing", DisplayOrder = 3 }
        //        );
        //}
=======
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Category21Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Category21Id = 2, Name = "Gadget", DisplayOrder = 2 },
                new Category { Category21Id = 3, Name = "Clothing", DisplayOrder = 3 }
                );
        }
>>>>>>> 9c34fb2ba4548b23c03c212b8dcb78b998505cdf
    }
}
