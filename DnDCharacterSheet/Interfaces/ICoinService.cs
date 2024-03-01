using DTOs;

namespace Interfaces
{
    public interface ICoinService
    {
        IEnumerable<CoinDTO> GetAllCoins();
        CoinDTO AddCoin(CoinDTO coinDto);
        CoinDTO GetCoinById(long id);
    }
}