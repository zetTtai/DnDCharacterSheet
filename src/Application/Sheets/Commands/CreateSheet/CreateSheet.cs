using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events.Sheets;

namespace CleanArchitecture.Application.Sheets.Commands.CreateSheet;

public record CreateSheetCommand : IRequest<int>
{
    public required string CharacterName { get; set; }
}

public class CreateSheetCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSheetCommand, int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(CreateSheetCommand request, CancellationToken cancellationToken)
    {
        List<string> staticSavingThrows = [];
        foreach (CharacterSavingThrows savingThrow in Enum.GetValues(typeof(CharacterSavingThrows)))
        {
            staticSavingThrows.Add(savingThrow.ToString());
        }

        List<Ability> abilities = await _context.Abilities
            .ToListAsync(cancellationToken);

        List<Capability> skills = await _context.Capabilities
            .Where(s => !staticSavingThrows.Contains(s.Name))
            .ToListAsync(cancellationToken);

        List<Capability> savingThrows = await _context.Capabilities
            .Where(s => staticSavingThrows.Contains(s.Name))
            .ToListAsync(cancellationToken);

        Sheet entity = new()
        {
            CharacterName = request.CharacterName,
            SheetAbilities = abilities.Select(a => new SheetAbility
            {
                Ability = a
            }).ToList(),

            SheetSkills = skills.Select(s => new SheetSkill
            {
                Capability = s
            }).ToList(),

            SheetSavingThrows = savingThrows.Select(st => new SheetSavingThrow
            {
                Capability = st
            }).ToList()
        };

        entity.AddDomainEvent(new SheetCreatedEvent(entity));
        _ = _context.Sheets.Add(entity);

        _ = await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
