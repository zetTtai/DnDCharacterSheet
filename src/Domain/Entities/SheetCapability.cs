namespace DnDCharacterSheet.Domain.Entities;

public class SheetCapability
{
    public int SheetId { get; set; }
    public Sheet? Sheet { get; set; }
    public int CapabilityId { get; set; }
    public Capability? Capability { get; set; }

    public bool Proficiency { get; set; } = false;
}

public class SheetSkill : SheetCapability { }

public class SheetSavingThrow : SheetCapability { }
