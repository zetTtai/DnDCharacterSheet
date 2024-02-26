using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;

namespace Strategies
{
    public class RollingDiceStrategy(IUtilsService utilsService, IAttributeStrategy attributeStrategy) : IAttributeSettingStrategy
    {
        private readonly IUtilsService _utilsService = utilsService;
        private readonly IAttributeStrategy _attributeStrategy = attributeStrategy;

        public Sheet SetStrengthAttribute(Sheet sheet, int value)
        {
            if (value < Constants.AttributeSettingStrategy.RollingDice.Min ||
                value > Constants.AttributeSettingStrategy.RollingDice.Max)
            {
                throw new BadRequestException(Constants.AttributeSettingStrategy.RollingDice.InvalidValueError);
            }

            string modifier = _utilsService.ValueToAttributeModifier(value);
            sheet.StrengthAttribute = modifier;
            sheet.Skills = _attributeStrategy.ModifyCapabilities(sheet.Skills, modifier);
            sheet.SavingThrows = _attributeStrategy.ModifyCapabilities(sheet.SavingThrows, modifier);
            return sheet;
        }
    }
}
