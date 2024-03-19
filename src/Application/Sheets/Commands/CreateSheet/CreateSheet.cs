

using System.ComponentModel.DataAnnotations;
using System.Linq;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events.Sheets;

namespace CleanArchitecture.Application;

public record CreateSheetCommand : IRequest<int>
{
    public required string CharacterName { get; set; }
}

public class CreateSheetCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSheetCommand, int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(CreateSheetCommand request, CancellationToken cancellationToken)
    {
        var staticSavingThrows = new List<string>();
        foreach (CharacterSavingThrows savingThrow in Enum.GetValues(typeof(CharacterSavingThrows)))
        {
            staticSavingThrows.Add(savingThrow.ToString());
        }

        var abilities = await _context.Abilities
            .ToListAsync(cancellationToken);

        var skills = await _context.Capabilities
            .Where(s => !staticSavingThrows.Contains(s.Name))
            .ToListAsync(cancellationToken);

        var savingThrows = await _context.Capabilities
            .Where(s => staticSavingThrows.Contains(s.Name))
            .ToListAsync(cancellationToken);

        var entity = new Sheet
        {
            CharacterName = request.CharacterName,
        };

        entity.SheetAbilities = abilities.Select(a => new SheetAbility
        {
            Ability = a
        }).ToList();

        entity.SheetSkills = skills.Select(s => new SheetSkill
        {
            Capability = s
        }).ToList();

        entity.SheetSavingThrows = savingThrows.Select(st => new SheetSavingThrow
        {
            Capability = st
        }).ToList();

        entity.AddDomainEvent(new SheetCreatedEvent(entity));
        _context.Sheets.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
