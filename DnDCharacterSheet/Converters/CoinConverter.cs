using DTOs;
using Interfaces;
using Models;

namespace Converters
{
    public class CoinConverter : IConverter<Coin, CoinDTO>
    {
        public CoinDTO Convert(Coin source)
        {
            return new CoinDTO()
            {
                Id = source.Id,
                Name = source.Name,
                Initials = source.Initials
            };
        }

        public IEnumerable<CoinDTO> Convert(IEnumerable<Coin> source)
        {
            return source.Select(Convert);
        }
    }
}
