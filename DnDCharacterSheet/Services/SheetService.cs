using DnDCharacterSheet;
using Enums;
using Interfaces;
using Models;

namespace Services
{
    public class SheetService : ISheetService
    {
        private IAttributeSettingStrategy? _attributeSettingStrategy;

        public void SetAttributeSettingStrategy(IAttributeSettingStrategy strategy)
        {
            _attributeSettingStrategy = strategy;
        }

        public Sheet SetAttribute(Sheet sheet, int value)
        {
            return _attributeSettingStrategy != null ?
                _attributeSettingStrategy.SetStrengthAttribute(sheet, value)
                : throw new Exception(Constants.SheetService.NoStrategyError);
        }
    }
}