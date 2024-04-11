using DnDCharacterSheet.Application.Common.Interfaces;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;
public class DeleteSheetCommandValidator : SheetCommandValidatorBase<DeleteSheetCommand>
{
    public DeleteSheetCommandValidator(IApplicationDbContext context, IUser user, IIdentityService identityService)
        : base(context, user, identityService)
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(SheetExistsAndUserIsOwner)
                .WithMessage("You must be the owner of the sheet, and the sheet must exist.")
                .WithErrorCode("OwnerAndExists");
    }
}
