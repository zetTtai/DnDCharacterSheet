using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Application.Common.Interfaces;
public interface ICurrencyService
{
    public Money ConvertCopperToSilver(Money currentMoney, int quantity);
    public Money ConvertSilverToElectrum(Money currentMoney, int quantity);
    public Money ConvertElectrumToGold(Money currentMoney, int quantity);
    public Money ConvertGoldToPlatinum(Money currentMoney, int quantity);

    public Money ConvertPlatinumToGold(Money mocurrentMoneyney, int quantity);
    public Money ConvertGoldToElectrum(Money currentMoney, int quantity);
    public Money ConvertElectrumToSilver(Money currentMoney, int quantity);
    public Money ConvertSilverToCopper(Money currentMoney, int quantity);

}
