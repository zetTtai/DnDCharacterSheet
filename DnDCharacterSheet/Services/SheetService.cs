using Enums;
using Interfaces;
using Models;

namespace Services;

public class SheetService(IAbilitySettingStrategy strategy) : ISheetService
{
    private IAbilitySettingStrategy _abilitySettingStrategy = strategy ?? throw new ArgumentNullException();

    public void SetAbilitySettingStrategy(IAbilitySettingStrategy strategy)
    {
        _abilitySettingStrategy = strategy;
    }

    public Sheet SetAbility(Sheet sheet, int value, CharacterAbilities currentAbility)
    {
        return _abilitySettingStrategy.SetAbility(sheet, value, currentAbility);
    }
}