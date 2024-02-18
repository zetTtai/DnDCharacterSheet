using Enums;

namespace Models
{
    public class Sheet
    {
        private readonly int _id;
        public string? StrengthScore { get; set; }
        public List<Capability> Skills { get; set; }
        public List<Capability> SavingThrows { get; set; }

        public Sheet()
        {
            _id = 0;
            Skills = [];
            SavingThrows = [];
            SetUpSheet();
        }
        public Sheet(int id)
        {
            _id = id;
            Skills = [];
            SavingThrows = [];
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

            foreach (KeyValuePair<string, CharacterAttributes> skill in staticSkills)
            {
                Skills.Add(new Capability()
                {
                    Name = skill.Key,
                    AsociatedAttribute = skill.Value,
                    Value = string.Empty,
                });
            }

            foreach (KeyValuePair<string, CharacterAttributes> savingThrow in staticSavingThrows)
            {
                SavingThrows.Add(new Capability()
                {
                    Name = savingThrow.Key,
                    AsociatedAttribute = savingThrow.Value,
                    Value = string.Empty,
                });
            }
        }
    }
}
