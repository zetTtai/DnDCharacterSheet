using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Sheets.Commands.CreateSheet;
public class CreateSheetCommandValidator : AbstractValidator<CreateSheetCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateSheetCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        _ = RuleFor(v => v.CharacterName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(10);
    }
}
