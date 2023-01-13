using Globomantics.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Globomantics.Client.ApiServices
{
    public class ProposalApiService : IProposalApiService
    {
        private readonly HttpClient _Client;

        public ProposalApiService(HttpClient client)
        {
            _Client = client;
        }

        public async Task<IEnumerable<ProposalModel>> GetAll(int conferenceId)
        {
            return await _Client.GetFromJsonAsync<IEnumerable<ProposalModel>>($"/api/proposal/{conferenceId}");
        }

        public async Task Add(ProposalModel model)
        {
            await _Client.PostAsJsonAsync("/api/proposal", model);
        }

        public async Task Approve(int proposalId)
        {
            await _Client.GetAsync($"/api/proposal/approve/{proposalId}");
        }
    }
}
