using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands;
public class SheetCommandValidatorBase<T>(IApplicationDbContext context, IUser user, IIdentityService identityService) : AbstractValidator<T>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    protected async Task<bool> SheetExistsAndUserIsOwner(int id, CancellationToken cancellationToken)
    {
        var currentSheet = await _context.Sheets
            .Where(s => s.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        var isAdmin = await _identityService.IsInRoleAsync(_user.Id!, Roles.Administrator);

        return currentSheet is not null && (currentSheet.CreatedBy == _user.Id || isAdmin);
    }
}
