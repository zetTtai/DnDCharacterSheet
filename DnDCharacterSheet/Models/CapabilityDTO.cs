namespace DnDCharacterSheet.Models
{
    public class CapabilityDTO
    {
        public required string Id { get; set; }
        // TODO: AsociatedScore may not be necessary in frontend
        public required string AsociatedScore { get; set; }
        public required string Value { get; set; }
    }
}
