using System.Data.Common;
using DnDCharacterSheet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;

namespace DnDCharacterSheet.Application.FunctionalTests;

public class PostgreSQLTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private NpgsqlConnection _connection = null!;
    private Respawner _respawner = null!;

    public PostgreSQLTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString);

        _connectionString = connectionString;
    }

    public async Task InitialiseAsync()
    {
        _connection = new NpgsqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
