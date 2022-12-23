using Globomantics.Areas.Identity.Data;
using Globomantics.Data;
using Globomantics.Models;
using Microsoft.EntityFrameworkCore;

namespace Globomantics.Repositories;
public class ConferenceRepository : IConferenceRepository
{
    private readonly ApplicationDbContext _DbContext;

    public ConferenceRepository(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<IEnumerable<ConferenceModel>> GetAll()
    {
        return await _DbContext.Conferences
            .Select(c => new ConferenceModel { 
                Id = c.Id, Name = c.Name, Start = c.Start, 
                Location = c.Location, AttendeeCount = c.AttendeeCount })
            .ToArrayAsync();
    }

    public async Task<ConferenceModel> GetById(int id)
    {
        var c = await _DbContext.Conferences.FirstAsync(c => c.Id == id);
        return new ConferenceModel
        {
            Id = c.Id,
            Name = c.Name,
            Start = c.Start,
            Location = c.Location,
            AttendeeCount = c.AttendeeCount
        };
    }

    public async Task<int> Add(ConferenceModel model)
    {
        var e = new ConferenceEntity
        {
            Name = model.Name,
            Start = model.Start,
            Location = model.Location,
            AttendeeCount = model.AttendeeCount
        };
        _DbContext.Conferences.Add(e);
        await _DbContext.SaveChangesAsync();
        return e.Id;
    }
}


