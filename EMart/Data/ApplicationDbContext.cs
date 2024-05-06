using EMart.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMart.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId=1, Name="Vegetables", DisplayOrder=1},
                new Category { CategoryId = 2, Name = "Fruits", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Foodgrains", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name = "Potato", Description = "Fresh organic potatoes", ListPrice = 30, PackSizeValue=1,PackSizeUnit="kg",CategoryId=1 , ImageUrl = "" },
               new Product { Id = 2, Name = "Tomato", Description = "Juicy red tomatoes", ListPrice = 25, PackSizeValue = 1, PackSizeUnit = "kg", CategoryId = 1 , ImageUrl = "" },
               new Product { Id = 3, Name = "Onion", Description = "Crisp and aromatic onions", ListPrice = 20, PackSizeValue = 1, PackSizeUnit = "kg", CategoryId = 1, ImageUrl = "" },
               new Product { Id = 4, Name = "Carrot", Description = "Fresh and crunchy carrots", ListPrice = 35, PackSizeValue = 1, PackSizeUnit = "kg", CategoryId = 1, ImageUrl="" }
                );
        }
    }
}
