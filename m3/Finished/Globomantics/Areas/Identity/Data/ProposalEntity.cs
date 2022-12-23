using System.ComponentModel.DataAnnotations;

namespace Globomantics.Areas.Identity.Data
{
    public class ProposalEntity
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public ConferenceEntity? Conference { get; set; }
        [StringLength(100)]
        public string Speaker { get; set; } = string.Empty;
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        public bool Approved { get; set; }
    }
}
