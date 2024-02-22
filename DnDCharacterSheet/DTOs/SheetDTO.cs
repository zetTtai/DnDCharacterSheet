namespace DTOs
{
    public class SheetDTO
    {
        public int Id { get; set; }
        public string? StrengthScore { get; set; }
        public IEnumerable<CapabilityDTO>? Skills { get; set; }
        public IEnumerable<CapabilityDTO>? SavingThrows { get; set; }
    }
}
