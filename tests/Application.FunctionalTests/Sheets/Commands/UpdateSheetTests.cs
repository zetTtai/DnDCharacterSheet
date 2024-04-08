using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

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

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test, TestCaseSource(nameof(InvalidCharacterNames))]
    public async Task InvalidCharacterName_ThrowsValidationException(string characterName)
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Sir test testable"
        });
        var command = new UpdateSheetCommand()
        {
            CharacterName = characterName
        };

        command.Id(sheetId);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task IfSheetDoesNotExist_ThrowsValidationException()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified",
        };
        command.Id(999);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task IfUserIsNotOwner_ThrowsValidationException()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Sir test testable"
        });
        await RunAsUserAsync("Hacker", "Hacker1234!", []);

        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified"
        };
        command.Id(sheetId);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Sir test testable"
        });
        await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Another character"
        });
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
}
