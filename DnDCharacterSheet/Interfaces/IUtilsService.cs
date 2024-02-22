using Enums;
using Models;

namespace Interfaces
{
    public interface IUtilsService
    {
        string ValueToAttributeModifier(int value);
        IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, CharacterAttributes associatedAttribute);
    }
}
