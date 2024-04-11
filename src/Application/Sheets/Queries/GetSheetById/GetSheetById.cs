using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;

namespace DnDCharacterSheet.Application;

public record GetSheetByIdQuery(int Id) : IRequest<SheetVm>;

public class GetSheetByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetSheetByIdQuery, SheetVm>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<SheetVm> Handle(GetSheetByIdQuery request, CancellationToken cancellationToken)
    {
        var sheet = await _context.Sheets
            .Include(s => s.SheetSkills)
                .ThenInclude(ss => ss.Capability)
            .Include(s => s.SheetAbilities)
                .ThenInclude(sa => sa.Ability)
            .Include(s => s.SheetSavingThrows)
                .ThenInclude(st => st.Capability)
            .FirstOrDefaultAsync(s => s.Id == request.Id);

        return new SheetVm
        {
            CharacterName = sheet!.CharacterName,
            Abilities = _mapper.Map<IEnumerable<AbilityDto>>(sheet!.SheetAbilities),
            Skills = _mapper.Map<IEnumerable<CapabilityDto>>(sheet!.SheetSkills),
            SavingThrows = _mapper.Map<IEnumerable<CapabilityDto>>(sheet!.SheetSavingThrows)
        };
    }
}
