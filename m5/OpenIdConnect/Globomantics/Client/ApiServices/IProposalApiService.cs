using Globomantics.Client.Models;

namespace Globomantics.Client.ApiServices
{
    public interface IProposalApiService
    {
        Task Add(ProposalModel model);
        Task Approve(int proposalId);
        Task<IEnumerable<ProposalModel>> GetAll(int conferenceId);
    }
}