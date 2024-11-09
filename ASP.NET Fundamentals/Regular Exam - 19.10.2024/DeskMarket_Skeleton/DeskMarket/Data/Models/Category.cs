using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constraints;

namespace DeskMarket.Data.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(CategoryNameMaxLength)]
		[Comment("Category Name")]
		public string Name { get; set; } = null!;

		public ICollection<Product> Products { get; set; } = new List<Product>();

	}
}
