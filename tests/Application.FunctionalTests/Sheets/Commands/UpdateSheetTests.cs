using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

using static SheetTesting;
using static Testing;

public class UpdateSheetTests : BaseTestFixture
{
    [SetUp]
    public async Task AdditionalSetUp()
    {
        await TestSetUp();
        await SeedAbilitiesAndCapabilitiesDatabaseAsync();
    }

    public static IEnumerable<string> InvalidCharacterNames
    {
        get
        {
            return [new string('a', 3), new string('a', 101)];
        }
    }

    [Test]
    public async Task IfRequiredFieldsAreMissing_ThrowsValidationException()
    {
        // Arrange
        var command = new UpdateSheetCommand
        {
            CharacterName = string.Empty
        };

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
    }

    [Test, TestCaseSource(nameof(InvalidCharacterNames))]
    public async Task InvalidCharacterName_ThrowsValidationException(string characterName)
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");
        var command = new UpdateSheetCommand()
        {
            CharacterName = characterName
        };

        command.Id(sheetId);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
    }

    [Test]
    public async Task IfSheetDoesNotExist_ThrowsValidationException()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified",
        };
        command.Id(999);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
    }

    [Test]
    public async Task IfUserIsNotOwner_ThrowsValidationException()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        int sheetId = await CreateSingleSheet("Sir Test Testable");
        await RunAsUserAsync("Hacker", "Hacker1234!", []);

        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified"
        };
        command.Id(sheetId);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        int sheetId = await CreateSingleSheet("Sir Test Testable");
        await CreateSingleSheet("Another character");
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified"
        };
        command.Id(sheetId);

        // Act
        await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(sheetId);

        // Assert
        sheet.Should().NotBeNull();

        sheet!.CharacterName.Should().Be("Sir test modified");

        AssertAuditDetails(sheet, userId, true);
    }

    [Test]
    public async Task Succeeds_IfIsAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");
        await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Another character"
        });

        var userId = await RunAsAdministratorAsync();
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified by admin"
        };
        command.Id(sheetId);

        // Act
        await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(sheetId);

        // Assert
        sheet.Should().NotBeNull();

        sheet!.CharacterName.Should().Be("Sir test modified by admin");

        AssertAuditDetails(sheet, userId, true);
    }

}
