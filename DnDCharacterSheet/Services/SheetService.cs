using DnDCharacterSheet.Interfaces;
using DnDCharacterSheet.Models;

namespace DnDCharacterSheet.Services
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
            sheet.Skills = ModifyCapabilities(sheet.Skills, modifier, Scores.STR);
            sheet.SavingThrows = ModifyCapabilities(sheet.SavingThrows, modifier, Scores.STR);
            return sheet;
        }

        private static List<Capability> ModifyCapabilities(List<Capability> capabilities, string? modifier, Scores asociatedScore)
        {
            foreach (Capability capability in capabilities)
            {
                if (capability.AsociatedScore == asociatedScore)
                {
                    capability.Value = modifier;
                }
            }
            return capabilities;
        }

        public Sheet SetStrenghtScore(Sheet sheet, int value, MethodsToIncreaseScores method = MethodsToIncreaseScores.RollingDice)
        {
            switch (method)
            {
                case MethodsToIncreaseScores.StandardArray:
                    throw new NotImplementedException();
                case MethodsToIncreaseScores.PointBuy:
                    throw new NotImplementedException();
                case MethodsToIncreaseScores.RollingDice:
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
                AsociatedScore = capability.AsociatedScore.ToString(),
                Value = capability.Value ?? "",
            }).ToList();
        }
    }
}