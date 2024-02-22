using DTOs;
using Interfaces;
using Models;

namespace Converters
{
    public class SheetConverter(IConverter<Capability, CapabilityDTO> capabilityConverter) : IConverter<Sheet, SheetDTO>
    {
        private readonly IConverter<Capability, CapabilityDTO> capabilityConverter = capabilityConverter;

        public SheetDTO Convert(Sheet source)
        {
            return new SheetDTO()
            {
                Id = source.Id(),
                StrengthScore = source.StrengthScore ?? "",
                Skills = capabilityConverter.Convert(source.Skills),
                SavingThrows = capabilityConverter.Convert(source.SavingThrows),
            };
        }

        public IEnumerable<SheetDTO> Convert(IEnumerable<Sheet> source)
        {
            return source.Select(Convert).ToList();
        }
    }
}
