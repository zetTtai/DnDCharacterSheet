using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;
using Ability = Models.Ability;

namespace Services;

public class UtilsService : IUtilsService
{
    public IEnumerable<Ability> ModifyAbility(IEnumerable<Ability> attributes, string value, string modifier, CharacterAbilities associatedAttribute)
    {
        return attributes.Select(attribute =>
            attribute.Name == associatedAttribute
            ? new Attribute
            {
                Name = attribute.Name,
                Value = value,
                Modifier = modifier,
            }
            : attribute
        );
    }

    public IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, CharacterAbilities associatedAttribute)
    {
        return capabilities.Select(capability =>
            capability.AssociatedAttribute == associatedAttribute
            ? new Capability
            {
                Name = capability.Name,
                AssociatedAttribute = capability.AssociatedAttribute,
                Value = modifier,
            }
            : capability
        );
    }

    public CharacterAbilities StringToCharacterAbility(string attribute)
    {
        return Enum.TryParse(attribute.ToUpper(), out CharacterAbilities result)
            ? result
            : throw new BadRequestException(Constants.UtilsService.InvalidAttributeError);
    }

    public string ValueToAbilityModifier(int value)
    {
        double result = Math.Floor((double)(value - 10) / 2);
        return result > 0 ?
            "+" + result.ToString()
            : result.ToString();
    }
}
