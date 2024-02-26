using Models;

namespace Interfaces
{
    public interface ISheetService
    {
        void SetAttributeSettingStrategy(IAttributeSettingStrategy strategy);
        void SetAttributeStrategy(IAttributeStrategy strategy);
        Sheet SetStrengthAttribute(Sheet sheet, int value);

        // Sheet SetDexterityAttribute(Sheet sheet, int value);
    }
}
