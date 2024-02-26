using DnDCharacterSheet;
using Enums;
using Exceptions;
using Interfaces;
using Strategies;

namespace Factories
{
    public class AttributeStrategyFactory : IAttributeStrategyFactory
    {
        public IAttributeStrategy CreateStrategy(string attribute)
        {
            return attribute.ToLower() switch
            {
                "str" => new StrengthStrategy(),
                "dex" => throw new NotImplementedException(),
                "con" => throw new NotImplementedException(),
                "int" => throw new NotImplementedException(),
                "wis" => throw new NotImplementedException(),
                "cha" => throw new NotImplementedException(),
                _ => throw new BadRequestException(Constants.SettingAttributesStrategyFactory.InvalidMethodError),
            };
        }

    }
}
