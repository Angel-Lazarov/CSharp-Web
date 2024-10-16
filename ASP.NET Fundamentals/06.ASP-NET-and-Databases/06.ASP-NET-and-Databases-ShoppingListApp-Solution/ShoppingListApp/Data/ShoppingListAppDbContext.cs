using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Data.Models;

namespace ShoppingListApp.Data
{
    public class ShoppingListAppDbContext : DbContext
    {

        public ShoppingListAppDbContext(DbContextOptions<ShoppingListAppDbContext> options)
            : base(options)
                => Database.EnsureCreated();

        public DbSet<Product> Products { get; set; }

    }
}