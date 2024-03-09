using Enums;
using Models;

namespace Interfaces;

public interface IUtilsService
{
    string ValueToAbilityModifier(int value);
    IEnumerable<Ability> ModifyAbility(IEnumerable<Ability> abilities, string value, string modifier, CharacterAbilities associatedAbility);
    IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, CharacterAbilities associatedAbility);
    CharacterAbilities StringToCharacterAbility(string ability);
}
