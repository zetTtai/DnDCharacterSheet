using Enums;
using Models;

namespace Interfaces
{
    public interface IAttributeSettingStrategy
    {
        Sheet SetStrengthAttribute(Sheet sheet, int value);
    }
}
