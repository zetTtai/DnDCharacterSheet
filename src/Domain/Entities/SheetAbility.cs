namespace CleanArchitecture.Domain.Entities;
public class SheetAbility
{
    public int SheetId { get; set; }
    public Sheet? Sheet { get; set; }
    public int AbilityId { get; set; }
    public Ability? Ability { get; set; }

    public string Value { get; set; } = string.Empty;
    public string Modifier { get; set; } = string.Empty;
}
