namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Queries;

using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using Namotion.Reflection;
using static SheetTesting;
using static Testing;
public class GetSheetsByUserIdTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnRequiredFields()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        await CreateSheets(3);
        var query = new GetSheetsByUserIdQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.First().CharacterName.Should().NotBeNull();
        result.First().Should().HasProperty("LastModified");
        result.First().Should().HasProperty("IsModifiedByAdmin");
    }

    [Test]
    public async Task ShouldVerifyIfSheetWasModifiedByAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();

        var sheetIds = await CreateSheets(1);

        await RunAsAdministratorAsync();
        var command = new UpdateSheetCommand()
        {
            CharacterName = "ModifiedByAdmin"
        };
        command.Id(sheetIds[0]);
        await SendAsync(command);

        await RunAsDefaultUserAsync();
        var query = new GetSheetsByUserIdQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.First().IsModifiedByAdmin.Should().BeTrue();
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        await CreateSheets(5);

        await RunAsUserAsync("test", "Test11234!", []);
        for (int i = 0; i < 10; i++)
        {
            await SendAsync(new CreateSheetCommand() { CharacterName = "Test_" + i });
        }

        var userId = await RunAsDefaultUserAsync();
        var query = new GetSheetsByUserIdQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Count.Should().Be(5);
    }
}
