using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Sheet> Sheets { get; }

    DbSet<Ability> Abilities { get; }

    DbSet<Capability> Capabilities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
