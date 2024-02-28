using Enums;

namespace Models
{
    public class Sheet
    {
        private readonly int _id;
        public IEnumerable<Attribute> Attributes { get; set; }
        public IEnumerable<Capability> Skills { get; set; }
        public IEnumerable<Capability> SavingThrows { get; set; }

        public Sheet()
        {
            _id = 0;
            Skills = new List<Capability>();
            SavingThrows = new List<Capability>();
            Attributes = new List<Attribute>();
            SetUpSheet();
        }
        public Sheet(int id)
        {
            _id = id;
            Skills = new List<Capability>();
            SavingThrows = new List<Capability>();
            Attributes = new List<Attribute>();
            SetUpSheet();
        }

        private void SetUpSheet()
        {

            Attributes = Enum.GetValues(typeof (CharacterAttributes))
                .Cast<CharacterAttributes>().ToList()
                .Select( attribute => new Attribute
                {
                    Name = attribute,
                    Value = "",
                    Modifier = "",
                }).ToList();

            Dictionary<string, CharacterAttributes> staticSkills = new()
            {
                {"Athletics", CharacterAttributes.STR },
                {"Acrobatics", CharacterAttributes.DEX },
                {"Persuasion", CharacterAttributes.CHA },
                {"History", CharacterAttributes.INT },
                {"Survival", CharacterAttributes.WIS },
                {"Intimidation", CharacterAttributes.CHA },
            };
            Dictionary<string, CharacterAttributes> staticSavingThrows = new()
            {
                {"Strength", CharacterAttributes.STR },
                {"Dexterity", CharacterAttributes.DEX },
                {"Constitution", CharacterAttributes.CON },
                {"Intelligence", CharacterAttributes.INT },
                {"Wisdom", CharacterAttributes.WIS },
                {"Charisma", CharacterAttributes.CHA },
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
