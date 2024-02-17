namespace DnDCharacterSheet.Models
{
    public class SheetDTO
    {
        public int Id { get; set; }
        public string? StrengthScore { get; set; }
        public List<CapabilityDTO>? Skills { get; set; }
        public List<CapabilityDTO>? SavingThrows { get; set; }
    }
}
