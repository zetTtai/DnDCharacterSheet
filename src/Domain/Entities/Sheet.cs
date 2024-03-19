namespace CleanArchitecture.Domain.Entities;

public class Sheet : BaseAuditableEntity
{
    public required string CharacterName { get; set; }
    public IEnumerable<SheetAbility> SheetAbilities { get; set; } = new List<SheetAbility>();
    public IEnumerable<SheetSkill> SheetSkills { get; set; } = new List<SheetSkill>();
    public IEnumerable<SheetSavingThrow> SheetSavingThrows { get; set; } = new List<SheetSavingThrow>();
}
