namespace Globomantics.Models;

public class ConferenceModel
{
    public ConferenceModel()
    {
        Start = DateTime.Now;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public string Location { get; set; } = string.Empty;
    public int AttendeeCount { get; set; }
}
