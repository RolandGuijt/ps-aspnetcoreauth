namespace Globomantics.Client.Models
{
    public class ProposalModel
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Speaker { get; set; } = string.Empty;
        public bool Approved { get; set; }
    }
}