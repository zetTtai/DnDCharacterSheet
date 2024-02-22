using Enums;
using Interfaces;
using Models;

namespace Services
{
    public class UtilsService : IUtilsService
    {
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

        public string ValueToAttributeModifier(int value)
        {
            double result = Math.Floor((double)(value - 10) / 2);
            return result > 0 ?
                "+" + result.ToString()
                : result.ToString();
        }
    }
}
