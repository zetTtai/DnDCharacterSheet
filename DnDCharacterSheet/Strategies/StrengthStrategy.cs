using Interfaces;
using Models;

namespace Strategies
{
    public class StrengthStrategy : IAttributeStrategy
    {
        public IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier)
        {
            throw new NotImplementedException();
        }
    }
}