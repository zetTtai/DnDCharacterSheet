using DnDCharacterSheet.Interfaces;
using DnDCharacterSheet.Models;

namespace DnDCharacterSheet.Services
{
    public class SheetService(IModifierCalculatorService modifierCalculator) : ISheetService
    {
        private readonly IModifierCalculatorService _modifierCalculator = modifierCalculator;

        public Sheet SetStrenghtScore(Sheet sheet, int value)
        {
            string modifier = _modifierCalculator.ValueToModifier(value);
            sheet.StrengthScore = modifier;

            sheet.Skills = ModifyCapabilities(sheet.Skills, modifier, Scores.STR);
            sheet.SavingThrows = ModifyCapabilities(sheet.SavingThrows, modifier, Scores.STR);

            return sheet;
        }

        private static List<Capability> ModifyCapabilities(List<Capability> capabilities, string modifier, Scores asociatedScore)
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