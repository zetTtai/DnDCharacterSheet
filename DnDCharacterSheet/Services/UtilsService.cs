using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;

namespace Services;

public class UtilsService : IUtilsService
{
    public IEnumerable<Ability> ModifyAbilities(IEnumerable<Ability> abilities, string value, string modifier, CharacterAbilities associatedAbility)
    {
        return abilities.Select(ability =>
            ability.Name == associatedAbility
            ? new Ability
            {
                Name = ability.Name,
                Value = value,
                Modifier = modifier,
            }
            : ability
        );
    }

    public IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, CharacterAbilities associatedAbility)
    {
        return capabilities.Select(capability =>
            capability.AssociatedAbility == associatedAbility
            ? new Capability
            {
                Name = capability.Name,
                AssociatedAbility = capability.AssociatedAbility,
                Value = modifier,
            }
            : capability
        );
    }

    public string ValueToAbilityModifier(int value)
    {
        double result = Math.Floor((double)(value - 10) / 2);
        return result > 0 ?
            "+" + result.ToString()
            : result.ToString();
    }
}
