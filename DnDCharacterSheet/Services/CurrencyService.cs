using Config;
using DTOs;
using Interfaces;
using Models;

namespace Services
{
    public class CurrencyService(ICurrencyRepository repository, IConverter<Currency, CurrencyDTO> converter) : ICurrencyService
    {
        private readonly ICurrencyRepository _repository = repository;
        private readonly IConverter<Currency, CurrencyDTO> _converter = converter;

        public IEnumerable<CurrencyDTO> GetAllCurrencies()
        {
            return _repository.GetAllCurrency().Select(_converter.Convert).ToList();
        }

        public CurrencyDTO GetCurrencyById(long id)
        {
            var coin = _repository.GetCurrencyById(id) ?? throw new KeyNotFoundException(Constants.CurrencyService.NoCurrencyFoundError);

            return _converter.Convert(coin);
        }

        public CurrencyDTO AddCurrency(CurrencyRequestDTO request)
        {
            var coin = new Currency
            {
                Name = request.Name,
                Initials = request.Initials,
            };

            var addedCoin = _repository.AddCurrency(coin);
            return _converter.Convert(addedCoin);
        }

        public CurrencyDTO UpdateCurrency(long id, CurrencyRequestDTO request)
        {
            var coin = new Currency
            {
                Id = id,
                Name = request.Name,
                Initials = request.Initials,
            };

            var updatedCoin = _repository.UpdateCurrency(coin) ?? throw new KeyNotFoundException(Constants.CurrencyService.NoCurrencyFoundError);
            return _converter.Convert(updatedCoin);
        }

        public bool DeleteCurrency(long id)
        {
            return _repository.DeleteCurrency(id);
        }
    }
}
