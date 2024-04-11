using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Domain.Common;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.Entities;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Infrastructure.Data;
using DnDCharacterSheet.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DnDCharacterSheet.Application.FunctionalTests;

[SetUpFixture]
public partial class Testing
{
    private static ITestDatabase _database;
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static string? _userId;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        _database = await TestDatabaseFactory.CreateAsync();

        _factory = new CustomWebApplicationFactory(_database.GetConnection());

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetUserId()
    {
        return _userId;
    }

    public static async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", []);
    }

    public static async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", [Roles.Administrator]);
    }

    public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByNameAsync(userName) ?? new ApplicationUser { UserName = userName, Email = userName };

        var userExists = await userManager.FindByNameAsync(userName);

        IdentityResult result = new();

        if (userExists is null)
        {
            result = await userManager.CreateAsync(user, password);
        }

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (userExists is not null)
        {
            _userId = userExists.Id;
            return _userId;
        }
        else if (result.Succeeded)
        {
            _userId = user.Id;
            return _userId;
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    public static async Task ResetState()
    {
        try
        {
            await _database.ResetAsync();
        }
        catch (Exception) 
        {
        }

        _userId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public static async Task<Sheet?> FindSheetWithDetailsAsync(int id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Use Include and ThenInclude to eagerly load related entities
        var sheet = await context.Sheets
            .Include(s => s.SheetSkills)
                .ThenInclude(ss => ss.Capability)
            .Include(s => s.SheetAbilities)
                .ThenInclude(sa => sa.Ability)
            .Include(s => s.SheetSavingThrows)
                .ThenInclude(st => st.Capability)
            .FirstOrDefaultAsync(s => s.Id == id);

        return sheet;
    }

    public static async Task SeedAbilitiesAndCapabilitiesDatabaseAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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
                         Name = capability.ToString()

                     }).ToList()
             }).ToList());

        await context.SaveChangesAsync();
    }

    public static void AssertAuditDetails(BaseAuditableEntity auditabeEntity, string userId, bool isUpdating = false)
    {
        if (!isUpdating)
        {
            auditabeEntity.CreatedBy.Should().Be(userId);
            auditabeEntity.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            return;
        }
        auditabeEntity.LastModifiedBy.Should().Be(userId);
        auditabeEntity.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }

    public static async Task<List<int>> CreateSheets(int quantity)
    {
        var sheetIds = new List<int>();
        var userId = await RunAsDefaultUserAsync();

        for (int i = 0; i < quantity; i++)
        {
            sheetIds.Add(await SendAsync(new CreateSheetCommand() { CharacterName = "Test_" + i }));
        }

        return sheetIds;
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}
