namespace DnDCharacterSheet.Domain.Entities;
public class SheetAbility
{
    public int SheetId { get; set; }
    public Sheet? Sheet { get; set; }
    public int AbilityId { get; set; }
    public Ability? Ability { get; set; }

    public int Value { get; set; } = -1;
}
