namespace DnDCharacterSheet.Domain.ValueObjects;
public class Money : ValueObject
{
    public int CopperPieces { get; set; } = 0;
    public int SilverPieces { get; set; } = 0;
    public int ElectrumPieces { get; set; } = 0;
    public int GoldPieces { get; set; } = 0;
    public int PlatinumPieces {  get; set; } = 0;

    public Money() { }

    public Money(int copperPieces = 0, int silverPieces = 0, int electrumPieces = 0, int goldPieces = 0, int platinumPieces = 0)
    {
        CopperPieces = copperPieces;
        SilverPieces = silverPieces;
        ElectrumPieces = electrumPieces;
        GoldPieces = goldPieces;
        PlatinumPieces = platinumPieces;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CopperPieces;
        yield return SilverPieces;
        yield return ElectrumPieces;
        yield return GoldPieces;
        yield return PlatinumPieces;
    }
}
