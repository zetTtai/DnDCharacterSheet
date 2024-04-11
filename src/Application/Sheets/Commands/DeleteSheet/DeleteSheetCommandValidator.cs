using DnDCharacterSheet.Application.Common.Interfaces;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;
public class DeleteSheetCommandValidator : SheetCommandValidatorBase<DeleteSheetCommand>
{
    public DeleteSheetCommandValidator(IApplicationDbContext context, IUser user, IIdentityService identityService)
        : base(context, user, identityService)
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(SheetExists)
                .WithMessage("Sheet does not exists")
                .WithErrorCode("Exists")
            .DependentRules(() =>
                RuleFor(v => v.Id)
                .MustAsync(UserIsOwner)
                    .WithMessage("You must be the owner of the sheet")
                    .WithErrorCode("OwnerAndExists")
            );
    }
}
