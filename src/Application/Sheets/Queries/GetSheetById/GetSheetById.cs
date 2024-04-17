using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Services;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application;

public record GetSheetByIdQuery(int Id) : IRequest<Result<SheetVm>>;

public class GetSheetByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IUser user,
    IAuthService authorizationService) : IRequestHandler<GetSheetByIdQuery, Result<SheetVm>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IUser _user = user;
    private readonly IAuthService _authorizationService = authorizationService;

    public async Task<Result<SheetVm>> Handle(GetSheetByIdQuery request, CancellationToken cancellationToken)
    {
        var sheet = await _context.Sheets
            .Include(s => s.SheetSkills)
                .ThenInclude(ss => ss.Capability)
            .Include(s => s.SheetAbilities)
                .ThenInclude(sa => sa.Ability)
            .Include(s => s.SheetSavingThrows)
                .ThenInclude(st => st.Capability)
        .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        var result = await _authorizationService.ValidateEntityAccess<SheetVm>(sheet, _user.Id!);

        if (result is not null)
        {
            return result;
        }

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
