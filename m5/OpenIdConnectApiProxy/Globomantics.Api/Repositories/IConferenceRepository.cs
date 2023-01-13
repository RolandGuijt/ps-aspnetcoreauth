using Globomantics.Client.Models;

namespace Globomantics.Repositories;

public interface IConferenceRepository
{
    int Add(ConferenceModel model);
    IEnumerable<ConferenceModel> GetAll();
}