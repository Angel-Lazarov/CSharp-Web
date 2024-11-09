using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DeskMarket.Constraints;

namespace DeskMarket.Data.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(ProductNameMaxLength)]
		[Comment("Product name")]
		public string ProductName { get; set; } = null!;

		[Required]
		[MaxLength(ProductDescriptionMaxLength)]
		[Comment("Product description")]
		public string Description { get; set; } = null!;

		[Required]
		[Comment("Product price")]
		public decimal Price { get; set; }

		public string? ImageUrl { get; set; }

		[Required]
		public string SellerId { get; set; } = null!;
		[ForeignKey(nameof(SellerId))]
		public IdentityUser Seller { get; set; } = null!;

		[Required]
		[Comment("Product addedOn")]
		public DateTime AddedOn { get; set; }

		[Required]
		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		public bool IsDeleted { get; set; }

		public ICollection<ProductClient> ProductsClients { get; set; } = new List<ProductClient>();

	}
}
