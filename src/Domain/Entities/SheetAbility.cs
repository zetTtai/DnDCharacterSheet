namespace CleanArchitecture.Domain.Entities;
public class SheetAbility
{
    public int SheetId { get; set; }
    public required Sheet Sheet { get; set; }
    public int AbilityId { get; set; }
    public required Ability Ability { get; set; }

    public required string Value { get; set; }
    public required string Modifier { get; set; }
}
