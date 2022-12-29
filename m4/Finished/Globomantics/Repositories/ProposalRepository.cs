using Globomantics.Models;

namespace Globomantics.Repositories;

public class ProposalRepository : IProposalRepository
{
    private static List<ProposalModel> proposals = new() {
        new ProposalModel { Id = 1, ConferenceId = 1, Title = "Authentication and Authorization in ASP.NET Core", Speaker = "Roland Guijt" },
        new ProposalModel { Id = 2, ConferenceId = 1, Title = "ASP.NET Core Basics", Speaker = "Alice Waterman" },
        new ProposalModel { Id = 3, ConferenceId = 2, Title = "Writing CSS Like a Boss", Speaker = "Deborah Midland" },
    };
    public IEnumerable<ProposalModel> GetAllForConference(int conferenceId)
    {
        return proposals.Where(p => p.ConferenceId == conferenceId);
    }

    public int Add(ProposalModel model)
    {
        model.Id = proposals.Max(p => p.Id) + 1;
        proposals.Add(model);
        return model.Id;
    }

    public ProposalModel Approve(int proposalId)
    {
        var proposal = proposals.First(p => p.Id == proposalId);
        proposal.Approved = true;
        return proposal;
    }
}
