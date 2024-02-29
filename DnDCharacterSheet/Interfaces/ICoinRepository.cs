using Models;

namespace Interfaces
{
    public interface ICoinRepository
    {
        IEnumerable<Coin> GetAllCoins();
    }
}
