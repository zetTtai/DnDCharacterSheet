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
            .MapGet<PaginatedList<SheetAdminListItemVm>>(GetSheets, statusCodes: GetStatusCodes(StatusCodes.Status200OK, bad_request: true, forbidden: true))
            .MapGet<List<SheetUserListItemVm>>(GetUserSheets, "user", statusCodes: GetStatusCodes(StatusCodes.Status200OK))
            .MapGet<SheetVm>(GetSheet, "{id}", statusCodes: GetStatusCodes(StatusCodes.Status201Created, true, true, true))
            .MapPost<int>(CreateSheet, statusCodes: GetStatusCodes(StatusCodes.Status200OK, bad_request: true))
            .MapPut(UpdateSheet, "{id}", statusCodes: GetStatusCodes(StatusCodes.Status204NoContent, true, true, true))
            .MapDelete(DeleteSheet, "{id}", statusCodes: GetStatusCodes(StatusCodes.Status204NoContent, true, forbidden: true));
    }

    public async Task<IResult> GetSheets(ISender sender, [AsParameters] GetSheetsWithPaginationQuery query)
    {
        var response = await sender.Send(query);
        return response.ToActionResult();
    }

    public async Task<IResult> GetUserSheets(ISender sender)
    {
        var response = await sender.Send(new GetSheetsByUserIdQuery());
        return response.ToActionResult();
    }

    public async Task<IResult> GetSheet(ISender sender, int id)
    {
        var response = await sender.Send(new GetSheetByIdQuery(id));
        return response.ToActionResult();
    }

    public async Task<IResult> CreateSheet(ISender sender, CreateSheetCommand command)
    {
        var response = await sender.Send(command);
        return response.ToActionResult();
    }

    public async Task<IResult> UpdateSheet(ISender sender, int id, UpdateSheetCommand command)
    {
        command.Id(id);
        var response = await sender.Send(command);
        return response.ToActionResult();
    }

    public async Task<IResult> DeleteSheet(ISender sender, int id)
    {
        var response = await sender.Send(new DeleteSheetCommand(id));
        return response.ToActionResult();
    }
}
