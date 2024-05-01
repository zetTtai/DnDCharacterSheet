using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;
using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Application;

public class SheetVm
{
    public string? CharacterName { get; set; }
    public IEnumerable<AbilityDto>? Abilities { get; set; } = new List<AbilityDto>();
    public IEnumerable<CapabilityDto>? SavingThrows { get; set; } = new List<CapabilityDto>();
    public IEnumerable<CapabilityDto>? Skills { get; set; } = new List<CapabilityDto>();
    public Money? Money { get; set; }
}
