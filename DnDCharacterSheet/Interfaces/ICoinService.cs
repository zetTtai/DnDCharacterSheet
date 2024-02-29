using DTOs;

namespace Interfaces
{
    public interface ICoinService
    {
        public IEnumerable<CoinDTO> GetAllCoins();
    }
}