using GameZone.Data;
using System.ComponentModel.DataAnnotations;
using static GameZone.Constraints;

namespace GameZone.Models
{
	public class GameViewModel
	{
		[Required]
		[MaxLength(GameTitleMaxLength)]
		[MinLength(GameTitleMinLength)]
		public string Title { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }

		[Required]
		//[MaxLength(GameDescriptionMaxLength)]
		//[MinLength(GameDescriptionMinLength)]
		[StringLength(GameDescriptionMaxLength, MinimumLength = GameDescriptionMinLength)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public string ReleasedOn { get; set; } = DateTime.Today.ToString(ReleaseDateFormat);

		[Required]
		public int GenreId { get; set; }

		public ICollection<Genre> Genres { get; set; } = new List<Genre>();
	}
}
