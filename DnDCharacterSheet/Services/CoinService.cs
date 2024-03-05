using Config;
using DTOs;
using Exceptions;
using Interfaces;
using Models;

namespace Services
{
    public class CoinService(ICoinRepository repository, IConverter<Coin, CoinDTO> converter) : ICoinService
    {
        private readonly ICoinRepository _repository = repository;
        private readonly IConverter<Coin, CoinDTO> _converter = converter;

        public IEnumerable<CoinDTO> GetAllCoins()
        {
            return _repository.GetAllCoins().Select(_converter.Convert).ToList();
        }

        public CoinDTO GetCoinById(long id)
        {
            var coin = _repository.GetCoinById(id) ?? throw new KeyNotFoundException(Constants.CoinService.NoCoinFoundError);

            return _converter.Convert(coin);
        }

        public CoinDTO AddCoin(CoinRequestDTO request)
        {
            var coin = new Coin
            {
                Name = request.Name ?? throw new BadRequestException(Constants.CoinService.NoNameError),
                Initials = request.Initials ?? throw new BadRequestException(Constants.CoinService.NoInitialsError),
            };

            var addedCoin = _repository.AddCoin(coin);
            return _converter.Convert(addedCoin);
        }

        public CoinDTO UpdateCoin(long id, CoinRequestDTO request)
        {
            var coin = new Coin
            {
                Id = id,
                Name = request.Name ?? throw new BadRequestException(Constants.CoinService.NoNameError),
                Initials = request.Initials ?? throw new BadRequestException(Constants.CoinService.NoInitialsError),
            };

            var updatedCoin = _repository.UpdateCoin(coin) ?? throw new KeyNotFoundException(Constants.CoinService.NoCoinFoundError);
            return _converter.Convert(updatedCoin);
        }

        public bool DeleteCoin(long id)
        {
            return _repository.DeleteCoin(id);
        }
    }
}
