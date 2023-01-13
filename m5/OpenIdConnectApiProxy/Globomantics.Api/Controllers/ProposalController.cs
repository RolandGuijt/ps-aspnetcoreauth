using Globomantics.Client.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Server.Controllers
{
    [ApiController]
    [Route("proposal")]
    [Authorize]
    public class ProposalController : Controller
    {
        private readonly IProposalRepository _Repo;

        public ProposalController(IProposalRepository repo)
        {
            _Repo = repo;
        }

        [HttpGet("{conferenceId}")]
        public IEnumerable<ProposalModel> GetAll(int conferenceId)
        {
            return _Repo.GetAllForConference(conferenceId);
        }

        [HttpPost]
        public IActionResult Add(ProposalModel model)
        {
            var id = _Repo.Add(model);
            return Ok(id);
        }

        [HttpGet("approve/{proposalId}")]
        public IActionResult Approve(int proposalId)
        {
            var prop = _Repo.Approve(proposalId);
            return Ok(prop);
        }
    }
}
