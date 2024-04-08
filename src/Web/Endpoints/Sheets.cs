
using DnDCharacterSheet.Application;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;

namespace DnDCharacterSheet.Web.Endpoints;

public class Sheets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateSheet)
            .MapPut(UpdateSheet, "{id}")
            .MapDelete(DeleteSheet, "{id}");
    }

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
