using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;
using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Application;

public class SheetVm
{
    public string? CharacterName { get; set; }
    public IEnumerable<AbilityDto> Abilities { get; set; } = Enumerable.Empty<AbilityDto>();
    public IEnumerable<CapabilityDto> SavingThrows { get; set; } = Enumerable.Empty<CapabilityDto>();
    public IEnumerable<CapabilityDto> Skills { get; set; } = Enumerable.Empty<CapabilityDto>();
    public Money? Money { get; set; }
}
