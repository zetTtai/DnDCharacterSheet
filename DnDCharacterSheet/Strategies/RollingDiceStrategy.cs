using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;

namespace Strategies
{
    public class RollingDiceStrategy(IUtilsService utilsService) : IAttributeSettingStrategy
    {
        private readonly IUtilsService _utilsService = utilsService;

        public Sheet SetStrengthAttribute(Sheet sheet, int value)
        {
            if (value < Constants.AttributeSettingStrategy.RollingDice.Min ||
                value > Constants.AttributeSettingStrategy.RollingDice.Max)
            {
                throw new BadRequestException(Constants.AttributeSettingStrategy.RollingDice.InvalidValueError);
            }

            string modifier = _utilsService.ValueToAttributeModifier(value);
            sheet.StrengthAttribute = modifier;
            sheet.Skills = _utilsService.ModifyCapabilities(sheet.Skills, modifier, CharacterAttributes.STR);
            sheet.SavingThrows = _utilsService.ModifyCapabilities(sheet.SavingThrows, modifier, CharacterAttributes.STR);
            return sheet;
        }
    }
}
