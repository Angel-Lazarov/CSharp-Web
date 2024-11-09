using DeskMarket.Data.Models;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constraints;

namespace DeskMarket.Models
{
    public class AddViewModel
    {
        [Required]
        [MinLength(ProductNameMinLength)]
        [MaxLength(ProductNameMaxLength)]
        public string ProductName { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        [MinLength(ProductDescriptionMinLength)]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(typeof(decimal), ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string AddedOn { get; set; } = DateTime.Today.ToString(AddedOnDateFormat);

        [Required]
        public int CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
