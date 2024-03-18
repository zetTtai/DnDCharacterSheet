namespace CleanArchitecture.Domain.Entities;
public class Capability : BaseAuditableEntity
{
    public int AbilityId { get; set; }
    public required string Name { get; set; }
    public IEnumerable<SheetSkill> SheetSkills { get; set; } = new List<SheetSkill>();
    public IEnumerable<SheetSavingThrow> SheetSavingThrows { get; set; } = new List<SheetSavingThrow>();
    public Ability Ability { get; set; } = null!;

}
