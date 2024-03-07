using Config;
using Interfaces;
using Models;
using Optional;

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
            return GetCurrencyById(id).Match(
                some: currencyToDelete =>
                {
                    _dbContext.Currencies.Remove(currencyToDelete);
                    _dbContext.SaveChanges();
                    return true;
                },
                none: () => false
            );
        }

        public IEnumerable<Currency> GetAllCurrency()
        {
            return _dbContext.Currencies.ToList();
        }

        public Option<Currency> GetCurrencyById(long id)
        {
            var currency = _dbContext.Currencies.Find(id);
            if (currency is null)
            {
                return Option.None<Currency>();
            }
            return Option.Some(currency);
        }

        public Option<Currency> UpdateCurrency(Currency currency)
        {
            return GetCurrencyById(currency.Id).Match(
                some: existingCurrency =>
                {
                    existingCurrency.Name = currency.Name;
                    existingCurrency.Initials  = currency.Initials;

                    _dbContext.SaveChanges();

                    return Option.Some(existingCurrency);
                },
                none: Option.None<Currency>
            );
        }

    }
}
