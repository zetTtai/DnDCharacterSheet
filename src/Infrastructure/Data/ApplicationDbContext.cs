using System.Reflection;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Entities;
using DnDCharacterSheet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DnDCharacterSheet.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Sheet> Sheets { get; set; }
    public DbSet<Ability> Abilities { get; set; }
    public DbSet<Capability> Capabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
