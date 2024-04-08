
using DnDCharacterSheet.Application;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Web.Endpoints;

public class Sheets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateSheet)
            .MapPut(UpdateSheet, "{id}");
    }

    public Task<int> CreateSheet(ISender sender, CreateSheetCommand command) => sender.Send(command);

    public async Task<IResult> UpdateSheet(ISender sender, int id, UpdateSheetCommand command)
    {
        command.Id(id);
        await sender.Send(command);
        return Results.NoContent();
    }
}
