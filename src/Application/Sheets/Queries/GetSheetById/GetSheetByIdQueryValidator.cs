namespace DnDCharacterSheet.Application;

public class GetSheetByIdQueryValidator : AbstractValidator<GetSheetByIdQuery>
{
    public GetSheetByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}
