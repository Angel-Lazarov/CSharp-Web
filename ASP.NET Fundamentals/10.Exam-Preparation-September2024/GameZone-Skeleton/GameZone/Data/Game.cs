using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Constraints;

namespace GameZone.Data
{
	public class Game
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(GameTitleMaxLength)]
		[Comment("Title of the Game")]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(GameDescriptionMaxLength)]
		[Comment("Game Description")]
		public string Description { get; set; } = null!;

		public string? ImageUrl { get; set; }


		[Required]
		//[MaxLength(PublisherIdMaxLength)]
		public string PublisherId { get; set; } = null!;
		[ForeignKey(nameof(PublisherId))]
		public IdentityUser Publisher { get; set; } = null!;


		[Required]
		public DateTime ReleasedOn { get; set; }

		[Required]
		public int GenreId { get; set; }
		[ForeignKey(nameof(GenreId))]
		public Genre Genre { get; set; } = null!;

		public bool IsDeleted { get; set; }

		public ICollection<GamerGame> GamersGames { get; set; } = new List<GamerGame>();

	}
}
