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

    public ProposalController(IConferenceRepository conferenceRepo, IProposalRepository proposalRepo)
    {
        this.conferenceRepo = conferenceRepo;
        this.proposalRepo = proposalRepo;
    }

    public async Task<IActionResult> Index(int conferenceId)
    {
        var conference = await conferenceRepo.GetById(conferenceId);
        ViewBag.Title = $"Speaker - Proposals For Conference {conference.Name} {conference.Location}";
        ViewBag.ConferenceId = conferenceId;

        return View(proposalRepo.GetAllForConference(conferenceId));
    }

    public IActionResult AddProposal(int conferenceId)
    {
        ViewBag.Title = "Speaker - Add Proposal";
        return View(new ProposalModel { ConferenceId = conferenceId });
    }

    [HttpPost]
    public async Task<IActionResult> AddProposal(ProposalModel proposal)
    {
        if (ModelState.IsValid)
            await proposalRepo.Add(proposal);
        return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
    }

    public async Task<IActionResult> Approve(int proposalId)
    {
        var proposal = await proposalRepo.Approve(proposalId);
        return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
    }
}
