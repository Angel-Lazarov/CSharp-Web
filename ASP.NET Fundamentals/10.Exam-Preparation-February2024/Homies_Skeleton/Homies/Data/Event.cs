using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Constraints;

namespace Homies.Data
{
	public class Event
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(EventNameMaxLength)]
		[Comment("Event Name.")]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(EventDescriptionMaxLength)]
		[Comment("Event Description.")]
		public string Description { get; set; } = null!;

		[Required]
		public string OrganiserId { get; set; } = null!;
		[ForeignKey(nameof(OrganiserId))]
		public IdentityUser Organiser { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

		[Required]
		public int TypeId { get; set; }

		[ForeignKey(nameof(TypeId))]
		public Type Type { get; set; } = null!;

		public ICollection<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
	}
}
