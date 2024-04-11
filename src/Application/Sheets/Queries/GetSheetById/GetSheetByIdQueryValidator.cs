

using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Sheets.Commands;

namespace DnDCharacterSheet.Application;

public class GetSheetByIdQueryValidator : SheetCommandValidatorBase<GetSheetByIdQuery>
{
    public GetSheetByIdQueryValidator(IApplicationDbContext context, IUser user, IIdentityService identityService)
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
