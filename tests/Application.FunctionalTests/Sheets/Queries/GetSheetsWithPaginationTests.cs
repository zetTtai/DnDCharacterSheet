namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Queries;

using System.Net;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheets;
using Namotion.Reflection;
using static SheetTesting;
using static Testing;

public class GetSheetsWithPaginationTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser_ReturnResponseWith401()
    {
        // Arrange
        var query = new GetSheetsWithPaginationQuery();

        // Act - Assert
        await ShouldDenyAnonymous(query);
    }

    [Test]
    public async Task ShouldReturnRequiredFields()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        await CreateSheets(1);
        await RunAsAdministratorAsync();
        var query = new GetSheetsWithPaginationQuery();

        // Act
        var response = await SendAsync(query);

        // Assert
        var sheet = response.Value!.Items.First();

        sheet.CreatedByName.Should().NotBeNull();
        sheet.CreatedBy.Should().NotBeNull();
        sheet.LastModifiedByName.Should().NotBeNull();
        sheet.LastModifiedBy.Should().NotBeNull();
        sheet.Should().HasProperty("Id");
        sheet.Should().HasProperty("Created");
        sheet.Should().HasProperty("LastModified");
    }

    [Test]
    public async Task IfUserIsNotAdmin_ReturnResponseWith403()
    {
        // Arrange
        await CreateSheets(1);
        await RunAsUserAsync("NormalUser", "Other1234!", []);
        var query = new GetSheetsWithPaginationQuery();

        // Act 
        var response = await SendAsync(query);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

    }

    [Test]
    public async Task Succeeds_ReturnPaginatedListOfSheets()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        await CreateSheets(20);
        await RunAsAdministratorAsync();
        var query = new GetSheetsWithPaginationQuery();

        // Act
        var response = await SendAsync(query);
        var paginatedList = response.Value;

        // Assert
        if (paginatedList is null)
        {
            Assert.Fail("List should not be null");
            return;
        }
        response.Succeeded.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        paginatedList.TotalCount.Should().Be(20);
        paginatedList.TotalPages.Should().Be(2);
    }
}
