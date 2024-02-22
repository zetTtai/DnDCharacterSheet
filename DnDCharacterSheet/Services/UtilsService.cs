using Interfaces;

namespace Services
{
    public class UtilsService : IUtilsService
    {
        public string ValueToAttributeModifier(int value)
        {
            double result = Math.Floor((double)(value - 10) / 2);
            return result > 0 ?
                "+" + result.ToString()
                : result.ToString();
        }
    }
}
