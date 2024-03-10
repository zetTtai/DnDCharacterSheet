using Enums;

namespace Models;

public class Sheet
{
    private readonly int _id;
    public IEnumerable<Ability> Abilities { get; set; }
    public IEnumerable<Capability> Skills { get; set; }
    public IEnumerable<Capability> SavingThrows { get; set; }

    public Sheet()
    {
        _id = 0;
        Skills = new List<Capability>();
        SavingThrows = new List<Capability>();
        Abilities = new List<Ability>();
        SetUpSheet();
    }
    public Sheet(int id)
    {
        _id = id;
        Skills = new List<Capability>();
        SavingThrows = new List<Capability>();
        Abilities = new List<Ability>();
        SetUpSheet();
    }

    private void SetUpSheet()
    {
        Abilities = (from CharacterAbilities ability in Enum.GetValues(typeof(CharacterAbilities))
                     select new Ability
                     {
                         Name = ability,
                         Value = string.Empty,
                         Modifier = string.Empty,
                     })
                     .ToList();

        Skills = (from CharacterSkills ability in Enum.GetValues(typeof(CharacterSkills))
                  select new Capability
                  {
                      Name = ability.ToString(),
                      AssociatedAbility = (CharacterAbilities)ability,
                      Value = string.Empty,
                  }).ToList();

        SavingThrows = (from CharacterSavingThrows savingThrow in Enum.GetValues(typeof(CharacterSavingThrows))
                        select new Capability
                        {
                            Name = savingThrow.ToString(),
                            AssociatedAbility = (CharacterAbilities)savingThrow,
                            Value = string.Empty,
                        })
                        .ToList();
    }

    public int Id()
    {
        return _id;
    }
}
