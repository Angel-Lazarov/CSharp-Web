using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
