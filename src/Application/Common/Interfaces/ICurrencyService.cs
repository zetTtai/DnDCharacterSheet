using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Application.Common.Interfaces;

public interface ICurrencyService
{
    public Money Convert(Money currentMoney, Currencies srcCurrency, Currencies dstCurrency, int quantity);
}
