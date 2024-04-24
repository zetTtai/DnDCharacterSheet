using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Security;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application;

[Authorize]
public record GetSheetsByUserIdQuery() : IRequest<Response<List<SheetUserListItemVm>>>;

public class GetSheetsByUserIdQueryHandler(
    IApplicationDbContext context,
    IUser user,
    IIdentityService identityService) : IRequestHandler<GetSheetsByUserIdQuery, Response<List<SheetUserListItemVm>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Response<List<SheetUserListItemVm>>> Handle(GetSheetsByUserIdQuery request, CancellationToken cancellationToken)
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

        return Response<List<SheetUserListItemVm>>.Success(sheetsVm);
    }
}
