using System.Net;
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
            .MapGet<PaginatedList<SheetAdminListItemVm>>(GetSheets, statusCodes: [
                StatusCodes.Status200OK,
                StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized, 
                StatusCodes.Status403Forbidden
             ])
            .MapGet<List<SheetUserListItemVm>>(GetUserSheets, "user", statusCodes: [
                StatusCodes.Status200OK,
                StatusCodes.Status401Unauthorized
             ])
            .MapGet<SheetVm>(GetSheet, "{id}", statusCodes: [
                StatusCodes.Status201Created,
                StatusCodes.Status400BadRequest,
                StatusCodes.Status404NotFound,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status403Forbidden
             ])
            .MapPost<int>(CreateSheet, statusCodes: [
                StatusCodes.Status200OK,
                StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
             ])
            .MapPut(UpdateSheet, "{id}", statusCodes: [
                StatusCodes.Status204NoContent,
                StatusCodes.Status400BadRequest,
                StatusCodes.Status404NotFound,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status403Forbidden
             ])
            .MapDelete(DeleteSheet, "{id}", statusCodes: [
                StatusCodes.Status204NoContent,
                StatusCodes.Status404NotFound,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status403Forbidden
             ]);
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
