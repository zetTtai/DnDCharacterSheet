
namespace DnDCharacterSheet.Models
{
    public class Capability
    {
        public required string Name { get; set; }
        public required Scores AsociatedScore { get; set; }

        public string? Value { get; set; }
    }
}
