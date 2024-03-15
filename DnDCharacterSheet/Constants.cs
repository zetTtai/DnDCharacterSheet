﻿namespace DnDCharacterSheet;

public static class Constants
{
    public static class AbilitySettingStrategy
    {
        public static class RollingDice
        {
            public const int Min = 3;
            public const int Max = 18;
            public static string InvalidValueError = "Value must be between " + Min + " and " + Max;
        }
    }

    public static class UtilsService
    {
        public static string InvalidAbilityError = "Ability must be STR, DEX, CON, INT, WIS or CHA";
    }

    public static class SettingAbilitiesStrategyFactory
    {
        public static string InvalidMethodError = "Method must be 0 => RollingDice, 1 => PointBuy or 2 => StandardArray";
    }
}
