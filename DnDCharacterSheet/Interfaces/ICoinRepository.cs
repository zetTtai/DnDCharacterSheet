using DTOs;
using Models;

namespace Interfaces
{
    public interface ICoinRepository
    {
        Coin AddCoin(Coin coin);
        bool DeleteCoin(long id);
        IEnumerable<Coin> GetAllCoins();
        Coin? GetCoinById(long id);
        Coin? UpdateCoin(Coin coin);
    }
}
