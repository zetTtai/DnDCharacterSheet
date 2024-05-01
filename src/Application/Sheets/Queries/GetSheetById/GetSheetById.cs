using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Security;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;

namespace DnDCharacterSheet.Application;

[Authorize]
public record GetSheetByIdQuery(int Id) : IRequest<Response<SheetVm>>;

public class GetSheetByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IUser user,
    IAuthService authorizationService) : IRequestHandler<GetSheetByIdQuery, Response<SheetVm>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IUser _user = user;
    private readonly IAuthService _authorizationService = authorizationService;

    public async Task<Response<SheetVm>> Handle(GetSheetByIdQuery request, CancellationToken cancellationToken)
    {
        var sheet = await _context.Sheets
            .Include(s => s.SheetSkills)
                .ThenInclude(ss => ss.Capability)
            .Include(s => s.SheetAbilities)
                .ThenInclude(sa => sa.Ability)
            .Include(s => s.SheetSavingThrows)
                .ThenInclude(st => st.Capability)
        .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        var response = await _authorizationService.ValidateEntityAccess<SheetVm>(sheet, _user.Id!);

        if (response is not null)
        {
            return Response<SheetVm>.Failure(response.StatusCode, response.Errors);
        }

        var vm = new SheetVm
        {
            CharacterName = sheet!.CharacterName,
            Abilities = _mapper.Map<IEnumerable<AbilityDto>>(sheet!.SheetAbilities),
            Skills = _mapper.Map<IEnumerable<CapabilityDto>>(sheet!.SheetSkills),
            SavingThrows = _mapper.Map<IEnumerable<CapabilityDto>>(sheet!.SheetSavingThrows),
            Money = sheet.Money
        };

        return Response<SheetVm>.Success(vm);
    }
}
