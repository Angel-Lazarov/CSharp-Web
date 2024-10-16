using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
	public class EventInfoViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Start { get; set; } = null!;

		[Required]
		public string Type { get; set; } = null!;

		[Required]
		public string Organiser { get; set; } = null!;
	}
}
