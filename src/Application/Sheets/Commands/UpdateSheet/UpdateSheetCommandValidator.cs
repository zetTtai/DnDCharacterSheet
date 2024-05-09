using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands.UpdateSheet;
public class UpdateSheetCommandValidator : AbstractValidator<UpdateSheetCommand>
{
    public UpdateSheetCommandValidator()
    {
        RuleFor(v => v.CharacterName)
            .NotEmpty()
            .MinimumLength(SheetConstants.CharacterNameMinLength)
            .MaximumLength(SheetConstants.CharacterNameMaxLength);
    }
}
