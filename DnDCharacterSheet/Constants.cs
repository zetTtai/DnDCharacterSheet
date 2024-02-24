﻿namespace DnDCharacterSheet
{
    public static class Constants
    {
        public static class AttributeSettingStrategy
        {
            public static class RollingDice
            {
                public const int Min = 3;
                public const int Max = 18;
                public static string InvalidValueError = "[ERROR] Value must be between " + Min + " and " + Max;
            }
        }
        public static class SheetService
        {
            public static string NoStrategyError = "[ERROR] Strategy was not set";
        }
    }
}
