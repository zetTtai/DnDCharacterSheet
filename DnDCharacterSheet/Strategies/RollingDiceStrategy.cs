using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Models;

namespace Strategies;

public class RollingDiceStrategy(IUtilsService utilsService, CharacterAbilities currentAbility) : IAbilitySettingStrategy
{
    private readonly IUtilsService _utilsService = utilsService;

    public Sheet SetAbility(Sheet sheet, int value)
    {
        if (value is < Constants.AbilitySettingStrategy.RollingDice.Min or
            > Constants.AbilitySettingStrategy.RollingDice.Max)
        {
            throw new BadRequestException(Constants.AbilitySettingStrategy.RollingDice.InvalidValueError);
        }

        string modifier = _utilsService.ValueToAbilityModifier(value);
        sheet.Abilities = _utilsService.ModifyAbilities(sheet.Abilities, value.ToString(), modifier, currentAbility);
        sheet.Skills = _utilsService.ModifyCapabilities(sheet.Skills, modifier, currentAbility);
        sheet.SavingThrows = _utilsService.ModifyCapabilities(sheet.SavingThrows, modifier, currentAbility);
        return sheet;
    }
}
