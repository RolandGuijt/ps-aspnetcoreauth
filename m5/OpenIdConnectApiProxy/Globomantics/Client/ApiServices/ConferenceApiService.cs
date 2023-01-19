using Globomantics.Client.Models;
using System.Net.Http.Json;

namespace Globomantics.Client.ApiServices
{
    public class ConferenceApiService : IConferenceApiService
    {
        private readonly HttpClient _Client;

        public ConferenceApiService(HttpClient client)
        {
            _Client = client;
        }

        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return await _Client
                .GetFromJsonAsync<IEnumerable<ConferenceModel>>("/api/conference");
        }

        public async Task Add(ConferenceModel model)
        {
            await _Client.PostAsJsonAsync("/api/conference", model);
        }
    }
}
