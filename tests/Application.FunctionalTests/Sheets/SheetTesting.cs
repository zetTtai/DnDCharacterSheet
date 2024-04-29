using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Domain.Entities;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.ValueObjects;
using DnDCharacterSheet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets;
public partial class SheetTesting : Testing
{
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

    public static async Task<int> CreateSingleSheet(string characterName)
    {
        var createSheetCommand = await SendAsync(new CreateSheetCommand()
        {
            CharacterName = characterName
        });
        var sheetId = createSheetCommand.Value;
        return sheetId;
    }

    public static async Task<List<int>> CreateSheets(int quantity)
    {
        var sheetIds = new List<int>();

        for (int i = 0; i < quantity; i++)
        {
            var response = await SendAsync(new CreateSheetCommand() { CharacterName = "Test_" + i });
            sheetIds.Add(response.Value);
        }

        return sheetIds;
    }

    public static void ValidateMoney(Money money, int cp = 0, int sp = 0, int ep = 0, int gp = 0, int pp = 0)
    {
        money.Should().NotBeNull();
        money.CooperPieces.Should().Be(cp);
        money.SilverPieces.Should().Be(sp);
        money.ElectrumPieces.Should().Be(ep);
        money.GoldPieces.Should().Be(gp);
        money.PlatinumPieces.Should().Be(pp);
    }
}
