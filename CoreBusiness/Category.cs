using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
    public class Category : CommonEntity
    {
        public int CategoryId { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageCateUrl { get; set; } = string.Empty;

        // navigation property for ef core
        public List<Product> Products { get; set; }
        public ApplicationUser User { get; set; }
    }
    public class CategoryConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                    .HasMany(c => c.Categories)
                    .WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Category>()
               .HasMany(c => c.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId);
            // seeding some data
            modelBuilder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, UserId ="test", Name = "Beverage", Description = "Beverage" },
                    new Category { CategoryId = 2, UserId = "test", Name = "Bakery", Description = "Bakery" },
                    new Category { CategoryId = 3, UserId = "test", Name = "Meat", Description = "Meat" }
                );
        }
    }
}
