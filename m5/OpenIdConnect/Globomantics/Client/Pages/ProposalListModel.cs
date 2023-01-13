
using Globomantics.Client.ApiServices;
using Globomantics.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Globomantics.Client.Pages;
public class ProposalListModel : ComponentBase
{
    [Parameter]
    public string ConferenceId { get; set; }

    public IEnumerable<ProposalModel> proposals;

    [Inject]
    protected IProposalApiService apiService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        proposals = await apiService.GetAll(int.Parse(ConferenceId));
    }
}

