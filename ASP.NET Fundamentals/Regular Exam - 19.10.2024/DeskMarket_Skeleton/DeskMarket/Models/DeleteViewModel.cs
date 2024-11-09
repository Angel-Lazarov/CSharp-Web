using System.ComponentModel.DataAnnotations;
namespace DeskMarket.Models
{
    public class DeleteViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public string Seller { get; set; } = null!;

        [Required]
        public string SellerId { get; set; } = null!;


    }
}
