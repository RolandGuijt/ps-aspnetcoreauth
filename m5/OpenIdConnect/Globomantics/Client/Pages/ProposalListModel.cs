
using Globomantics.Client.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Components;

namespace Globomantics.Client.Pages;
public class ProposalListModel : ComponentBase
{
    [Parameter]
    public string ConferenceId { get; set; }

    public IEnumerable<ProposalModel> proposals;

    [Inject]
    protected IProposalRepository ProposalRepository { get; set; }

    protected override void OnInitialized()
    {
        proposals = ProposalRepository.GetAllForConference(int.Parse(ConferenceId));
    }
}

