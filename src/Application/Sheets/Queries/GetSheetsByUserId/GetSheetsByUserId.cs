using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application;

public record GetSheetsByUserIdQuery() : IRequest<List<SheetUserListItemDto>>;

public class GetSheetsByUserIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IUser user,
    IIdentityService identityService) : IRequestHandler<GetSheetsByUserIdQuery, List<SheetUserListItemDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task<List<SheetUserListItemDto>> Handle(GetSheetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Sheets
            .AsNoTracking()
            .Where(s => s.CreatedBy == _user.Id)
            .OrderBy(s => s.Created);

        var sheets = await query.ToListAsync(cancellationToken);
        var sheetsDto = await query.
            ProjectTo<SheetUserListItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (sheetsDto.Count != sheets.Count) throw new Exception($"{sheetsDto.Count} sheets");

        for ( var i = 0; i < sheetsDto.Count; i++)
        {
            if (sheets[i].LastModifiedBy is null) continue;

            sheetsDto[i].IsModifiedByAdmin = await _identityService.IsInRoleAsync(sheets[i].LastModifiedBy!, Roles.Administrator);
        }

        return sheetsDto;
    }
}
