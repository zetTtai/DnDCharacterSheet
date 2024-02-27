namespace DTOs
{
    public class SheetDTO
    {
        public int Id { get; set; }
        public IEnumerable<AttributeDTO>? Attributes { get; set; }
        public IEnumerable<CapabilityDTO>? Skills { get; set; }
        public IEnumerable<CapabilityDTO>? SavingThrows { get; set; }
    }
}
