using DnDCharacterSheet;
using Interfaces;
using Models;

namespace Services
{
    public class SheetService : ISheetService
    {
        private IAttributeSettingStrategy? _attributeSettingStrategy;

        public void SetStrategy(IAttributeSettingStrategy strategy)
        {
            _attributeSettingStrategy = strategy;
        }

        public Sheet SetStrengthAttribute(Sheet sheet, int value)
        {
            return _attributeSettingStrategy != null ?
                _attributeSettingStrategy.SetStrengthAttribute(sheet, value)
                : throw new Exception(Constants.SheetService.NoStrategyError);
        }
    }
}