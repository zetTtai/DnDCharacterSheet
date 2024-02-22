using Interfaces;
using Models;

namespace Services
{
    public class SheetService(IAttributeSettingStrategy attributeSettingStrategy) : ISheetService
    {
        private IAttributeSettingStrategy _attributeSettingStrategy = attributeSettingStrategy;

        public void SetStrategy(IAttributeSettingStrategy strategy)
        {
            _attributeSettingStrategy = strategy;
        }

        public Sheet SetStrengthAttribute(Sheet sheet, int value)
        {
            return _attributeSettingStrategy.SetStrengthAttribute(sheet, value);
        }
    }
}