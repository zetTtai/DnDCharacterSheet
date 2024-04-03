using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Domain.Events.Sheets;
public class SheetDeletedEvent(Sheet sheet) : BaseEvent
{
    public Sheet Sheet { get; } = sheet;
}
