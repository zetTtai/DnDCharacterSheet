using System.Net;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;
using static Testing;
using static SheetTesting;

public class CreateSheetTests : BaseTestFixture
{
    public static IEnumerable<string> InvalidCharacterNames
    {
        get
        {
            return [new string('a', 3), new string('a', 101)];
        }
    }

    [Test]
    public async Task IfRequiredFieldsAreMissing_ReturnStatusCodeBadRequest()
    {
        // Arrange
        var command = new CreateSheetCommand()
        {
            CharacterName = string.Empty
        };

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Test, TestCaseSource(nameof(InvalidCharacterNames))]
    public async Task InvalidCharacterName_ReturnStatusCodeBadRequest(string characterName)
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var command = new CreateSheetCommand()
        {
            CharacterName = characterName
        };

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        await SeedAbilitiesAndCapabilitiesDatabaseAsync();
        var userId = await RunAsDefaultUserAsync();
        var command = new CreateSheetCommand
        {
            CharacterName = "Sir Test Testable"
        };

        // Act
        var result = await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(result.Value);

        // Assert
        result.Succeeded.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        sheet.Should().NotBeNull();

        sheet!.CharacterName.Should().Be(command.CharacterName);
        sheet!.SheetAbilities.Should().NotBeEmpty();
        sheet!.SheetAbilities!.Count().Should().Be(6);
        sheet!.SheetAbilities!.First().Value.Should().Be(-1);

        sheet!.SheetSkills.Should().NotBeEmpty();
        sheet!.SheetSkills!.Count().Should().Be(18);
        sheet!.SheetSkills!.First().Proficiency.Should().BeFalse();

        sheet!.SheetSavingThrows.Should().NotBeEmpty();
        sheet!.SheetSavingThrows!.Count().Should().Be(6);
        sheet!.SheetSavingThrows!.First().Proficiency.Should().BeFalse();

        AssertAuditDetails(sheet, userId);
    }
}
