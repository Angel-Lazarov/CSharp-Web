using System.ComponentModel.DataAnnotations;
using static Homies.Constraints;
using Type = Homies.Data.Type;

namespace Homies.Models
{
    public class EventViewModel
    {
        [Required]
        [MaxLength(EventNameMaxLength)]
        [MinLength(EventNameMinLength)]

        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EventDescriptionMaxLength)]
        [MinLength(EventDescriptionMinLength)]

        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; } = DateTime.Today.ToString(EventDataFormatAdd);

        [Required]
        public string End { get; set; } = DateTime.Today.ToString(EventDataFormatAdd);

        [Required]
        public int TypeId { get; set; }

        public ICollection<Type> Types { get; set; } = new List<Type>();


    }
}
