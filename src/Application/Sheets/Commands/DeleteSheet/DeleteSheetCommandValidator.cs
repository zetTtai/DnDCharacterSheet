namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;
public class DeleteSheetCommandValidator : AbstractValidator<DeleteSheetCommand>
{
    public DeleteSheetCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}
