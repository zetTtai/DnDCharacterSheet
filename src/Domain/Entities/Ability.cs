namespace CleanArchitecture.Domain.Entities;
public class Ability : BaseAuditableEntity
{
    public required string Name { get; set; }
    public IEnumerable<SheetAbility> SheetAbilities { get; set; } = new List<SheetAbility>();
    public IEnumerable<Capability> Capabilities { get; set; } = new List<Capability>();

}
