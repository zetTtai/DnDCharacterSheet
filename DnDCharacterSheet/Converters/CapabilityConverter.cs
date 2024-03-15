using DTOs;
using Interfaces;
using Models;

namespace Converters;

public class CapabilityConverter : IConverter<Capability, CapabilityDTO>
{
    public CapabilityDTO Convert(Capability source)
    {
        return new CapabilityDTO(
            source.Name,
            source.AssociatedAbility.ToString(),
            source.Value
        );
    }

    public IEnumerable<CapabilityDTO> Convert(IEnumerable<Capability> source)
    {
        return source.Select(Convert);
    }
}
