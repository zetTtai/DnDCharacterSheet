using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;

namespace Config
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Currency> Currencies { get; set; }
    }
}
