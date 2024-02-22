using DnDCharacterSheet;
using Enums;
using Interfaces;
using Models;
using System;

namespace Strategies
{
    public class RollingDiceStrategy(IUtilsService utilsService) : IAttributeSettingStrategy
    {
        private readonly IUtilsService _utilsService = utilsService;

        public Sheet SetStrengthAttribute(Sheet sheet, int value)
        {
            if (value < Constants.MethodsToIncreaseAttributes.RollingDice.Min ||
                value > Constants.MethodsToIncreaseAttributes.RollingDice.Max)
            {
                throw new Exception(Constants.MethodsToIncreaseAttributes.RollingDice.Error);
            }

            string modifier = _utilsService.ValueToAttributeModifier(value);
            sheet.StrengthScore = modifier;
            sheet.Skills = _utilsService.ModifyCapabilities(sheet.Skills, modifier, CharacterAttributes.STR);
            sheet.SavingThrows = _utilsService.ModifyCapabilities(sheet.SavingThrows, modifier, CharacterAttributes.STR);
            return sheet;
        }
    }
}
