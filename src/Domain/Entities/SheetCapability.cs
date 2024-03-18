namespace CleanArchitecture.Domain.Entities;
public class SheetCapability
{
    public int SheetId { get; set; }
    public required Sheet Sheet { get; set; }
    public int CapabilityId { get; set; }
    public required Capability Capability { get; set; }

    public required string Modifier { get; set; }
    public required bool Proficiency { get; set; } = false;
}

public class SheetSkill : SheetCapability { }

public class SheetSavingThrow : SheetCapability { }
