using DnDCharacterSheet.Application.Common.Interfaces;

namespace DnDCharacterSheet.Application.Sheets.Commands;
public class SheetCommandValidatorBase<T>(IApplicationDbContext context, IUser user) : AbstractValidator<T>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;

    protected async Task<bool> SheetExistsAndUserIsOwner(int id, CancellationToken cancellationToken)
    {
        var currentSheet = await _context.Sheets
            .Where(s => s.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return currentSheet is not null && currentSheet.CreatedBy == _user.Id;
    }
}
