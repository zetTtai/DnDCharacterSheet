using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
public class CreateSheetCommandValidator : AbstractValidator<CreateSheetCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateSheetCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.CharacterName)
            .NotEmpty()
            .MinimumLength(SheetConstants.CharacterNameMinLength)
            .MaximumLength(SheetConstants.CharacterNameMaxLength);
    }
}
