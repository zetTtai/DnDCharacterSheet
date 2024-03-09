using Enums;

namespace Interfaces;

public interface ISettingAbilitiesStrategyFactory
{
    IAbilitySettingStrategy CreateStrategy(MethodsToIncreaseAbilities method, CharacterAbilities abilities);
}
