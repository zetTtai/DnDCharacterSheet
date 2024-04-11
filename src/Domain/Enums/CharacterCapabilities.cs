namespace DnDCharacterSheet.Domain.Enums;
public enum CharacterCapabilities
{
    // Skills
    Acrobatics = CharacterAbilities.DEX* 100 + 1,
    AnimalHandling = CharacterAbilities.WIS* 100 + 2,
    Arcana = CharacterAbilities.INT* 100 + 3,
    Athletics = CharacterAbilities.STR* 100 + 4,
    Deception = CharacterAbilities.CHA* 100 + 5,
    History = CharacterAbilities.INT* 100 + 6,
    Insight = CharacterAbilities.WIS* 100 + 7,
    Intimidation = CharacterAbilities.CHA* 100 + 8,
    Investigation = CharacterAbilities.INT* 100 + 9,
    Medicine = CharacterAbilities.WIS* 100 + 10,
    Nature = CharacterAbilities.INT* 100 + 11,
    Perception = CharacterAbilities.WIS* 100 + 12,
    Performance = CharacterAbilities.CHA* 100 + 13, 
    Persuasion = CharacterAbilities.CHA* 100 + 14,
    Religion = CharacterAbilities.INT* 100 + 15,
    SleightOfHand = CharacterAbilities.DEX* 100 + 16,
    Stealth = CharacterAbilities.DEX* 100 + 17,
    Survival = CharacterAbilities.WIS* 100 + 18,

    // SavingThrows
    Strength = CharacterAbilities.STR * 100 + 19,
    Dexterity = CharacterAbilities.DEX * 100 + 20,
    Constitution = CharacterAbilities.CON * 100 + 21,
    Intelligence = CharacterAbilities.INT * 100 + 22,
    Wisdom = CharacterAbilities.WIS * 100 + 23,
    Charisma = CharacterAbilities.CHA * 100 + 24
}

