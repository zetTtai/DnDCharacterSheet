namespace CleanArchitecture.Domain.Entities;
public class Ability : BaseAuditableEntity
{
    public required CharacterAbilities Name { get; set; }
    public required string Modifier { get; set; }
    public required string Value { get; set; }
}
