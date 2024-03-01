using DTOs;
using Models;

namespace Interfaces
{
    public interface ICoinRepository
    {
        Coin AddCoin(Coin coin);
        IEnumerable<Coin> GetAllCoins();
        Coin GetCoinById(long id);
    }
}
