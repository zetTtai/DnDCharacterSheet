using DTOs;

namespace Interfaces
{
    public interface ICoinService
    {
        IEnumerable<CoinDTO> GetAllCoins();
        CoinDTO AddCoin(CoinRequestDTO request);
        CoinDTO GetCoinById(long id);
        CoinDTO UpdateCoin(long id, CoinRequestDTO request);
        bool DeleteCoin(long id);
    }
}