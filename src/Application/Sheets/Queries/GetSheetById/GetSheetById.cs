using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application;

public record GetSheetByIdQuery(int Id) : IRequest<Result<SheetVm>>;

public class GetSheetByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IUser user,
    IIdentityService identityService) : IRequestHandler<GetSheetByIdQuery, Result<SheetVm>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<SheetVm>> Handle(GetSheetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id], cancellationToken: cancellationToken);

        if (entity is null)
        {
            return Result<SheetVm>.Failure([]);
        }

        if (entity.CreatedBy != _user.Id && !await _identityService.IsInRoleAsync(_user.Id!, Roles.Administrator))
        {
            return Result<SheetVm>.Failure([]);
        }

        var sheet = await _context.Sheets
            .Include(s => s.SheetSkills)
                .ThenInclude(ss => ss.Capability)
            .Include(s => s.SheetAbilities)
                .ThenInclude(sa => sa.Ability)
            .Include(s => s.SheetSavingThrows)
                .ThenInclude(st => st.Capability)
            .FirstOrDefaultAsync(s => s.Id == request.Id);

        var vm = new SheetVm
        {
            CharacterName = sheet!.CharacterName,
            Abilities = _mapper.Map<IEnumerable<AbilityDto>>(sheet!.SheetAbilities),
            Skills = _mapper.Map<IEnumerable<CapabilityDto>>(sheet!.SheetSkills),
            SavingThrows = _mapper.Map<IEnumerable<CapabilityDto>>(sheet!.SheetSavingThrows)
        };

        return Result<SheetVm>.Success(vm);
    }
}
