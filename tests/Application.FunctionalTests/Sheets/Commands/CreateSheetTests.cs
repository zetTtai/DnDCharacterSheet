using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

using static Testing;

public class CreateSheetTests : BaseTestFixture
{
    [Test]
    public async Task CreateSheet_IfRequiredFieldsAreMissing_ThrowsValidationException()
    {
        var command = new CreateSheetCommand()
        {
            CharacterName = string.Empty
        };
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateSheet_IfCharacterNameTooShort_ThrowsValidationException()
    {
        var command = new CreateSheetCommand()
        {
            CharacterName = new string('a', 3)
        };
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateSheet_IfCharacterNameTooLong_ThrowsValidationException()
    {
        var command = new CreateSheetCommand()
        {
            CharacterName = new string('a', 101)
        };
        await FluentActions.Invoking(() =>
            SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateSheet_Succeeds()
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
        sheet!.SheetSavingThrows.Should().NotBeEmpty();
        sheet!.SheetSkills.Should().NotBeEmpty();
        sheet!.SheetAbilities.Should().NotBeEmpty();

        sheet.CreatedBy.Should().Be(userId);
        sheet.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
