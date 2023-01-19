﻿using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Controllers;

[Authorize]
public class ConferenceController : Controller
{
    private readonly IConferenceRepository repo;

    public ConferenceController(IConferenceRepository repo)
    {
        this.repo = repo;
    }

    public IActionResult Index()
    {
        ViewBag.Title = "Organizer - Conference Overview";
        return View(repo.GetAll());
    }

    [Authorize(Policy = "CanAddConference")]
    public IActionResult Add()
    {
        ViewBag.Title = "Organizer - Add Conference";
        return View(new ConferenceModel());
    }

    [HttpPost]
    [Authorize(Policy = "CanAddConference")]
    public IActionResult Add(ConferenceModel model)
    {
        if (ModelState.IsValid)
            repo.Add(model);

        return RedirectToAction("Index");
    }
}
