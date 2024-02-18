
namespace Models
{
    public class Capability
    {
        public required string Name { get; set; }
        public required Scores AsociatedScore { get; set; }
        public required string Value { get; set; }
    }
}
