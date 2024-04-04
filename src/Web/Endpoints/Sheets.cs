
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Web.Endpoints;

public class Sheets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateSheet);
    }

    public Task<int> CreateSheet(ISender sender, CreateSheetCommand command) => sender.Send(command);
}
