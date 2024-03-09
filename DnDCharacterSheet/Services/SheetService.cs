using DnDCharacterSheet;
using Interfaces;
using Models;

namespace Services;

public class SheetService : ISheetService
{
    private IAbilitySettingStrategy? _attributeSettingStrategy;

    public void SetAbilitySettingStrategy(IAbilitySettingStrategy strategy)
    {
        _attributeSettingStrategy = strategy;
    }

    public Sheet SetAbility(Sheet sheet, int value)
    {
        return _attributeSettingStrategy != null ?
            _attributeSettingStrategy.SetAbility(sheet, value)
            : throw new Exception(Constants.SheetService.NoStrategyError);
    }
}