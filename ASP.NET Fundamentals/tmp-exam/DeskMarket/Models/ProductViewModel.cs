using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constraints;

namespace DeskMarket.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		[Required]
		[MinLength(ProductNameMinLength)]
		[MaxLength(ProductNameMaxLength)]
		public string ProductName { get; set; } = null!;

		[Required]
		[Range(typeof(decimal), ProductPriceMinValue, ProductPriceMaxValue)]
		public decimal Price { get; set; }

		public string? ImageUrl { get; set; }

		public bool IsSeller { get; set; }
		public bool HasBought { get; set; }



	}
}
