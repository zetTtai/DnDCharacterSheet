using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;

namespace Services
{
    public class UtilsService : IUtilsService
    {
        public IEnumerable<Models.Attribute> ModifyAttributes(IEnumerable<Models.Attribute> attributes, string value, string modifier, CharacterAttributes associatedAttribute)
        {
            return attributes.Select(attribute =>
                attribute.Name == associatedAttribute
                ? new Models.Attribute
                {
                    Name = attribute.Name,
                    Value = value,
                    Modifier = modifier,
                }
                : attribute
            );
        }

        public IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, CharacterAttributes associatedAttribute)
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

        public CharacterAttributes StringToCharacterAttribute(string attribute)
        {
            if (Enum.TryParse(attribute.ToUpper(), out CharacterAttributes result))
            {
                return result;
            }

            throw new BadRequestException(Constants.UtilsService.InvalidAttributeError);
        }

        public string ValueToAttributeModifier(int value)
        {
            double result = Math.Floor((double)(value - 10) / 2);
            return result > 0 ?
                "+" + result.ToString()
                : result.ToString();
        }
    }
}
