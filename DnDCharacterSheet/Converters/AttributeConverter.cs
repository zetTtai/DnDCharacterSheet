using DTOs;
using Interfaces;

namespace Converters
{
    public class AttributeConverter : IConverter<Models.Attribute, AttributeDTO>
    {
        public AttributeDTO Convert(Models.Attribute source)
        {
            return new AttributeDTO
            {
                Name = source.Name.ToString(),
                Value = source.Value,
                Modifier = source.Modifier,
            };
        }

        public IEnumerable<AttributeDTO> Convert(IEnumerable<Models.Attribute> source)
        {
            return source.Select(Convert);
        }
    }
}
