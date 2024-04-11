using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands.UpdateSheet;
public class UpdateSheetCommandValidator : SheetCommandValidatorBase<UpdateSheetCommand>
{
    public UpdateSheetCommandValidator(IApplicationDbContext context, IUser user, IIdentityService identityService)
        : base(context, user, identityService)
    {
        RuleFor(v => v.CharacterName)
            .NotEmpty()
            .MinimumLength(SheetConstants.CharacterNameMinLength)
            .MaximumLength(SheetConstants.CharacterNameMaxLength);

        RuleFor(v => v.Id())
            .NotEmpty()
            .MustAsync(SheetExists)
                .WithMessage("Sheet does not exists")
                .WithErrorCode("Exists")
            .DependentRules(() =>
                RuleFor(v => v.Id())
                .MustAsync(UserIsOwner)
                    .WithMessage("You must be the owner of the sheet")
                    .WithErrorCode("OwnerAndExists")
            );
    }
}
