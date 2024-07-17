using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Contexts
{
    public class ProductWebAPIContext : DbContext
    {
        public ProductWebAPIContext(DbContextOptions<ProductWebAPIContext> options) : base(options) 
        { 
        
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 101, ImageUrl = "https://kousikblob.blob.core.windows.net/images/sword.png", Name = "Sword", Price = 2000 },
                new Product() { Id = 102, ImageUrl = "https://kousikblob.blob.core.windows.net/images/laptop.png", Name = "Laptop", Price = 55000},
                new Product() { Id = 103, ImageUrl = "https://kousikblob.blob.core.windows.net/images/dumbells.png", Name = "Dumbells", Price = 1500}
            );
        }
    }
}
