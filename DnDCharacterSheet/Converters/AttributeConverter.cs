using DTOs;
using Interfaces;
using Ability = Models.Ability;

namespace Converters;

public class AttributeConverter : IConverter<Ability, AttributeDTO>
{
    public AttributeDTO Convert(Ability source)
    {
        return new AttributeDTO
        {
            Name = source.Name.ToString(),
            Value = source.Value,
            Modifier = source.Modifier,
        };
    }

    public IEnumerable<AttributeDTO> Convert(IEnumerable<Ability> source)
    {
        return source.Select(Convert);
    }
}
