using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Strategies;

namespace Factories
{
    public class SettingAttributeStrategyFactory(IUtilsService utilsService) : ISettingAttributeStrategyFactory
    {
        private readonly IUtilsService _utilsService = utilsService;


        public IAttributeSettingStrategy CreateStrategy(MethodsToIncreaseAttributes method, CharacterAttributes currentAttribute)
        {
            return method switch
            {
                MethodsToIncreaseAttributes.RollingDice => new RollingDiceStrategy(_utilsService, currentAttribute),
                MethodsToIncreaseAttributes.PointBuy => new PointBuyStrategy(),
                MethodsToIncreaseAttributes.StandardArray => new StandardArrayStrategy(),
                _ => throw new BadRequestException(Constants.SettingAttributesStrategyFactory.InvalidMethodError),
            };
        }
    }
}
