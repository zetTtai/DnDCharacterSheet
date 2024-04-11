using System.Threading;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands;
public class SheetCommandValidatorBase<T>(IApplicationDbContext context, IUser user, IIdentityService identityService) : AbstractValidator<T>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    protected async Task<bool> UserIsOwner(int id, CancellationToken cancellationToken)
    {
        var currentSheet = await _context.Sheets
            .Where(s => s.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        var isAdmin = await _identityService.IsInRoleAsync(_user.Id!, Roles.Administrator);

        return currentSheet!.CreatedBy == _user.Id || isAdmin;
    }

    protected async Task<bool> SheetExists(int id, CancellationToken cancellationToken)
    {
        return await _context.Sheets.AnyAsync(s => s.Id == id, cancellationToken);
    }
}
