using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Controllers;

[Authorize]
public class ProposalController : Controller
{
    private readonly IConferenceRepository conferenceRepo;
    private readonly IProposalRepository proposalRepo;
    private readonly IAuthorizationService authorizationService;

    public ProposalController(IConferenceRepository conferenceRepo, 
        IProposalRepository proposalRepo, 
        IAuthorizationService authorizationService)
    {
        this.conferenceRepo = conferenceRepo;
        this.proposalRepo = proposalRepo;
        this.authorizationService = authorizationService;
    }

    public IActionResult Index(int conferenceId)
    {
        var conference = conferenceRepo.GetById(conferenceId);
        ViewBag.Title = $"Speaker - Proposals For Conference {conference.Name} {conference.Location}";
        ViewBag.ConferenceId = conferenceId;

        return View(proposalRepo.GetAllForConference(conferenceId));
    }

    [Authorize(Policy = "IsSpeaker")]
    [Authorize(Policy = "YearsOfExperience")]
    public IActionResult AddProposal(int conferenceId)
    {
        ViewBag.Title = "Speaker - Add Proposal";
        return View(new ProposalModel { ConferenceId = conferenceId });
    }

    [HttpPost]
    [Authorize(Policy = "IsSpeaker")]
    [Authorize(Policy = "YearsOfExperience")]
    public IActionResult AddProposal(ProposalModel proposal)
    {
        if (ModelState.IsValid)
            proposalRepo.Add(proposal);
        return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var proposal = proposalRepo.GetById(id);
        var result = await authorizationService
            .AuthorizeAsync(User, proposal, "EditProposal");

        if (!result.Succeeded)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        return View("EditProposal", proposal);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProposalModel proposal)
    {
        var result = await authorizationService
            .AuthorizeAsync(User, proposal, "EditProposal");

        if (!result.Succeeded)
        {
            return RedirectToAction("AccessDenied", "Account");
        }


        if (ModelState.IsValid)
        {
            proposalRepo.EditProposal(proposal);
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }
        return View("EditProposal", proposal);
    }

    [Authorize(Policy = "IsOrganizer")]
    public IActionResult Approve(int proposalId)
    {
        var proposal = proposalRepo.Approve(proposalId);
        return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
    }
}
