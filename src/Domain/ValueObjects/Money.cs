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

    private static (int lowerCurrency, int higherCurrency) ConvertCurrency(int lowerCurrency, int higherCurrency, int conversionRate, int quantity)
    {
        int additionalHigher = quantity / conversionRate;
        higherCurrency += additionalHigher;
        lowerCurrency = lowerCurrency - quantity + (quantity % conversionRate);

        return (lowerCurrency, higherCurrency);
    }

    public void ConvertCopperToSilver(int quantity) => (CopperPieces, SilverPieces) = ConvertCurrency(CopperPieces, SilverPieces, 10, quantity);
    public void ConvertSilverToElectrum(int quantity) => (SilverPieces, ElectrumPieces) = ConvertCurrency(SilverPieces, ElectrumPieces, 5, quantity);
    public void ConvertElectrumToGold(int quantity) => (ElectrumPieces, GoldPieces) = ConvertCurrency(ElectrumPieces, GoldPieces, 2, quantity);
    public void ConvertGoldToPlatinum(int quantity) => (GoldPieces, PlatinumPieces) = ConvertCurrency(GoldPieces, PlatinumPieces, 10, quantity);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CopperPieces;
        yield return SilverPieces;
        yield return ElectrumPieces;
        yield return GoldPieces;
        yield return PlatinumPieces;
    }
}
