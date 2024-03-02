using DnDCharacterSheet;
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

        public bool DeleteCoin(long id)
        {
            var coinToDelete = GetCoinById(id);
            if (coinToDelete == null)
            {
                return false;
            }

            _dbContext.Coins.Remove(coinToDelete);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<Coin> GetAllCoins()
        {
            return _dbContext.Coins.ToList();
        }

        public Coin? GetCoinById(long id)
        {
            return _dbContext.Coins.Find(id);
        }

        public Coin? UpdateCoin(Coin coin)
        {
            var existingCoin = GetCoinById(coin.Id);
            if (existingCoin == null)
            {
                return null;
            }

            existingCoin.Name = coin.Name;
            existingCoin.Initials = coin.Initials;

            _dbContext.SaveChanges();
            return existingCoin;
        }

    }
}
