﻿namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Queries;

using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using Namotion.Reflection;
using static Testing;
public class GetSheetByIdTests : BaseTestFixture
{
    [SetUp]
    public async Task AdditionalSetUp()
    {
        await TestSetUp();
        await SeedAbilitiesAndCapabilitiesDatabaseAsync();
    }

    [Test]
    public async Task ShouldReturnRequiredFields()
    {
        // Arrange
        var sheetIds = await CreateSheets(3);
        var query = new GetSheetByIdQuery(sheetIds[0]);

        // Act
        var sheetVm = await SendAsync(query);

        // Assert
        sheetVm.CharacterName.Should().NotBeNull();

        sheetVm.Abilities.Should().NotBeEmpty();
        sheetVm.Abilities!.Count().Should().Be(6);
        sheetVm.Abilities!.First().Should().HasProperty("Id");
        sheetVm.Abilities!.First().Value.Should().Be(-1);

        sheetVm.Skills.Should().NotBeEmpty();
        sheetVm.Skills!.Count().Should().Be(18);
        sheetVm.Skills!.First().Should().HasProperty("Id");
        sheetVm.Skills!.First().Proficiency.Should().BeFalse();

        sheetVm.SavingThrows.Should().NotBeEmpty();
        sheetVm.SavingThrows!.Count().Should().Be(6);
        sheetVm.SavingThrows!.First().Should().HasProperty("Id");
        sheetVm.SavingThrows!.First().Proficiency.Should().BeFalse();
    }

    [Test]
    public async Task IfSheetDoesNotExist_ThrowsValidationException()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        var query = new GetSheetByIdQuery(9999);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(query))
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
        var query = new GetSheetByIdQuery(sheetId);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(query))
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
        var query = new GetSheetByIdQuery(sheetId);

        // Act
        var sheet = await SendAsync(query);

        // Assert
        sheet.Should().NotBeNull();
        sheet!.CharacterName.Should().Be("Sir test testable");
    }

    [Test]
    public async Task Succeeds_IfIsAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Sir test testable"
        });
        await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Another character"
        });

        await RunAsAdministratorAsync();
        var query = new GetSheetByIdQuery(sheetId);

        // Act
        var sheet = await SendAsync(query);

        // Assert
        sheet.Should().NotBeNull();
        sheet!.CharacterName.Should().Be("Sir test testable");

    }
}
