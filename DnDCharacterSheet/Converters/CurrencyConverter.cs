using DTOs;
using Interfaces;
using Models;

namespace Converters
{
    public class CurrencyConverter : IConverter<Currency, CurrencyDTO>
    {
        public CurrencyDTO Convert(Currency source)
        {
            return new CurrencyDTO()
            {
                Id = source.Id,
                Name = source.Name,
                Initials = source.Initials
            };
        }

        public IEnumerable<CurrencyDTO> Convert(IEnumerable<Currency> source)
        {
            return source.Select(Convert);
        }
    }
}
