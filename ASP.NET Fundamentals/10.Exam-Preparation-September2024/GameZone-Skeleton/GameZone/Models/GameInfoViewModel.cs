using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
	public class GameInfoViewModel
	{
		public int Id { get; set; }

		public string? ImageUrl { get; set; }

		[Required]
		public string Title { get; set; } = null!;

		[Required]
		public string ReleasedOn { get; set; } = null!;

		[Required]
		public string Genre { get; set; } = null!;

		[Required]
		public string Publisher { get; set; } = null!;
	}
}
