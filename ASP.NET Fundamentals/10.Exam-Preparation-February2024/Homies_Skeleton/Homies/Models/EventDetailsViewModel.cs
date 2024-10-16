using System.ComponentModel.DataAnnotations;
using static Homies.Constraints;

namespace Homies.Models
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; } = DateTime.Today.ToString(EventDataFormatAdd);

        [Required]
        public string End { get; set; } = DateTime.Today.ToString(EventDataFormatAdd);

        [Required]
        public string Organiser { get; set; } = null!;

        [Required] public string CreatedOn { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;


    }
}
