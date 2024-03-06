using Config;
using Interfaces;
using Models;

namespace Repositories
{
    public class CurrencyRepository(AppDbContext dbContext) : ICurrencyRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public Currency AddCurrency(Currency currency)
        {
            _dbContext.Currencies.Add(currency);
            _dbContext.SaveChanges();
            return currency;
        }

        public bool DeleteCurrency(long id)
        {
            var currencyToDelete = GetCurrencyById(id);
            if (currencyToDelete is null)
            {
                return false;
            }

            _dbContext.Currencies.Remove(currencyToDelete);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<Currency> GetAllCurrency()
        {
            return _dbContext.Currencies.ToList();
        }

        public Currency? GetCurrencyById(long id)
        {
            return _dbContext.Currencies.Find(id);
        }

        public Currency? UpdateCurrency(Currency currency)
        {
            var existingcurrency = GetCurrencyById(currency.Id);
            if (existingcurrency == null)
            {
                return null;
            }

            existingcurrency.Name = currency.Name;
            existingcurrency.Initials = currency.Initials;

            _dbContext.SaveChanges();
            return existingcurrency;
        }

    }
}
