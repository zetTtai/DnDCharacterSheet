using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

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
    public async Task IfRequiredFieldsAreMissing_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateSheetCommand()
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
        var id = await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(id);

        // Assert
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
