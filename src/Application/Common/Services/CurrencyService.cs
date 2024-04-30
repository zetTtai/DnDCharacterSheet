using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Application.Common.Services;
public class CurrencyService : ICurrencyService
{
    private static readonly Dictionary<(Currencies, Currencies), int> ConversionRates = new()
    {
        { (Currencies.CopperPieces, Currencies.SilverPieces), 10 },
        { (Currencies.SilverPieces, Currencies.CopperPieces), 10 },

        { (Currencies.SilverPieces, Currencies.ElectrumPieces), 5 },
        { (Currencies.ElectrumPieces, Currencies.SilverPieces), 5 },

        { (Currencies.ElectrumPieces, Currencies.GoldPieces), 2 },
        { (Currencies.GoldPieces, Currencies.ElectrumPieces), 2 },

        { (Currencies.GoldPieces, Currencies.PlatinumPieces), 10 },
        { (Currencies.PlatinumPieces, Currencies.GoldPieces), 10 }
    };

    private static Money ConvertCurrency(Money currentMoney, Currencies srcCurrency, Currencies dstCurrency, int quantity)
    {
        var isTierUpgrading = srcCurrency < dstCurrency;

        var srcQuantity = currentMoney.GetByCurrency(srcCurrency);
        var dstQuantity = currentMoney.GetByCurrency(dstCurrency);


        if (!ConversionRates.TryGetValue((srcCurrency, dstCurrency), out var conversionRate))
        {
            throw new InvalidOperationException($"Unhandled conversion: FROM {srcCurrency} TO {dstCurrency}");
        }

        if (isTierUpgrading)
        {
            dstQuantity += quantity / conversionRate;
            srcQuantity = srcQuantity - quantity + (quantity % conversionRate);
        }
        else
        {
            dstQuantity += quantity * conversionRate;
            srcQuantity -= quantity;
        }

        currentMoney.SetByCurrency(srcCurrency, srcQuantity);
        currentMoney.SetByCurrency(dstCurrency, dstQuantity);

        return currentMoney;
    }

    public Money ConvertCopperToSilver(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.CopperPieces, Currencies.SilverPieces, quantity);

    public Money ConvertElectrumToGold(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.ElectrumPieces, Currencies.GoldPieces, quantity);

    public Money ConvertElectrumToSilver(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.ElectrumPieces, Currencies.SilverPieces, quantity);

    public Money ConvertGoldToElectrum(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.GoldPieces, Currencies.ElectrumPieces, quantity);

    public Money ConvertGoldToPlatinum(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.GoldPieces, Currencies.PlatinumPieces, quantity);

    public Money ConvertPlatinumToGold(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.PlatinumPieces, Currencies.GoldPieces, quantity);

    public Money ConvertSilverToCopper(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.SilverPieces, Currencies.CopperPieces, quantity);

    public Money ConvertSilverToElectrum(Money currentMoney, int quantity) => ConvertCurrency(currentMoney, Currencies.SilverPieces, Currencies.ElectrumPieces, quantity);
}
