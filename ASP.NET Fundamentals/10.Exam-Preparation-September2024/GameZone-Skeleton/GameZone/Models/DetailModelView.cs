using System.ComponentModel.DataAnnotations;
using static GameZone.Constraints;

namespace GameZone.Models
{
    public class DetailModelView
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        public string ReleasedOn { get; set; } = DateTime.Today.ToString(ReleaseDateFormat);

        [Required]
        public string Publisher { get; set; } = null!;
    }
}
