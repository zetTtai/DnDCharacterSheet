using DnDCharacterSheet;
using Interfaces;
using Models;

namespace Services
{
    public class SheetService : ISheetService
    {
        private IAttributeSettingStrategy? _attributeSettingStrategy;
        private IAttributeStrategy? _attributeStrategy;

        public void SetAttributeStrategy(IAttributeStrategy strategy)
        {
            _attributeStrategy = strategy;
        }

        public void SetAttributeSettingStrategy(IAttributeSettingStrategy strategy)
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