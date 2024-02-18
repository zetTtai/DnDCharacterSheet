using DnDCharacterSheet.Models;

namespace DnDCharacterSheet.Interfaces
{
    public interface ISheetService
    {
        List<CapabilityDTO> ConvertToDTO(List<Capability> capabilities, bool areSkills = true);
        Sheet SetStrenghtScore(Sheet sheet, int value, MethodsToIncreaseScores method = MethodsToIncreaseScores.RollingDice);
    }
}
