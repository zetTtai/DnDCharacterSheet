using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Strategies;

namespace Factories;

public class SettingAbilityStrategyFactory(IUtilsService utilsService) : ISettingAbilitiesStrategyFactory
{
    private readonly IUtilsService _utilsService = utilsService;


    public IAbilitySettingStrategy CreateStrategy(MethodsToIncreaseAbilities method, CharacterAbilities currentAbility)
    {
        return method switch
        {
            MethodsToIncreaseAbilities.RollingDice => new RollingDiceStrategy(_utilsService, currentAbility),
            MethodsToIncreaseAbilities.PointBuy => new PointBuyStrategy(),
            MethodsToIncreaseAbilities.StandardArray => new StandardArrayStrategy(),
            _ => throw new BadRequestException(Constants.SettingAbilitiesStrategyFactory.InvalidMethodError),
        };
    }
}
