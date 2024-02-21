using Interfaces;
using Models;
using Enums;
using DTOs;
using DnDCharacterSheet;

namespace Services
{
    public class SheetService(IModifierCalculatorService modifierCalculator) : ISheetService
    {
        private readonly IModifierCalculatorService _modifierCalculator = modifierCalculator;

        private Sheet SetStrengthScoreByRollingDice(Sheet sheet, int value)
        {
            if (value < Constants.MethodsToIncreaseAttributes.RollingDice.min || 
                value > Constants.MethodsToIncreaseAttributes.RollingDice.max)
            {
                throw new Exception("Invalid value, must be between "
                    + Constants.MethodsToIncreaseAttributes.RollingDice.min
                    + " and " + Constants.MethodsToIncreaseAttributes.RollingDice.max);
            }

            string modifier = _modifierCalculator.ValueToModifier(value);
            sheet.StrengthScore = modifier;
            sheet.Skills = ModifyCapabilities(sheet.Skills, modifier, CharacterAttributes.STR);
            sheet.SavingThrows = ModifyCapabilities(sheet.SavingThrows, modifier, CharacterAttributes.STR);
            return sheet;
        }

        private static IEnumerable<Capability> ModifyCapabilities(IEnumerable<Capability> capabilities, string modifier, CharacterAttributes associatedAttribute)
        {
            return capabilities.Select(capability =>
                capability.AssociatedAttribute == associatedAttribute
                ? new Capability
                {
                    Name = capability.Name,
                    AssociatedAttribute = capability.AssociatedAttribute,
                    Value = modifier,
                }
                : capability
            );
        }

        public Sheet SetStrengthAttribute(Sheet sheet, int value, MethodsToIncreaseAttributes method = MethodsToIncreaseAttributes.RollingDice)
        {
            switch (method)
            {
                case MethodsToIncreaseAttributes.StandardArray:
                    throw new NotImplementedException();
                case MethodsToIncreaseAttributes.PointBuy:
                    throw new NotImplementedException();
                case MethodsToIncreaseAttributes.RollingDice:
                default:
                    return SetStrengthScoreByRollingDice(sheet, value);
            }
        }

        public List<CapabilityDTO> ConvertToDTO(IEnumerable<Capability> capabilities, bool areSkills = true)
        {
            return new List<CapabilityDTO>();
        }
    }
}