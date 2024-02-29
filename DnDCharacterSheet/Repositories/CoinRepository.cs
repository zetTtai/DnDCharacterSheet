using DnDCharacterSheet;
using Interfaces;
using Models;

namespace Repositories
{
    public class CoinRepository(AppDbContext dbContext) : ICoinRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<Coin> GetAllCoins()
        {
            return _dbContext.Coins.ToList();
        }
    }
}
