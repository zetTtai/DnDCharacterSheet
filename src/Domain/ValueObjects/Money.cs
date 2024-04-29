
namespace DnDCharacterSheet.Domain.ValueObjects;
public class Money : ValueObject
{
    public int CooperPieces { get; set; } = 0;
    public int SilverPieces { get; set; } = 0;
    public int ElectrumPieces { get; set; } = 0;
    public int GoldPieces { get; set; } = 0;
    public int PlatinumPieces {  get; set; } = 0;

    public Money() { }

    public Money(int cooperPieces, int silverPieces, int electrumPieces, int goldPieces, int platinumPieces)
    {
        CooperPieces = cooperPieces;
        SilverPieces = silverPieces;
        ElectrumPieces = electrumPieces;
        GoldPieces = goldPieces;
        PlatinumPieces = platinumPieces;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CooperPieces;
        yield return SilverPieces;
        yield return ElectrumPieces;
        yield return GoldPieces;
        yield return PlatinumPieces;
    }
}
