using Microsoft.EntityFrameworkCore;
using PizzaApplicationAPI.Models;

namespace PizzaApplicationAPI.Contexts
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions options) : base(options)
        {

        }
        #region DbSets
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza()
                {
                    Id = 101,
                    Name = "Pepperoni Pizza",
                    Description = "A classic pizza topped with pepperoni slices",
                    Stock = 10,
                    Price = 9.99f
                },
                new Pizza()
                {
                    Id = 102,
                    Name = "Cheese Pizza",
                    Description = "A simple pizza topped with a blend of cheeses",
                    Stock = 15,
                    Price = 8.99f
                }
            );
        }
    }
}
