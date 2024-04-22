using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Entities;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.Events.Sheets;

namespace DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
public record CreateSheetCommand : IRequest<Response<int>>
{
    public required string CharacterName { get; set; }
}

public class CreateSheetCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSheetCommand, Response<int>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Response<int>> Handle(CreateSheetCommand request, CancellationToken cancellationToken)
    {
        var abilities= await _context.Abilities.ToListAsync(cancellationToken);
        var (skills, savingThrows) = await FetchSheetCapabilities(cancellationToken);

        var entity = new Sheet
        {
            CharacterName = request.CharacterName,
            SheetAbilities = MapToSheetAbilities(abilities),
            SheetSkills = MapToSheetSkills(skills),
            SheetSavingThrows = MapToSheetSavingThrows(savingThrows)
        };

        entity.AddDomainEvent(new SheetCreatedEvent(entity));

        _context.Sheets.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(entity.Id, HttpStatusCode.Created);
    }

    private async Task<(List<Capability> skills, List<Capability> savingThrows)> FetchSheetCapabilities(CancellationToken cancellationToken)
    {
        var staticSavingThrows = Enum.GetNames(typeof(CharacterSavingThrows)).ToList();

        var skillsTask = await _context.Capabilities.Where(s => !staticSavingThrows.Contains(s.Name)).ToListAsync(cancellationToken);
        var savingThrowTask = await _context.Capabilities.Where(st => staticSavingThrows.Contains(st.Name)).ToListAsync(cancellationToken);

        return (skillsTask, savingThrowTask);
    }

    private static List<SheetAbility> MapToSheetAbilities(IEnumerable<Ability> abilities)
        => abilities.Select(a => new SheetAbility { Ability = a }).ToList();

    private static List<SheetSkill> MapToSheetSkills(IEnumerable<Capability> skills)
        => skills.Select(s => new SheetSkill { Capability = s }).ToList();

    private static List<SheetSavingThrow> MapToSheetSavingThrows(IEnumerable<Capability> savingThrows)
        => savingThrows.Select(st => new SheetSavingThrow { Capability = st }).ToList();
}
