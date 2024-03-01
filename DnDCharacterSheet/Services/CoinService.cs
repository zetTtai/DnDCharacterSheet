using DTOs;
using Exceptions;
using Interfaces;
using Models;

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

        public CoinDTO AddCoin(CoinDTO coinDto)
        {
            var coin = new Coin
            {
                Name = coinDto.Name,
                Initials = coinDto.Initials
            };

            var addedCoin = _repository.AddCoin(coin);
            return new CoinDTO
            {
                Id = addedCoin.Id,
                Name = addedCoin.Name,
                Initials = addedCoin.Initials
            };
        }

        public CoinDTO GetCoinById(long id)
        {
            var coin = _repository.GetCoinById(id);

            return new CoinDTO()
            {
                Id = coin.Id,
                Name = coin.Name,
                Initials = coin.Initials
            };
        }
    }
}
