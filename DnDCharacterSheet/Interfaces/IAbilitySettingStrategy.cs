using Enums;
using Models;

namespace Interfaces;

public interface IAbilitySettingStrategy
{
    Sheet SetAbility(Sheet sheet, int value, CharacterAbilities currentAbility);
}
