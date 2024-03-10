using DnDCharacterSheet;
using Interfaces;
using Models;

namespace Services;

public class SheetService : ISheetService
{
    private IAbilitySettingStrategy? _abilitySettingStrategy;

    public void SetAbilitySettingStrategy(IAbilitySettingStrategy strategy)
    {
        _abilitySettingStrategy = strategy;
    }

    public Sheet SetAbility(Sheet sheet, int value)
    {
        return _abilitySettingStrategy != null ?
            _abilitySettingStrategy.SetAbility(sheet, value)
            : throw new Exception(Constants.SheetService.NoStrategyError);
    }
}