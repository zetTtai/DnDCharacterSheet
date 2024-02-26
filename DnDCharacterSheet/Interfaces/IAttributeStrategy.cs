using Models;

namespace Interfaces
{
    public interface IAttributeStrategy
    {
        IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier);
    }
}
