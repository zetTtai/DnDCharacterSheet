using DTOs;
using Models;
using Optional;

namespace Interfaces
{
    public interface ICurrencyRepository
    {
        Currency AddCurrency(Currency currency);
        bool DeleteCurrency(long id);
        IEnumerable<Currency> GetAllCurrency();
        Option<Currency> GetCurrencyById(long id);
        Option<Currency> UpdateCurrency(Currency currency);
    }
}
