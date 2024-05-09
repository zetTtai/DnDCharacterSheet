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

    private static Money GetUpdatedMoney(Money currentMoney, Currencies srcCurrency, Currencies dstCurrency, int quantity, int conversionRate)
    {
        var isTierUpgrading = srcCurrency < dstCurrency;
        var currentSrcQuantity = currentMoney.GetByCurrency(srcCurrency);
        var currentDstQuantity = currentMoney.GetByCurrency(dstCurrency);

        var srcQuantity = currentSrcQuantity - quantity + (quantity % conversionRate);
        var dstQuantity = currentDstQuantity + quantity / conversionRate;

        if (!isTierUpgrading)
        {
            srcQuantity = currentSrcQuantity - quantity;
            dstQuantity = currentDstQuantity + quantity * conversionRate;
        }

        currentMoney.SetByCurrency(srcCurrency, srcQuantity);
        currentMoney.SetByCurrency(dstCurrency, dstQuantity);

        return currentMoney;
    }

    public Money Convert(Money currentMoney, Currencies srcCurrency, Currencies dstCurrency, int quantity)
    {
        return ConversionRates.TryGetValue((srcCurrency, dstCurrency), out var conversionRate)
            ? GetUpdatedMoney(currentMoney, srcCurrency, dstCurrency, quantity, conversionRate)
            :throw new InvalidOperationException($"Unhandled conversion: FROM {srcCurrency} TO {dstCurrency}");
    }
}
