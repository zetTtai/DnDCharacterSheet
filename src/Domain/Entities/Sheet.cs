namespace CleanArchitecture.Domain.Entities;
public class Sheet : BaseAuditableEntity
{
    public IEnumerable<Ability> Abilities { get; set; }
    public IEnumerable<Capability> Skills { get; set; }
    public IEnumerable<Capability> SavingThrows { get; set; }

    public Sheet()
    {
        Skills = new List<Capability>();
        SavingThrows = new List<Capability>();
        Abilities = new List<Ability>();
        SetUpSheet();
    }

    private void SetUpSheet()
    {
        Abilities =
        (from CharacterAbilities ability in Enum.GetValues(typeof(CharacterAbilities))
         select new Ability
         {
             Name = ability,
             Value = string.Empty,
             Modifier = string.Empty
         })
            .ToList();

        Skills =
        (from CharacterSkills skill in Enum.GetValues(typeof(CharacterSkills))
         select new Capability
         {
             Name = skill.ToString(),
             // Divide by 100 to get the current associated CharacterAbilities
             AssociatedAbility = (CharacterAbilities)((int)skill / 100),
             Value = string.Empty
         }).ToList();

        SavingThrows =
        (from CharacterSavingThrows savingThrow in Enum.GetValues(typeof(CharacterSavingThrows))
         select new Capability
         {
             Name = savingThrow.ToString(),
             AssociatedAbility = (CharacterAbilities)savingThrow,
             Value = string.Empty
         })
            .ToList();
    }
}
