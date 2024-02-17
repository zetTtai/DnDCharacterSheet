
using System.Xml.Linq;

namespace DnDCharacterSheet.Models
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
            Dictionary<string, Scores> staticSkills = new()
            {
                {"Athletics", Scores.STR },
                {"Acrobatics", Scores.DEX },
            };
            Dictionary<string, Scores> staticSavingThrows = new()
            {
                {"Strength", Scores.STR },
                {"Dexterity", Scores.DEX },
            };

            foreach (KeyValuePair<string, Scores> skill in staticSkills)
            {
                Skills.Add(new Capability()
                {
                    Name = skill.Key,
                    AsociatedScore = skill.Value,
                });
            }

            foreach (KeyValuePair<string, Scores> savingThrow in staticSavingThrows)
            {
                SavingThrows.Add(new Capability()
                {
                    Name = savingThrow.Key,
                    AsociatedScore = savingThrow.Value,
                });
            }
        }
    }

    public enum Scores
    {
        STR,
        DEX,
        CON,
        INT,
        WIS,
        CHA,
    }
}
