using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.Entities;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DnDCharacterSheet.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }

    public static void EnableRLSToAllPublicTables(this WebApplication app, IConfiguration configuration)
    {
        using var scope = app.Services.CreateScope();

        var connectionString = Environment.GetEnvironmentVariable("APPSETTING_CONNECTION_STRING")
            ?? configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Connection string not found.");
            return;
        }

        try
        {
            EnableRLSOnTables(connectionString, GetAllPublicTableNames(connectionString));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error enabling RLS: {ex.Message}");
        }
    }

    private static void EnableRLSOnTables(string connectionString, List<string> tableNames)
    {
        if (tableNames.Count == 0) return;

        using var connection = new NpgsqlConnection(connectionString);

        connection.Open();
        try
        {
            foreach (var name in tableNames)
            {
                using var command = new NpgsqlCommand($"ALTER TABLE \"{name}\" ENABLE ROW LEVEL SECURITY;", connection);
                command.ExecuteNonQuery();
            }
        }
        finally
        {
            connection.Close();
        }
    }

    private static List<string> GetAllPublicTableNames(string connectionString)
    {
        var tableNames = new List<string>();
        // Only retrieve those who RLS is not enable
        var query = @"
            SELECT t.table_name
            FROM information_schema.tables t
            JOIN pg_catalog.pg_class c ON t.table_name = c.relname
            JOIN pg_catalog.pg_namespace n ON c.relnamespace = n.oid
            WHERE t.table_schema = 'public'
                AND t.table_type = 'BASE TABLE'
                AND n.nspname = 'public'
                AND c.relrowsecurity = FALSE;";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string tableName = reader.GetString(0);
                tableNames.Add(tableName);
            }
        }

        return tableNames;
    }
}

public class ApplicationDbContextInitialiser(
    ILogger<ApplicationDbContextInitialiser> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{

    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!context.Abilities.Any() && !context.Capabilities.Any())
        {
            context.Abilities.AddRange(
                (from CharacterAbilities ability in Enum.GetValues(typeof(CharacterAbilities))
                 select new Ability
                 {
                     Name = ability.ToString(),
                     Capabilities =
                            (from CharacterCapabilities capability in Enum.GetValues(typeof(CharacterCapabilities))
                             where (CharacterAbilities)((int)capability / 100) == ability
                             select new Capability
                             {
                                 Name = capability.ToString(),
                             }
                            ).ToList()
                 }
                ).ToList());

            await context.SaveChangesAsync();
        }
    }
}
