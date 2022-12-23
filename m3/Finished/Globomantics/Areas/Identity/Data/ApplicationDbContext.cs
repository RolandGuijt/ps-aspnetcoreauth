using Globomantics.Areas.Identity.Data;
using Globomantics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Globomantics.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ConferenceEntity> Conferences => Set<ConferenceEntity>();
    public DbSet<ProposalEntity> Proposals => Set<ProposalEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ConferenceEntity>().HasData(
          new ConferenceEntity
          {
              Id = 1,
              Name = "A Nice Day of Coding",
              Location = "Remote",
              Start = DateTime.Now,
              AttendeeCount = 201
          },
          new ConferenceEntity
          {
              Id = 2,
              Name = "Hackathon Live",
              Location = "New York",
              Start = DateTime.Now.AddDays(50),
              AttendeeCount = 140
          });
        builder.Entity<ProposalEntity>().HasData(
            new ProposalEntity { Id = 1, ConferenceId = 1, Title = "Authentication and Authorization in ASP.NET Core", Speaker = "Roland Guijt" },
            new ProposalEntity { Id = 2, ConferenceId = 1, Title = "ASP.NET Core Basics", Speaker = "Alice Waterman" },
            new ProposalEntity { Id = 3, ConferenceId = 2, Title = "Writing CSS Like a Boss", Speaker = "Deborah Midland" }
        );
    }
}
