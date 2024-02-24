using DnDCharacterSheet;
using Enums;
using Interfaces;
using Strategies;

namespace Factories
{
    public class SettingAttributeStrategyFactory(IUtilsService utilsService) : ISettingAttributeStrategyFactory
    {
        private readonly IUtilsService _utilsService = utilsService;

        public IAttributeSettingStrategy CreateStrategy(MethodsToIncreaseAttributes method)
        {
            return method switch
            {
                MethodsToIncreaseAttributes.RollingDice => new RollingDiceStrategy(_utilsService),
                MethodsToIncreaseAttributes.PointBuy => new PointBuyStrategy(),
                MethodsToIncreaseAttributes.StandardArray => new StandardArrayStrategy(),
                _ => throw new ArgumentException(Constants.SettingAttributesStrategyFactory.InvalidMethodError),
            };
        }
    }
}
