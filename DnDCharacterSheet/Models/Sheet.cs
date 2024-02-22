using Enums;

namespace Models
{
    public class Sheet
    {
        private readonly int _id;
        public string? StrengthScore { get; set; }
        public IEnumerable<Capability> Skills { get; set; }
        public IEnumerable<Capability> SavingThrows { get; set; }

        public Sheet()
        {
            _id = 0;
            Skills = new List<Capability>();
            SavingThrows = new List<Capability>();
            SetUpSheet();
        }
        public Sheet(int id)
        {
            _id = id;
            Skills = new List<Capability>();
            SavingThrows = new List<Capability>();
            SetUpSheet();
        }

        private void SetUpSheet()
        {
            Dictionary<string, CharacterAttributes> staticSkills = new()
            {
                {"Athletics", CharacterAttributes.STR },
                {"Acrobatics", CharacterAttributes.DEX },
            };
            Dictionary<string, CharacterAttributes> staticSavingThrows = new()
            {
                {"Strength", CharacterAttributes.STR },
                {"Dexterity", CharacterAttributes.DEX },
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
}
