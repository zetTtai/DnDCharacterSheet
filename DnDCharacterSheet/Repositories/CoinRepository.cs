using DnDCharacterSheet;
using Exceptions;
using Interfaces;
using Models;

namespace Repositories
{
    public class CoinRepository(AppDbContext dbContext) : ICoinRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public Coin AddCoin(Coin coin)
        {
            _dbContext.Coins.Add(coin);
            _dbContext.SaveChanges();
            return coin;
        }

        public IEnumerable<Coin> GetAllCoins()
        {
            return _dbContext.Coins.ToList();
        }

        public Coin GetCoinById(long id)
        {
            var coin = _dbContext.Coins.Find(id);
            return coin ?? throw new BadRequestException("There is no Coin with the given id");
        }
    }
}
