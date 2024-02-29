using DTOs;
using Interfaces;

namespace Services
{
    public class CoinService(ICoinRepository repository) : ICoinService
    {
        private readonly ICoinRepository _repository = repository;

        public IEnumerable<CoinDTO> GetAllCoins()
        {
            return _repository.GetAllCoins().Select(coin => new CoinDTO
            {
                Id = coin.Id,
                Name = coin.Name,
                Initials = coin.Initials
            }).ToList();
        }
    }
}
