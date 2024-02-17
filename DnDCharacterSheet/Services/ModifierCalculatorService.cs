using DnDCharacterSheet.Interfaces;

namespace DnDCharacterSheet.Services
{
    public class ModifierCalculatorService : IModifierCalculatorService
    {
        public string ValueToModifier(int value)
        {
            double result = Math.Floor((double)(value - 10) / 2);
            return result > 0 ? "+" + result.ToString() : result.ToString();
        }
    }
}
