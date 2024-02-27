using Models;

namespace Interfaces
{
    public interface IUtilsService
    {
        string ValueToAttributeModifier(int value);
        IEnumerable<Models.Attribute> ModifyAttributes(IEnumerable<Models.Attribute> attributes, string value, string modifier, Enums.CharacterAttributes associatedAttribute);
        IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, Enums.CharacterAttributes associatedAttribute);
        Enums.CharacterAttributes StringToCharacterAttribute(string attribute);
    }
}
