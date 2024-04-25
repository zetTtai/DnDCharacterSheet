using System.Diagnostics;
using System.Net;
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
        Debug.Assert(_user.Id is not null, "User ID should never be null here due to AuthorizationBehaviour middleware check.");

        var sheet = await _context.Sheets
            .Include(s => s.SheetSkills)
                .ThenInclude(ss => ss.Capability)
            .Include(s => s.SheetAbilities)
                .ThenInclude(sa => sa.Ability)
            .Include(s => s.SheetSavingThrows)
                .ThenInclude(st => st.Capability)
        .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sheet is null)
        {
            return Response<SheetVm>.Failure(HttpStatusCode.NotFound, [$"Sheet with ID {request.Id} not found."]);
        }

        if (!await _authorizationService.IsOwner(sheet, _user.Id))
        {
            return Response<SheetVm>.Failure(HttpStatusCode.Forbidden, []);
        }

        var vm = new SheetVm
        {
            CharacterName = sheet.CharacterName,
            Abilities = _mapper.Map<IEnumerable<AbilityDto>>(sheet.SheetAbilities),
            Skills = _mapper.Map<IEnumerable<CapabilityDto>>(sheet.SheetSkills),
            SavingThrows = _mapper.Map<IEnumerable<CapabilityDto>>(sheet.SheetSavingThrows)
        };

        return Response<SheetVm>.Success(vm);
    }
}
