using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Controllers;

public class ConferenceController : Controller
{
    private readonly IConferenceRepository repo;

    public ConferenceController(IConferenceRepository repo)
    {
        this.repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = "Organizer - Conference Overview";
        return View(await repo.GetAll());
    }

    public IActionResult Add()
    {
        ViewBag.Title = "Organizer - Add Conference";
        return View(new ConferenceModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(ConferenceModel model)
    {
        if (ModelState.IsValid)
            await repo.Add(model);

        return RedirectToAction("Index");
    }
}
 