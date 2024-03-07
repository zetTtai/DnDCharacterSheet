namespace Config
{
    public static class Constants
    {
        public static class CurrencyService
        {
            public readonly static string NoCurrencyFoundError = "There is no currency with the given ID";
            public readonly static string CurrencyDeleted = "Currency successfully deleted";

        }

        public static class AttributeSettingStrategy
        {
            public static class RollingDice
            {
                public const int Min = 3;
                public const int Max = 18;
                public readonly static string InvalidValueError = "Value must be between " + Min + " and " + Max;
            }
        }
        public static class SheetService
        {
            public readonly static string NoStrategyError = "Strategy was not set";
        }

        public static class SettingAttributesStrategyFactory
        {
            public readonly static string InvalidMethodError = "Method must be 0 => RollingDice, 1 => PointBuy or 2 => StandardArray";
        }
    }
}
