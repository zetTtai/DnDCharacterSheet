namespace CleanArchitecture.Domain.Entities;
public class Capability : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required CharacterAbilities AssociatedAbility { get; set; }
    public required string Value { get; set; }
}
