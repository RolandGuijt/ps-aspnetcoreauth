using Globomantics.Models;

namespace Globomantics.Repositories;

public interface IProposalRepository
{
    ProposalModel GetById(int id);
    void EditProposal(ProposalModel proposal);
    int Add(ProposalModel model);
    ProposalModel Approve(int proposalId);
    IEnumerable<ProposalModel> GetAllForConference(int conferenceId);
}