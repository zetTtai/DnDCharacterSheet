namespace DnDCharacterSheet
{
    public static class Constants
    {
        public static class AttributeSettingStrategy
        {
            public static class RollingDice
            {
                public const int Min = 3;
                public const int Max = 18;
                public static string InvalidValueError = "Value must be between " + Min + " and " + Max;
            }
        }
        public static class SheetService
        {
            public static string NoStrategyError = "Strategy was not set";
        }

        public static class SettingAttributesStrategyFactory
        {
            public static string InvalidMethodError = "Method must be 0 => RollingDice, 1 => PointBuy or 2 => StandardArray";
        }
    }
}
