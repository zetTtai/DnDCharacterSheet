using DTOs;
using Models;

namespace Interfaces
{
    public interface ICurrencyRepository
    {
        Currency AddCurrency(Currency currency);
        bool DeleteCurrency(long id);
        IEnumerable<Currency> GetAllCurrency();
        Currency? GetCurrencyById(long id);
        Currency? UpdateCurrency(Currency currency);
    }
}
