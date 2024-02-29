using Microsoft.EntityFrameworkCore;
using Models;

namespace DnDCharacterSheet
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Coin> Coins { get; set; }
    }
}
