using DTOs;
using Interfaces;
using Models;

namespace Converters
{
    public class SheetConverter(
        IConverter<Capability, CapabilityDTO> capabilityConverter,
        IConverter<Models.Attribute, AttributeDTO> attributeConverter
        ) : IConverter<Sheet, SheetDTO>
    {
        private readonly IConverter<Capability, CapabilityDTO> _capabilityConverter = capabilityConverter;
        private readonly IConverter<Models.Attribute, AttributeDTO> _attributeConverter = attributeConverter;


        public SheetDTO Convert(Sheet source)
        {
            return new SheetDTO()
            {
                Id = source.Id(),
                Attributes = _attributeConverter.Convert(source.Attributes),
                Skills = _capabilityConverter.Convert(source.Skills),
                SavingThrows = _capabilityConverter.Convert(source.SavingThrows),
            };
        }

        public IEnumerable<SheetDTO> Convert(IEnumerable<Sheet> source)
        {
            return source.Select(Convert);
        }
    }
}
