using Globomantics.Areas.Identity.Data;
using Globomantics.Data;
using Globomantics.Models;
using Microsoft.EntityFrameworkCore;

namespace Globomantics.Repositories;

public class ProposalRepository : IProposalRepository
{
    private readonly ApplicationDbContext _DbContext;

    public ProposalRepository(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }
    public async Task<IEnumerable<ProposalModel>> GetAllForConference(int conferenceId)
    {
        return await _DbContext.Proposals
            .Where(p => p.ConferenceId == conferenceId)
            .Select(p => new ProposalModel
            {
                Id = p.Id,
                ConferenceId = p.ConferenceId,
                Speaker = p.Speaker,
                Approved = p.Approved,
                Title = p.Title
            })
            .ToArrayAsync();
    }

    public async Task<int> Add(ProposalModel model)
    {
        var e = new ProposalEntity
        {
            ConferenceId = model.ConferenceId,
            Speaker = model.Speaker,
            Approved = model.Approved,
            Title = model.Title
        };
        _DbContext.Proposals.Add(e);
        await _DbContext.SaveChangesAsync();
        return e.Id;
    }

    public async Task<ProposalModel> Approve(int proposalId)
    {
        var proposal = _DbContext.Proposals.First(p => p.Id == proposalId);
        proposal.Approved = true;
        await _DbContext.SaveChangesAsync();

        return new ProposalModel
        {
            Id = proposal.Id,
            Speaker = proposal.Speaker,
            ConferenceId = proposal.ConferenceId,
            Approved = proposal.Approved,
            Title = proposal.Title
        };
    }
}
