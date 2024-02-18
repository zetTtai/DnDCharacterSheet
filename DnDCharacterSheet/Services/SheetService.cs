using Interfaces;
using Models;
using Enums;
using DTOs;

namespace Services
{
    public class SheetService(IModifierCalculatorService modifierCalculator) : ISheetService
    {
        private readonly IModifierCalculatorService _modifierCalculator = modifierCalculator;

        private Sheet SetStrengthScoreByRollingDice(Sheet sheet, int value)
        {
            if (value < 3 || value > 18)
            {
                throw new Exception("Invalid value, must be between 3 and 18");
            }

            string modifier = _modifierCalculator.ValueToModifier(value);
            sheet.StrengthScore = modifier;
            sheet.Skills = ModifyCapabilities(sheet.Skills, modifier, CharacterAttributes.STR);
            sheet.SavingThrows = ModifyCapabilities(sheet.SavingThrows, modifier, CharacterAttributes.STR);
            return sheet;
        }

        private static List<Capability> ModifyCapabilities(List<Capability> capabilities, string modifier, CharacterAttributes asociatedAttribute)
        {
            foreach (var capability in capabilities)
            {
                if (capability.AsociatedAttribute == asociatedAttribute)
                {
                    capability.Value = modifier;
                }
            }
            return capabilities;
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

        public List<CapabilityDTO> ConvertToDTO(List<Capability> capabilities, bool areSkills = true)
        {
            return capabilities.Select(capability => new CapabilityDTO
            {
                // TODO: In the future, there should be a field Id, we set name to simplify current implementation
                Id = capability.Name,
                AsociatedScore = capability.AsociatedAttribute.ToString(),
                Value = capability.Value,
            }).ToList();
        }
    }
}