using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;

namespace Strategies;

public class RollingDiceStrategy(IUtilsService utilsService, CharacterAbilities currentAttribute) : IAbilitySettingStrategy
{
    private readonly IUtilsService _utilsService = utilsService;

    public Sheet SetAbility(Sheet sheet, int value)
    {
        if (value is < Constants.AttributeSettingStrategy.RollingDice.Min or
            > Constants.AttributeSettingStrategy.RollingDice.Max)
        {
            throw new BadRequestException(Constants.AttributeSettingStrategy.RollingDice.InvalidValueError);
        }

        string modifier = _utilsService.ValueToAbilityModifier(value);
        sheet.Attributes = _utilsService.ModifyAbility(sheet.Attributes, value.ToString(), modifier, currentAttribute);
        sheet.Skills = _utilsService.ModifyCapabilities(sheet.Skills, modifier, currentAttribute);
        sheet.SavingThrows = _utilsService.ModifyCapabilities(sheet.SavingThrows, modifier, currentAttribute);
        return sheet;
    }
}
