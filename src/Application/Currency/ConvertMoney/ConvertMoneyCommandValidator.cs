namespace DnDCharacterSheet.Application.Currency.ConvertMoney;
public class ConvertMoneyCommandValidator : AbstractValidator<ConvertMoneyCommand>
{
    public ConvertMoneyCommandValidator()
    {
        RuleFor(v => v.CurrentMoney)
            .NotNull();

        RuleFor(v => v.Quantity)
            .GreaterThan(0);

        RuleFor(v => v.SrcCurrency)
            .IsInEnum();

        RuleFor(v => v.DstCurrency)
            .IsInEnum();
    }
}
