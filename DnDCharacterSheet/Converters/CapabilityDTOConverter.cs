using DTOs;
using Interfaces;
using Models;

namespace Mappers
{
    public class CapabilityDTOConverter : IConverter<CapabilityDTO, Capability>
    {
        public Capability Convert(CapabilityDTO source)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Capability> Convert(IEnumerable<CapabilityDTO> source)
        {
            throw new NotImplementedException();
        }
    }
}
