using Globomantics.Models;

namespace Globomantics.Repositories;
public class ConferenceRepository : IConferenceRepository
{
    private static List<ConferenceModel> conferences = new() {
          new ConferenceModel { Id = 1, Name = "A Nice Day of Coding", Location = "Remote", Start = DateTime.Now, AttendeeCount = 201 },
          new ConferenceModel { Id = 2, Name = "Hackathon Live", Location = "New York", Start = DateTime.Now.AddDays(50), AttendeeCount = 140  }
        };
    public IEnumerable<ConferenceModel> GetAll()
    {
        return conferences;
    }

    public ConferenceModel GetById(int id)
    {
        return conferences.First(c => c.Id == id);
    }

    public int Add(ConferenceModel model)
    {
        model.Id = conferences.Max(c => c.Id) + 1;
        conferences.Add(model);
        return model.Id;
    }
}


