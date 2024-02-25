using Enums;

namespace Interfaces
{
    public interface ISettingAttributeStrategyFactory
    {
        IAttributeSettingStrategy CreateStrategy(MethodsToIncreaseAttributes method);
    }
}
