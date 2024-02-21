using Models;
using Enums;
using DTOs;

namespace Interfaces
{
    public interface ISheetService
    {
        Sheet SetStrengthAttribute(Sheet sheet, int value, MethodsToIncreaseAttributes method = MethodsToIncreaseAttributes.RollingDice);
    }
}
