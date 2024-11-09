using DeskMarket.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<Product> Products { get; set; } = null!;
		public DbSet<ProductClient> ProductsClients { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ProductClient>()
				.HasKey(pc => new { pc.ClientId, pc.ProductId });

			builder.Entity<Product>(entity =>
			{
				entity.HasMany(p => p.ProductsClients)
					.WithOne(p => p.Product)
					.HasForeignKey(p => p.ProductId)
					.OnDelete(DeleteBehavior.NoAction);
			});


			builder
				.Entity<Category>()
				.HasData(
					new Category { Id = 1, Name = "Laptops" },
					new Category { Id = 2, Name = "Workstations" },
					new Category { Id = 3, Name = "Accessories" },
					new Category { Id = 4, Name = "Desktops" },
					new Category { Id = 5, Name = "Monitors" });
		}
	}
}
