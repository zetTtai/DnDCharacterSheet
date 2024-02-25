using DTOs;
using Interfaces;
using Models;

namespace Converters
{
    public class CapabilityConverter : IConverter<Capability, CapabilityDTO>
    {
        public CapabilityDTO Convert(Capability source)
        {
            return new CapabilityDTO()
            {
                Id = source.Name,
                AssociatedAttribute = source.AssociatedAttribute.ToString(),
                Value = source.Value,
            };
        }

        public IEnumerable<CapabilityDTO> Convert(IEnumerable<Capability> source)
        {
            return source.Select(Convert).ToList();
        }
    }
}
