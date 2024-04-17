using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application;

public record GetSheetsByUserIdQuery() : IRequest<Result<List<SheetUserListItemVm>>>;

public class GetSheetsByUserIdQueryHandler(
    IApplicationDbContext context,
    IUser user,
    IIdentityService identityService) : IRequestHandler<GetSheetsByUserIdQuery, Result<List<SheetUserListItemVm>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<List<SheetUserListItemVm>>> Handle(GetSheetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Sheets
            .AsNoTracking()
            .Where(s => s.CreatedBy == _user.Id)
            .OrderBy(s => s.Created);

        var sheets = await query.ToListAsync(cancellationToken);
        var sheetsVm = new List<SheetUserListItemVm>();

        foreach (var sheet in sheets)
        {
            if (sheet.LastModifiedBy is null) continue;

            sheetsVm.Add(new SheetUserListItemVm() {
                Id = sheet.Id,
                CharacterName = sheet.CharacterName,
                LastModified = sheet.LastModified,
                IsModifiedByAdmin = await _identityService.IsInRoleAsync(sheet.LastModifiedBy, Roles.Administrator)
            });
        }

        return Result<List<SheetUserListItemVm>>.Success(sheetsVm);
    }
}
