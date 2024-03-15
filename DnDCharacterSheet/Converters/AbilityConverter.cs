using DTOs;
using Interfaces;
using Ability = Models.Ability;

namespace Converters;

public class AbilityConverter : IConverter<Ability, AbilityDTO>
{
    public AbilityDTO Convert(Ability source)
    {
        return new AbilityDTO(
            source.Name.ToString(), 
            source.Value, 
            source.Modifier
        );
    }

    public IEnumerable<AbilityDTO> Convert(IEnumerable<Ability> source)
    {
        return source.Select(Convert);
    }
}
