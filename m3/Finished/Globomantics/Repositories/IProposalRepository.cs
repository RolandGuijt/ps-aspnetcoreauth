using Globomantics.Models;

namespace Globomantics.Repositories;

public interface IProposalRepository
{
    Task<int> Add(ProposalModel model);
    Task<ProposalModel> Approve(int proposalId);
    Task<IEnumerable<ProposalModel>> GetAllForConference(int conferenceId);
}