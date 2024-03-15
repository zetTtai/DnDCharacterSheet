
using Enums;

namespace Models;

public class Capability
{
    public required string Name { get; set; }
    public required CharacterAbilities AssociatedAbility { get; set; }
    public required string Value { get; set; }
}
