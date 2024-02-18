using Models;
using Enums;
using DTOs;

namespace Interfaces
{
    public interface ISheetService
    {
        List<CapabilityDTO> ConvertToDTO(List<Capability> capabilities, bool areSkills = true);
        Sheet SetStrengthAttribute(Sheet sheet, int value, MethodsToIncreaseAttributes method = MethodsToIncreaseAttributes.RollingDice);
    }
}
