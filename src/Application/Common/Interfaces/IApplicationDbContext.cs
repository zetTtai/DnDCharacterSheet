using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Sheet> Sheets { get; }

    DbSet<Ability> Abilities { get; }

    DbSet<Capability> Capabilities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
