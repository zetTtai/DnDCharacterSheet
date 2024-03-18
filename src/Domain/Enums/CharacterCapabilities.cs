namespace CleanArchitecture.Domain.Enums;
public enum CharacterCapabilities
{
    // Multiply by 100 + n to make them unique and do not lose the relation with CharacterAbilities
    // To retrieve the associatedAbility you have to divide by 100

    // Skills
    Acrobatics = CharacterAbilities.DEX * 100 + 1,
    AnimalHandling = CharacterAbilities.WIS * 100 + 2,
    Arcana = CharacterAbilities.INT * 100 + 3,
    Athletics = CharacterAbilities.STR * 100 + 4,
    Deception = CharacterAbilities.CHA * 100 + 5,
    History = CharacterAbilities.INT * 100 + 6,
    Insight = CharacterAbilities.WIS * 100 + 7,
    Intimidation = CharacterAbilities.CHA * 100 + 8,
    Investigation = CharacterAbilities.INT * 100 + 9,
    Medicine = CharacterAbilities.WIS * 100 + 10,
    Nature = CharacterAbilities.INT * 100 + 11,
    Perception = CharacterAbilities.WIS * 100 + 12,
    Persuasion = CharacterAbilities.CHA * 100 + 13,
    Religion = CharacterAbilities.INT * 100 + 14,
    SleightOfHand = CharacterAbilities.DEX * 100 + 15,
    Stealth = CharacterAbilities.DEX * 100 + 16,
    Survival = CharacterAbilities.WIS * 100 + 17,

    // SavingThrows
    Strength = CharacterAbilities.STR * 100 + 18,
    Dexterity = CharacterAbilities.DEX * 100 + 19,
    Constitution = CharacterAbilities.CON * 100 + 20,
    Intelligence = CharacterAbilities.INT * 100 + 21,
    Wisdom = CharacterAbilities.WIS * 100 + 22,
    Charisma = CharacterAbilities.CHA * 100 + 23
}
