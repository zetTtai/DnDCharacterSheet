using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
public class CreateSheetCommandValidator : AbstractValidator<CreateSheetCommand>
{
    public CreateSheetCommandValidator()
    {
        RuleFor(v => v.CharacterName)
            .NotEmpty()
            .MinimumLength(SheetConstants.CharacterNameMinLength)
            .MaximumLength(SheetConstants.CharacterNameMaxLength);
    }
}
