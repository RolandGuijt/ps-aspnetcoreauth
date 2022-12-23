using System.ComponentModel.DataAnnotations;

namespace Globomantics.Areas.Identity.Data
{
    public class ConferenceEntity
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;
        public int AttendeeCount { get; set; }
    }
}
