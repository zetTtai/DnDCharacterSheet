using Enums;
using Models;

namespace Interfaces
{
    public interface ISheetService
    {
        void SetAttributeSettingStrategy(IAttributeSettingStrategy strategy);
        Sheet SetAttribute(Sheet sheet, int value);
    }
}
