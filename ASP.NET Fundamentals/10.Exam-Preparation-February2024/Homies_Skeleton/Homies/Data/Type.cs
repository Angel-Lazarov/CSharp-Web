using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Homies.Constraints;

namespace Homies.Data
{
	public class Type
	{
		[Key] public int Id { get; set; }

		[Required]
		[MaxLength(TypeNameMaxLength)]
		[Comment("Type name.")]
		public string Name { get; set; } = null!;

		public ICollection<Event> Events { get; set; } = new List<Event>();
	}
}
