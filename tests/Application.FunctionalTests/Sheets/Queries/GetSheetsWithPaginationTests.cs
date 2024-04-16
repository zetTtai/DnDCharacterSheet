namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Queries;

using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheets;
using Namotion.Reflection;
using static Testing;
using static SheetTesting;
public class GetSheetsWithPaginationTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnRequiredFields()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        await CreateSheets(1);
        await RunAsAdministratorAsync();
        var query = new GetSheetsWithPaginationQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        var sheet = result.Items.First();

        sheet.CreatedByName.Should().NotBeNull();
        sheet.CreatedBy.Should().NotBeNull();
        sheet.LastModifiedByName.Should().NotBeNull();
        sheet.LastModifiedBy.Should().NotBeNull();
        sheet.Should().HasProperty("Id");
        sheet.Should().HasProperty("Created");
        sheet.Should().HasProperty("LastModified");
    }

    [Test]
    public async Task IfUserIsNotAdmin_ThrowsForbiddenAccessException()
    {
        // Arrange
        await CreateSheets(1);
        await RunAsUserAsync("NormalUser", "Other1234!", []);
        var query = new GetSheetsWithPaginationQuery();

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(query))
            .Should()
            .ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        // Arrange
        var query = new GetSheetsWithPaginationQuery();

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(query))
            .Should()
            .ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task Succeeds_ReturnPaginatedListOfSheets()
    {
        // Arrange
        await CreateSheets(20);
        await RunAsAdministratorAsync();
        var query = new GetSheetsWithPaginationQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.TotalCount.Should().Be(20);
        result.TotalPages.Should().Be(2);
    }
}
