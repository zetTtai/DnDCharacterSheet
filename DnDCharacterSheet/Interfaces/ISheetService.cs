using DnDCharacterSheet.Models;

namespace DnDCharacterSheet.Interfaces
{
    public interface ISheetService
    {
        Sheet SetStrenghtScore(Sheet sheet, int value);
    }
}
