using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string CategoryName { get; set; } = null!;

        [Required]
        public string AddedOn { get; set; } = null!;

        [Required]
        public string Seller { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool HasBought { get; set; }

    }
}
