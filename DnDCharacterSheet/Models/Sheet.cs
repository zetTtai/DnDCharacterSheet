using Enums;

namespace Models;

public class Sheet
{
    private readonly int _id;
    public IEnumerable<Ability> Attributes { get; set; }
    public IEnumerable<Capability> Skills { get; set; }
    public IEnumerable<Capability> SavingThrows { get; set; }

    public Sheet()
    {
        _id = 0;
        Skills = new List<Capability>();
        SavingThrows = new List<Capability>();
        Attributes = new List<Ability>();
        SetUpSheet();
    }
    public Sheet(int id)
    {
        _id = id;
        Skills = new List<Capability>();
        SavingThrows = new List<Capability>();
        Attributes = new List<Ability>();
        SetUpSheet();
    }

    private void SetUpSheet()
    {

        Attributes = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>().ToList()
            .Select(attribute => new Ability
            {
                Name = attribute,
                Value = "",
                Modifier = "",
            }).ToList();

        Dictionary<string, CharacterAbilities> staticSkills = new()
        {
            {"Athletics", CharacterAbilities.STR },
            {"Acrobatics", CharacterAbilities.DEX },
            {"Persuasion", CharacterAbilities.CHA },
            {"History", CharacterAbilities.INT },
            {"Survival", CharacterAbilities.WIS },
            {"Intimidation", CharacterAbilities.CHA },
        };
        Dictionary<string, CharacterAbilities> staticSavingThrows = new()
        {
            {"Strength", CharacterAbilities.STR },
            {"Dexterity", CharacterAbilities.DEX },
            {"Constitution", CharacterAbilities.CON },
            {"Intelligence", CharacterAbilities.INT },
            {"Wisdom", CharacterAbilities.WIS },
            {"Charisma", CharacterAbilities.CHA },
        };

        Skills = staticSkills.Select(skill => new Capability
        {
            Name = skill.Key,
            AssociatedAttribute = skill.Value,
            Value = string.Empty,
        }).ToList();

        SavingThrows = staticSavingThrows.Select(savingThrow => new Capability
        {
            Name = savingThrow.Key,
            AssociatedAttribute = savingThrow.Value,
            Value = string.Empty,
        }).ToList();
    }

    public int Id()
    {
        return _id;
    }
}
