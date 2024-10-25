using LMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.DataAccess.Data
{
   public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Categories{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Science Fiction", Code = new List<string> { "SF001", "SF002", "SF003" } },
                new Category { Id = 2, CategoryName = "Fantasy", Code = new List<string> { "F001", "F002", "F003" } },
                new Category { Id = 3, CategoryName = "History", Code = new List<string> { "H001", "H002", "H003" } }
            );

            // Seed data for Books
            modelBuilder.Entity<Books>().HasData(
                new Books
                {
                    Id = 1,
                    BookName = "Dune",
                    AuthorName = "Frank Herbert",
                    SerialName = "SF001",
                    IsBookAvailable = true,
                    CategoryId = 1
                },
                new Books
                {
                    Id = 2,
                    BookName = "The Hobbit",
                    AuthorName = "J.R.R. Tolkien",
                    SerialName = "F001",
                    IsBookAvailable = true,
                    CategoryId = 2
                },
                new Books
                {
                    Id = 3,
                    BookName = "The Art of War",
                    AuthorName = "Sun Tzu",
                    SerialName = "H001",
                    IsBookAvailable = false,
                    CategoryId = 3
                }
            );
        }

    }
}
