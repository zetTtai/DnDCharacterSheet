using DTOs;
using Interfaces;
using Models;
using Ability = Models.Ability;

namespace Converters;

public class SheetConverter(
    IConverter<Capability, CapabilityDTO> capabilityConverter,
    IConverter<Ability, AbilityDTO> abilityConverter
    ) : IConverter<Sheet, SheetDTO>
{
    private readonly IConverter<Capability, CapabilityDTO> _capabilityConverter = capabilityConverter;
    private readonly IConverter<Ability, AbilityDTO> _abilityConverter = abilityConverter;


    public SheetDTO Convert(Sheet source)
    {
        return new SheetDTO(source.Id(),
            _abilityConverter.Convert(source.Abilities),
            _capabilityConverter.Convert(source.Skills),
            _capabilityConverter.Convert(source.SavingThrows)
        );
    }

    public IEnumerable<SheetDTO> Convert(IEnumerable<Sheet> source)
    {
        return source.Select(Convert);
    }
}
