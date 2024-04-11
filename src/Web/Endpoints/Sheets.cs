
using DnDCharacterSheet.Application;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheets;

namespace DnDCharacterSheet.Web.Endpoints;

public class Sheets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetSheets)
            .MapGet(GetUserSheets, "user")
            .MapGet(GetSheet, "{id}")
            .MapPost(CreateSheet)
            .MapPut(UpdateSheet, "{id}")
            .MapDelete(DeleteSheet, "{id}");
    }

    public Task<PaginatedList<SheetAdminListItemVm>> GetSheets(ISender sender, [AsParameters] GetSheetsWithPaginationQuery query)
        => sender.Send(query);

    public Task<List<SheetUserListItemVm>> GetUserSheets(ISender sender) => sender.Send(new GetSheetsByUserIdQuery());

    public Task<SheetVm> GetSheet(ISender sender, int id) => sender.Send(new GetSheetByIdQuery(id));

    public Task<int> CreateSheet(ISender sender, CreateSheetCommand command) => sender.Send(command);

    public async Task<IResult> UpdateSheet(ISender sender, int id, UpdateSheetCommand command)
    {
        command.Id(id);
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteSheet(ISender sender, int id)
    {
        await sender.Send(new DeleteSheetCommand(id));
        return Results.NoContent();
    }
}
