using DTOs;

namespace Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetAllCurrencies();
        CurrencyDTO AddCurrency(CurrencyRequestDTO request);
        CurrencyDTO GetCurrencyById(long id);
        CurrencyDTO UpdateCurrency(long id, CurrencyRequestDTO request);
        bool DeleteCurrency(long id);
    }
}