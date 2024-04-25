using System.Net;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;
using static SheetTesting;
using static Testing;

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
    public async Task ShouldDenyAnonymousUser_ReturnResponseWith401()
    {
        // Arrange
        var command = new CreateSheetCommand()
        {
            CharacterName = "Sir Test Testable"
        };

        // Act - Assert
        await ShouldDenyAnonymous(command);
    }

    [Test]
    public async Task IfRequiredFieldsAreMissing_ReturnResponseWith400()
    {
        await RunAsDefaultUserAsync();

        // Arrange
        var command = new CreateSheetCommand()
        {
            CharacterName = string.Empty
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Test, TestCaseSource(nameof(InvalidCharacterNames))]
    public async Task InvalidCharacterName_ReturnResponseWith400(string characterName)
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var command = new CreateSheetCommand()
        {
            CharacterName = characterName
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
        var response = await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(response.Value);

        // Assert
        response.Succeeded.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        if (sheet is null)
        {
            Assert.Fail("Sheet should not be null");
            return;
        }

        sheet.CharacterName.Should().Be(command.CharacterName);
        sheet.SheetAbilities.Should().NotBeEmpty();
        sheet.SheetAbilities.Count().Should().Be(6);
        sheet.SheetAbilities.First().Value.Should().Be(-1);

        sheet.SheetSkills.Should().NotBeEmpty();
        sheet.SheetSkills.Count().Should().Be(18);
        sheet.SheetSkills.First().Proficiency.Should().BeFalse();

        sheet.SheetSavingThrows.Should().NotBeEmpty();
        sheet.SheetSavingThrows.Count().Should().Be(6);
        sheet.SheetSavingThrows.First().Proficiency.Should().BeFalse();

        AssertAuditDetails(sheet, userId);
    }
}
