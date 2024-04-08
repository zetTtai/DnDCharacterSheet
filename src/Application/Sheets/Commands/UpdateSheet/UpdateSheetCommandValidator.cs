using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands.UpdateSheet;
public class UpdateSheetCommandValidator : AbstractValidator<UpdateSheetCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public UpdateSheetCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.CharacterName)
            .NotEmpty()
            .MinimumLength(SheetConstants.CharacterNameMinLength)
            .MaximumLength(SheetConstants.CharacterNameMaxLength);

        RuleFor(v => v.Id())
            .NotEmpty()
            .MustAsync(SheetExistsAndUserIsOwner)
                .WithMessage("You must be the owner of the sheet, and the sheet must exist.")
                .WithErrorCode("OwnerAndExists");
    }

    public async Task<bool> SheetExistsAndUserIsOwner(int Id, CancellationToken cancellationToken)
    {
        var currentSheet = await _context.Sheets
            .Where(s => s.Id == Id)
            .SingleOrDefaultAsync(cancellationToken);

        return currentSheet is not null && currentSheet.CreatedBy == _user.Id;
    }
}
