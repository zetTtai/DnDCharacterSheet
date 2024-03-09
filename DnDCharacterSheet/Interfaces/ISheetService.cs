using Models;

namespace Interfaces;

public interface ISheetService
{
    void SetAbilitySettingStrategy(IAbilitySettingStrategy strategy);
    Sheet SetAbility(Sheet sheet, int value);
}
