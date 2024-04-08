using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands.UpdateSheet;
public class UpdateSheetCommandValidator : SheetCommandValidatorBase<UpdateSheetCommand>
{
    public UpdateSheetCommandValidator(IApplicationDbContext context, IUser user) : base(context, user)
    {
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
}
